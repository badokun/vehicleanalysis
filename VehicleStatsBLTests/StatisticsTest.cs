using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VehicleStats.Core.Extraction;
using VehicleStats.Core.Statistics;

namespace VehicleStats.Core.Test
{
    [TestClass]
    public class StatisticsTest
    {
        private IList<IVehicle> _outlierVehiclesList;
        private List<IVehicle> _depreciationVehicles;

        [TestInitialize]
        public void Init()
        {
            BuildOutlierVehicles();
            BuildDepreciationVehicles();
        }

        [TestMethod]
        public void Can_Identify_Outliers()
        {
            IStatistics stats = new Statistics.Statistics(_outlierVehiclesList.ToList());

            Assert.AreEqual(1, stats.OutlierVehicles.Count);
            Assert.AreEqual(15, stats.OutlierVehicles.First().Vehicle.Price);
        }

        [TestMethod]
        public void If_Statistics_Is_CreatedWith_RemoveOutliersFalse_ShouldHaveNoOutliers()
        {
            IStatistics stats = new Statistics.Statistics(_outlierVehiclesList.ToList(), false);

            Assert.AreEqual(0, stats.OutlierVehicles.Count);
        }

        [TestMethod]
        public void Test_MeanPriceByYear()
        {
            IStatistics stats = new Statistics.Statistics(_depreciationVehicles.ToList());

            Assert.AreEqual(100000, stats.MeanPriceByYear.First(y => y.Item1 == 2014).Item2);
            Assert.AreEqual(90000, stats.MeanPriceByYear.First(y => y.Item1 == 2013).Item2); 
            Assert.AreEqual(80000, stats.MeanPriceByYear.First(y => y.Item1 == 2012).Item2);
            Assert.AreEqual(70000, stats.MeanPriceByYear.First(y => y.Item1 == 2011).Item2);
        }

        [TestMethod]
        public void Test_MeanPrice()
        {
            IStatistics stats = new Statistics.Statistics(_depreciationVehicles.ToList());
            Assert.AreEqual(85000, stats.MeanPrice);
        }

        [TestMethod]
        public void Test_DepreciationByYear()
        {
            IStatistics stats = new Statistics.Statistics(_depreciationVehicles.ToList());
            Assert.AreEqual(0, stats.DepreciationByYear.First(y=>y.Item1 == 2014).Item2);
            Assert.AreEqual(-10, stats.DepreciationByYear.First(y => y.Item1 == 2013).Item2);
            Assert.AreEqual(-11.11, Math.Round(stats.DepreciationByYear.First(y => y.Item1 == 2012).Item2, 2));
            Assert.AreEqual(-12.5, Math.Round(stats.DepreciationByYear.First(y => y.Item1 == 2011).Item2, 2));
        }

        [TestMethod]
        public void Test_DepreciationByYearCumulative()
        {
            IStatistics stats = new Statistics.Statistics(_depreciationVehicles.ToList());
            Assert.AreEqual(0, stats.DepreciationByYearCumulative.First(y => y.Item1 == 2014).Item2);
            Assert.AreEqual(-10, stats.DepreciationByYearCumulative.First(y => y.Item1 == 2013).Item2);
            Assert.AreEqual(-20, Math.Round(stats.DepreciationByYearCumulative.First(y => y.Item1 == 2012).Item2, 2));
            Assert.AreEqual(-30, Math.Round(stats.DepreciationByYearCumulative.First(y => y.Item1 == 2011).Item2, 2));
        }

        [TestMethod]
        public void Test_MedianPrice()
        {
            IStatistics stats = new Statistics.Statistics(_depreciationVehicles.ToList());
            Assert.AreEqual(85000, stats.MedianPrice);
        }

        [TestMethod]
        public void Test_ModePrice()
        {
            IStatistics stats = new Statistics.Statistics(_outlierVehiclesList.ToList());
            Assert.AreEqual(3, stats.ModePrice);
        }

        [TestMethod]
        public void Test_StandardDeviation()
        {
            IStatistics stats = new Statistics.Statistics(_outlierVehiclesList.ToList());
            IStatistics statsDepreciation = new Statistics.Statistics(_depreciationVehicles.ToList());
            Assert.AreEqual(1.05, Math.Round(stats.StandardDeviation, 2));
            Assert.AreEqual(15000, statsDepreciation.StandardDeviation);
        }

        [TestMethod]
        public void Test_SampleSize_WithOutliers()
        {
            IStatistics stats = new Statistics.Statistics(_outlierVehiclesList.ToList(), false);
            Assert.AreEqual(8, stats.SampleSize);
        }

        [TestMethod]
        public void Test_SampleSize_WithoutOutliers()
        {
            IStatistics stats = new Statistics.Statistics(_outlierVehiclesList.ToList());
            Assert.AreEqual(7, stats.SampleSize);
        }

        [TestMethod]
        public void Test_LowestPrice()
        {
            IStatistics stats = new Statistics.Statistics(_outlierVehiclesList.ToList());
            Assert.AreEqual(2, stats.LowestPrice);
        }

        [TestMethod]
        public void Test_HighestPrice_With_Ourliers()
        {
            IStatistics stats = new Statistics.Statistics(_outlierVehiclesList.ToList(), false);
            Assert.AreEqual(15, stats.HighestPrice);
        }

        [TestMethod]
        public void Test_HighestPrice_Without_Ourliers()
        {
            IStatistics stats = new Statistics.Statistics(_outlierVehiclesList.ToList());
            Assert.AreEqual(5, stats.HighestPrice);
        }

[TestMethod]
        public void RemoveZeroPriced()
        { }


        private void BuildOutlierVehicles()
        {
            //4, 5, 2, 3, 15, 3, 3, 5

            _outlierVehiclesList = new List<IVehicle>
            {
                new Vehicle() {Year = 2014, Price = 4},
                new Vehicle() {Year = 2014, Price = 5},
                new Vehicle() {Year = 2014, Price = 2},
                new Vehicle() {Year = 2014, Price = 3},
                new Vehicle() {Year = 2014, Price = 15},
                new Vehicle() {Year = 2014, Price = 3},
                new Vehicle() {Year = 2014, Price = 3},
                new Vehicle() {Year = 2014, Price = 5}
            };
        }

        private void BuildDepreciationVehicles()
        {
            _depreciationVehicles = new List<IVehicle>
            {
                new Vehicle() {Year = 2014, Price = 110000 },
                new Vehicle() {Year = 2014, Price = 90000 },
                new Vehicle() {Year = 2013, Price = 100000 },
                new Vehicle() {Year = 2013, Price = 80000 },
                new Vehicle() {Year = 2012, Price = 90000 },
                new Vehicle() {Year = 2012, Price = 70000 },
                new Vehicle() {Year = 2011, Price = 80000 },
                new Vehicle() {Year = 2011, Price = 60000 }
            };
        }
    }
}
