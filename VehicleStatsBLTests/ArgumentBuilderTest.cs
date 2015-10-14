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
    public class AutoTraderArgumentBuilderTest
    {
        private IArgumentBuilder _argumentBuilder;
        [TestInitialize]
        public void Init()
        {
            var httpWebWrapper = MockRepository.GenerateMock<IHtmlWebWrapper>();
            var log = MockRepository.GenerateMock<ILog>();

            var testRootDocument = new HtmlDocument();
            var testRootDocumentPath = Path.GetFullPath(@"TestData\Root - Auto Trader South Africa - Used Cars for sale_files\Auto Trader South Africa - Used Cars for sale_za-.htm");
            testRootDocument.Load(testRootDocumentPath);
            httpWebWrapper.Stub(h => h.Load(Arg<string>.Is.Anything)).Return(testRootDocument);

            var jsonResponse = File.ReadAllText(Path.GetFullPath(@"TestData\jsonResponse.txt"));

            httpWebWrapper.Stub(h => h.DownloadString(Arg<string>.Is.Anything)).Return(jsonResponse);

            _argumentBuilder = new AutoTraderArgumentBuilder(httpWebWrapper, log);
        }

        [TestMethod]
        public void GetAllMakes()
        {
            var makes = _argumentBuilder.GetAllMakes();
            Assert.AreEqual(98, makes.Count);
        }

        [TestMethod]
        public void NoEmptyStringMakes()
        {
            var makes = _argumentBuilder.GetAllMakes();
            Assert.AreEqual(0, makes.Count(string.IsNullOrEmpty));
        }

        [TestMethod]
        public void GetAllModels()
        {
            var make = "bmw";
            var models = _argumentBuilder.GetAllModels(make);

            Assert.AreEqual(30, models.Count);
        }

        [TestMethod]
        public void GetVehicleMakeModels()
        {
            var vehicleMakeModels = _argumentBuilder.GetVehicleMakeModels();
            Assert.AreEqual(98, vehicleMakeModels.Count);
            Assert.AreEqual(30, vehicleMakeModels.First(v => v.Make=="bmw").Models.Count);
        }

    }
}