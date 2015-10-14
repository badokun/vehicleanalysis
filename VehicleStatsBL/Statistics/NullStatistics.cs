using System;
using System.CodeDom;
using System.Collections.Generic;
using VehicleStats.Core.Extraction;

namespace VehicleStats.Core.Statistics
{
    public class NullStatistics : IStatistics
    {
        public double MeanPrice
        {
            get { return 0; }
        }

        public double MedianPrice
        {
            get { return 0; }
        }

        public double ModePrice
        {
            get { return 0; }
        }

        public double StandardDeviation
        {
            get { return 0; }
        }

        public int SampleSize
        {
            get { return 0; }
        }

        public double LowestPrice
        {
            get { return 0; }
        }

        public double HighestPrice
        {
            get { return 0; }
        }

        public string Make { get; private set; }
        public string Model { get; private set; }

        public IEnumerable<Tuple<int, double, int>> MeanPriceByYear
        {
            get { return new List<Tuple<int, double, int>>(); }
        }

        public IEnumerable<Tuple<int, double, int>> DepreciationByYear
        {
            get { return new List<Tuple<int, double, int>>(); }
        }


        public IEnumerable<Tuple<int, double>> DepreciationByYearCumulative
        {
            get { return new List<Tuple<int, double>>(); }
        }

        public IList<OutlierVehicle> OutlierVehicles { get; private set; }
        public IEnumerable<IVehicle> Vehicles { get; private set; }
    }
}
