using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using VehicleStats.Core.AutoTrader;
using VehicleStats.Core.Extraction;

namespace VehicleStats.Core.Test
{
    [TestClass]
    public class ExtractionEngineTest
    {
        private IHtmlWebWrapper _htmlWrapper;
        private ILog _log;
        private AutoTraderExtractionEngine _extractorEngine;
        private IExtractionArguments _extractionArgs;
        private IExtractionResults _extractionResults;
        private Uri _dummyUri;
        private HtmlDocument _dummyFirstDocument;
        private IAutoTraderZaPageScraper _pageScraper;

        [TestInitialize]
        public void Init()
        {
            _htmlWrapper = MockRepository.GenerateMock<IHtmlWebWrapper>();
            _log = MockRepository.GenerateMock<ILog>();
            _extractionArgs = MockRepository.GenerateMock<IExtractionArguments>();
            _extractionResults = MockRepository.GenerateMock<IExtractionResults>();
            _pageScraper = MockRepository.GenerateMock<IAutoTraderZaPageScraper>();

            _pageScraper.Stub(p => p.Scrape(Arg<IExtractionArguments>.Is.Anything, Arg<HtmlDocument>.Is.Anything)).Return(new List<IVehicle>());
            _extractionResults.Stub(p => p.Vehicles).Return(new List<IVehicle>());


            _dummyUri = new Uri("http://dummyUrl.com");
            _dummyFirstDocument = new HtmlDocument();

            _extractorEngine = new AutoTraderExtractionEngine(_htmlWrapper, _log, _pageScraper);
        }

        [TestMethod]
        public void test_an_empty_first_results_page_returns_with_no_exceptions()
        {
            _pageScraper.Stub(p => p.GetFirstPageUrl(_extractionArgs)).Return(_dummyUri);

            _htmlWrapper.Stub(p => p.Load(_dummyUri.OriginalString)).Return(_dummyFirstDocument);
            _pageScraper.Stub(p => p.GetRemainingUrls(_dummyFirstDocument)).Return(new List<string>());
            _pageScraper.Stub(p => p.Scrape(Arg<IExtractionArguments>.Is.Anything, Arg<HtmlDocument>.Is.Anything)).Return(new List<IVehicle>());
            _extractionResults.Stub(p => p.Vehicles).Return(new List<IVehicle>());

            _extractorEngine.Extract(_extractionArgs, _extractionResults);
        }

        [TestMethod]
        public void test_extractionResults_operation_duration_works()
        {
            _pageScraper.Stub(p => p.GetFirstPageUrl(_extractionArgs)).Return(_dummyUri);
            _htmlWrapper.Stub(p => p.Load(_dummyUri.OriginalString)).Return(_dummyFirstDocument);
            _pageScraper.Stub(p => p.GetRemainingUrls(_dummyFirstDocument)).Return(new List<string>());
            var extractionResults = new ExtractionResults();

            _extractorEngine.Extract(_extractionArgs, extractionResults);

            Assert.AreNotEqual(extractionResults.OperationDuration.TotalMilliseconds, 0);
        }

        [TestMethod]
        public void test_can_read_multiple_pages()
        {
            //HtmlWebWrapper wrapper = new HtmlWebWrapper(_log);
            //var testDocumentPath = Path.GetFullPath(@"TestData\Used cars for sale in South Africa   Auto Trader South Africa.htm");
            ////var testDocument = wrapper.Load(testDocumentPath);
            //_pathResolver.Stub(p => p.GetFirstPageUrl(Arg<IEngineArguments>.Is.Anything, Arg<IExtractionArguments>.Is.Anything)).Return(_dummyUri);
            //_pathResolver.Stub(p => p.GetRemainingUrls(Arg<HtmlDocument>.Is.Anything)).Return(new List<string>());
            //_htmlWrapper.Stub(p => p.Load(Arg<string>.Is.Anything)).Return(testDocument);
            //var extractionResults = new ExtractionResults(_statisticsFactory);

            //_extractorEngine.Extract(_extractionArgs, extractionResults);

            //Assert.AreEqual(extractionResults.Vehicles.Count, 9);
        }
        

    }
}
