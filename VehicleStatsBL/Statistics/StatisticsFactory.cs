using System.Collections.Generic;
using System.Linq;
using VehicleStats.Core.Extraction;

namespace VehicleStats.Core.Statistics
{
    public static class StatisticsFactory
    {
        public static IStatistics GetStatistics(IList<IVehicle> sampleVehicles, bool removeOutliers = true)
        {
            if (sampleVehicles == null || !sampleVehicles.Any())
                return new NullStatistics();

            return new Statistics(sampleVehicles, removeOutliers);
        }
    }
}
