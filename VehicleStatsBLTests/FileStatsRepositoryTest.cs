using System.IO;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleStats.Core.Extraction;
using VehicleStats.Data;
 
using Vehicle = VehicleStats.Core.Extraction.Vehicle;

namespace VehicleStats.Core.Test
{
    // [TestClass]
    //public class FileStatsRepositoryTest
    //{
    //     private const string TestPath = "TestData";
    //    // private const string TestDataFile = @"volvo[s60] 2000_2013 (20131111).dat";

    //     //private string ResolvedTestPath;
    //    // private string ResolvedTestFile;
    //     //private string DummyOutputFile;

    //     private ILog _log;
    //     private FileExtractRepository _fileExtractRepository;
    //     private ExtractionResults _results;
    //     private Vehicle _testVehicle;

    //     [TestInitialize]
    //     public void Init()
    //     {
    //         _log = Rhino.Mocks.MockRepository.GenerateMock<ILog>();
    //         _fileExtractRepository = new FileExtractRepository(_log, Path.GetFullPath(TestPath));
    //         _results = new ExtractionResults();
    //         _testVehicle = new Vehicle() { Make = "bmw", Model = "3-series", Milage = "100", Price = 100000, Title = "BMW M3", Year = 2013 };
    //         _results.Vehicles.Add(_testVehicle);
          

    //       //  ResolvedTestPath = Path.GetFullPath(TestPath);
    //        // ResolvedTestFile = Path.GetFullPath(string.Format("{0}\\{1}", TestPath, TestDataFile));
    //        // DummyOutputFile = string.Format("{0}\\{1}", ResolvedTestPath, "dummy.dat");
    //     }

    //     [TestCleanup]
    //     public void Cleanup()
    //     {
    //         //if (File.Exists(DummyOutputFile))
    //         //    File.Delete(DummyOutputFile);
    //     }

    //     [TestMethod]
    //     public void test_that_you_can_deserialise()
    //     {
    //         var arguments = ExtractionArguments.Create("bmw", "3-series", 2001, 2014);
    //         var extractionResults = _fileExtractRepository.Read(arguments);

    //         Assert.IsNotNull(extractionResults);
    //     }

    //     [TestMethod]
    //     public void test_that_you_can_serialise()
    //     {
    //         var arguments = ExtractionArguments.Create("bmw", "3-series", 2000, 2013);
    //         _fileExtractRepository.Write(arguments,  _results);

    //         Assert.IsTrue(File.Exists(_fileExtractRepository.GetFilePath(arguments)));
    //     }

    //     [TestMethod]
    //     public void test_that_you_can_serialise_and_deserialse()
    //     {
    //         var arguments = ExtractionArguments.Create("bmw", "3-series", 2000, 2013);
    //         _fileExtractRepository.Write(arguments, _results);
    //         var extractionResults = _fileExtractRepository.Read(arguments);

    //         Assert.IsNotNull(extractionResults);
    //     }


    //     [TestMethod]
    //     [ExpectedException(typeof(FileNotFoundException))]
    //     public void test_that_it_handles_files_that_do_not_exist()
    //     {
    //         var arguments = ExtractionArguments.Create("bmw", "4-series", 2000, 2013);
    //         var extractionResults = _fileExtractRepository.Read(arguments);
    //     }
    //}
}
