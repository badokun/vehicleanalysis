using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;
using log4net;
using VehicleStats.Core.Extraction;

namespace VehicleStats.Core.AutoTrader
{
    public class AutoTraderZaPageScraper : IAutoTraderZaPageScraper
    {
        private NumberFormatInfo _numberFormatInfo;
        private ILog _log;
        private Uri _baseUri = new Uri("http://www.autotrader.co.za/makemodel/make/{0}/model/{1}/");

        public AutoTraderZaPageScraper(ILog log)
        {
            _log = log;

            _numberFormatInfo = new NumberFormatInfo();
            _numberFormatInfo.NegativeSign = "-";
            _numberFormatInfo.CurrencyDecimalSeparator = ".";
            _numberFormatInfo.CurrencyGroupSeparator = ",";
            _numberFormatInfo.CurrencySymbol = "R";
        }
      
        public IList<IVehicle> Scrape(IExtractionArguments args, HtmlDocument page)
        {
            var vehicleList = new List<IVehicle>();
            var searchResults = page.DocumentNode.SelectNodes("//div[@class='searchResult   ']");
            if (searchResults == null)
                return vehicleList;

            foreach (var searchResult in searchResults)
            {
                IVehicle vehicle = new Vehicle();
                vehicle.Make = args.Make;
                vehicle.Model = args.Model;
                vehicle.Title = searchResult.SelectSingleNode(".//h2[@class='serpTitle']").InnerText.Trim();

                vehicle.Price = GetVehiclePrice(searchResult);
                vehicle.Milage = GetMilage(searchResult);
                vehicle.Year = GetAge(searchResult);

                vehicleList.Add(vehicle);
                _log.DebugFormat("Added {0}", vehicle);
            }
            return vehicleList;
        }

        private int GetAge(HtmlNode searchResult)
        {
            int age;
            int.TryParse(searchResult.SelectSingleNode(".//div[@class='serpAge']").InnerText.Trim(), out age);
            return age;
        }

        private string GetMilage(HtmlNode searchResult)
        {
            try
            {
                return searchResult.SelectSingleNode(".//ul[@class='advertSpecs']").InnerText.Split('\r').FirstOrDefault(p => p.Contains("km")).Trim().Replace("km", string.Empty).Replace(",", string.Empty);
            }
            catch
            {
                return string.Empty;
            }
        }

        private double GetVehiclePrice(HtmlNode searchResult)
        {
            double price;
            double.TryParse(searchResult.SelectSingleNode(".//div[@class='serpPrice']").InnerText.Trim(), NumberStyles.Currency, _numberFormatInfo, out price);
            return price;
        }


        private const string yearString = "caryearrangeszar/{0}/";
        private Uri _firstPageUrl;

        public Uri GetFirstPageUrl(IExtractionArguments args)
        {
            _firstPageUrl = new Uri(string.Format(_baseUri.OriginalString, args.Make, args.Model));
            string dateRange = string.Empty;
            for (int i = args.From; i <= args.To; i++)
            {
                dateRange += string.Format(yearString, i);
            }

            _firstPageUrl = new Uri(_firstPageUrl.OriginalString + dateRange + "search");
            return _firstPageUrl;
        }

        public List<string> GetRemainingUrls(HtmlDocument firstPage)
        {
            if (_firstPageUrl == null)
                throw new InvalidOperationException("First call the GetFirstPageUrl method");

            List<string> remainingUrls = new List<string>();
            int lastPageIndex = GetLastPageIndex(firstPage);

            for (int i = 1; i < lastPageIndex + 1; i++)
            {
                remainingUrls.Add(_firstPageUrl + "?pageNumber=" + i);
            }

            return remainingUrls;
        }

        private int GetLastPageIndex(HtmlDocument page)
        {
            try
            {
                var href = page.DocumentNode.SelectSingleNode("//ol[@class='paginator']").SelectSingleNode(".//li/a[@class='last']").Attributes["href"].Value;
                return int.Parse(href.Substring(href.LastIndexOf("=") + 1));
            }
            catch
            {
                return 0;
            }
        }
    }
}
