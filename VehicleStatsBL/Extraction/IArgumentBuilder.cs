using System.Collections.Generic;
using VehicleStats.Core.Statistics;

namespace VehicleStats.Core.Extraction
{
    public interface IArgumentBuilder
    {
        string SourceSystem { get;  }
        IList<string> GetAllMakes();
        IList<string> GetAllModels(string make);
        List<VehicleMakeModel> GetVehicleMakeModels();
    }
}
