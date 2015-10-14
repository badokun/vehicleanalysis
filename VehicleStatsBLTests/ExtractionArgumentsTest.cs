using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleStats.Core.Extraction;

namespace VehicleStats.Core.Test
{
    [TestClass]
    public class ExtractionArgumentsTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "from or to")]
        public void test_that_you_cannot_exceed_max_year_range()
        {
            var args = ExtractionArguments.Create("bmw", "3-series", 1900, 2013);
        }

        [TestMethod]
        public void test_that_you_can_create_args_within_year_range()
        {
            var args = ExtractionArguments.Create("bmw", "3-series", 2012, 2013);
            Assert.IsNotNull(args);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "make")]
        public void test_that_you_cannot_pass_empty_vehicle_make()
        {
            var args = ExtractionArguments.Create("", "3-series", 2012, 2013);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "model")]
        public void test_that_you_cannot_pass_empty_vehicle_model()
        {
            var args = ExtractionArguments.Create("bmw", "", 2012, 2013);
        }
    }
}
