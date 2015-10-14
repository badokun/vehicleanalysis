using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;
using log4net;
using VehicleStats.Core.Extraction;

namespace VehicleStats.Core.AutoTrader
{
    public class AutoTraderExtractionEngine : IExtractionEngine
    {
          
        private object _lockObject = new object();
        private IHtmlWebWrapper _htmlWebWrapper;
        private ILog _log;
        private IAutoTraderZaPageScraper _pageScraper;

        public AutoTraderExtractionEngine(IHtmlWebWrapper htmlWebWrapper, ILog log, IAutoTraderZaPageScraper pageScraper)
        {
            _log = log;
            _htmlWebWrapper = htmlWebWrapper;
            _pageScraper = pageScraper;
            
        }

        public void Extract(IExtractionArguments args, IExtractionResults extractionResults)
        {
            if (extractionResults == null) throw new ArgumentNullException("extractionResults");

            _log.Debug("Extraction starting");
            extractionResults.Start();

            var firstPageUrl = _pageScraper.GetFirstPageUrl(args);
            HtmlDocument firstPage = _htmlWebWrapper.Load(firstPageUrl.OriginalString);

            extractionResults.Vehicles.AddRange(_pageScraper.Scrape(args, firstPage));

            List<string> remainingUrls = _pageScraper.GetRemainingUrls(firstPage);

            Parallel.ForEach(remainingUrls, pagedUrl =>
            {
                var resultsPage = _htmlWebWrapper.Load(pagedUrl);

                lock (_lockObject)
                {
                    extractionResults.Vehicles.AddRange(_pageScraper.Scrape(args, resultsPage));
                }

            });

            extractionResults.Stop();
        }
    }
}
