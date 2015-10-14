using System;
using System.Collections.Generic;
using VehicleStats.Core.Extraction;

namespace VehicleStats.Core.Statistics
{
    public interface IStatistics
    {
        double MeanPrice { get;   }
        double MedianPrice { get;  }
        double ModePrice { get;   }
        double StandardDeviation { get;   }
        int SampleSize { get; }
        double LowestPrice { get; }
        double HighestPrice { get; }

        string Make { get; }
        string Model { get; }

        IEnumerable<Tuple<int, double, int>> MeanPriceByYear { get; }
        IEnumerable<Tuple<int, double, int>> DepreciationByYear { get; }
        IEnumerable<Tuple<int, double>> DepreciationByYearCumulative { get; }

        IList<OutlierVehicle> OutlierVehicles { get; }
        IEnumerable<IVehicle> Vehicles { get; }

    }
}
