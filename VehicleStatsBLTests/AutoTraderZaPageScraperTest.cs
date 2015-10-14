using System.IO;
using System.Linq;
using HtmlAgilityPack;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using VehicleStats.Core.AutoTrader;
using VehicleStats.Core.Extraction;

namespace VehicleStats.Core.Test
{
    [TestClass]
    public class AutoTraderZaPageScraperTest
    {
        private ILog _log;
        private IExtractionArguments _extractionArguments;
        private AutoTraderZaPageScraper _pageScraper;

        [TestInitialize]
        public void Init()
        {
            _log = MockRepository.GenerateMock<ILog>();
            _extractionArguments = MockRepository.GenerateMock<IExtractionArguments>();
            _pageScraper = new AutoTraderZaPageScraper(_log);
        }

        [TestMethod]
        public void can_extract_vehicles()
        {
            var document = GetTestDocument();
            var vehicles = _pageScraper.Scrape(_extractionArguments, document);

            Assert.AreEqual(vehicles.Count, 9);
        }

        [TestMethod]
        public void all_vehicle_prices_are_populated()
        {
            var document = GetTestDocument();
            var vehicles = _pageScraper.Scrape(_extractionArguments, document);

            Assert.AreEqual(vehicles.Where(p => p.Price != 0).Count(), 9);
        }

        [TestMethod]
        public void all_vehicle_makes_are_populated()
        {
            _extractionArguments.Stub(p => p.Make).Return("TestMake");
            var document = GetTestDocument();
            var vehicles = _pageScraper.Scrape(_extractionArguments, document);

            Assert.AreEqual(vehicles.Where(p => p.Make == _extractionArguments.Make).Count(), 9);
        }

        [TestMethod]
        public void all_vehicle_models_are_populated()
        {
            _extractionArguments.Stub(p => p.Model).Return("TestModel");
            var document = GetTestDocument();
            var vehicles = _pageScraper.Scrape(_extractionArguments, document);

            Assert.AreEqual(vehicles.Where(p => p.Model == _extractionArguments.Model).Count(), 9);
        }

        private HtmlDocument GetTestDocument()
        {
            HtmlWebWrapper wrapper = new HtmlWebWrapper(_log);
            var testDocumentPath = Path.GetFullPath(@"TestData\Used cars for sale in South Africa   Auto Trader South Africa.htm");
            return wrapper.Load(testDocumentPath);
        }
    }


}
