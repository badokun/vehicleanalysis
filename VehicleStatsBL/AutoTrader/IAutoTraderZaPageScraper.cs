using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using VehicleStats.Core.Extraction;

namespace VehicleStats.Core.AutoTrader
{
    public interface IAutoTraderZaPageScraper
    {
        IList<IVehicle> Scrape(IExtractionArguments args, HtmlDocument page);

        Uri GetFirstPageUrl(IExtractionArguments extractionArgs);

        List<string> GetRemainingUrls(HtmlDocument page);

    }
}