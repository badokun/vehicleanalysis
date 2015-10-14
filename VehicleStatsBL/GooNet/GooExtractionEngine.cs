using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using log4net;
using VehicleStats.Core.Extraction;

namespace VehicleStats.Core.GooNet
{
    public class GooExtractionEngine : IExtractionEngine
    {
        private const string PostUrl = "http://www.goo-net.com/php/search/summary.php";
        private readonly IHtmlWebWrapper _htmlWebWrapper;
        private readonly ILog _log;
        private readonly GooNetPageScraper _pageScraper;

        public GooExtractionEngine(IHtmlWebWrapper htmlWebWrapper, ILog log, GooNetPageScraper pageScraper)
        {
            _htmlWebWrapper = htmlWebWrapper;
            _log = log;
            _pageScraper = pageScraper;
        }

        public void Extract(IExtractionArguments args, IExtractionResults extractionResults)
        {
            if (extractionResults == null) throw new ArgumentNullException("extractionResults");

            _log.Debug("Extraction starting");
            extractionResults.Start();

            //http://www.goo-net.com/usedcar/LEXUS__IS_F.html
            var areaSelectionUri = _pageScraper.GetFirstPageUrl(args);
            var areaSelectionPage = _htmlWebWrapper.Load(areaSelectionUri.OriginalString);

            var postValues = BuildPostDictionary(args, areaSelectionPage);
            var firstPage = _htmlWebWrapper.Post(PostUrl, postValues);

            extractionResults.Vehicles.AddRange(_pageScraper.Scrape(args, firstPage));

            var numberOfPages = _pageScraper.GetPageCount(firstPage);

            var postValueList = new List<NameValueCollection>();

            for (int i = 1; i < numberOfPages; i++)
            {
                var pageNameValueCollection = new NameValueCollection(postValues);
                pageNameValueCollection["offset"] = (i*30).ToString();
                postValueList.Add(pageNameValueCollection);
            }
            
            foreach (var resultsPage in postValueList.Select(nameValueCollection => _htmlWebWrapper.Post(PostUrl, nameValueCollection)))
            {
                extractionResults.Vehicles.AddRange(_pageScraper.Scrape(args, resultsPage));
            }
            
            extractionResults.Stop();
        }

        private static NameValueCollection BuildPostDictionary(IExtractionArguments args, HtmlDocument page)
        {
            var carId = page.DocumentNode.SelectSingleNode("//input[@name='integration_car_cd']").Attributes["value"].Value.Replace("|", string.Empty);

            var maker_cd = page.DocumentNode.SelectSingleNode("//input[@name='maker_cd']").Attributes["value"].Value;

            var nameValue = new NameValueCollection
            {
                {"maker_cd", maker_cd},
                {"car_price", "1"},
                {"total_payment", "1"},
                {"smart_phone_paging_flg", "0"},
                {"integration_car_cd", carId},
                {"area_search_flg", "1"},
                {"pref_all", "on"},
                {"area_search_flg", "1"},
                {"pref_c", "08,10,09,13,11,12,14"},
                {"car_cd", carId},
                {"area_s_id", "1301,1302,1402,1403"},
                {"select_pref", "08,09,10,11,12,13,14"},
                {"integration_car_cd", carId + "|"},
                {"new_car_cds_list", carId},
                {"baitai", "goo"},
                {"search_type", "car_search"},
                {"disp_mode", "detail_list"},
                {"page", "1"},
                {"offset", "0"},
                {"nen1", args.From.ToString()},
                {"nen2", args.To.ToString()}
            };

            return nameValue;
        }

         

    }
}
   