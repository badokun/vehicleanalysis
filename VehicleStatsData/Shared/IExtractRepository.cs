using System.Collections.Generic;
using VehicleStats.Core.Extraction;
using VehicleStats.Core.Statistics;

namespace VehicleStats.Data.Shared
{
    public interface IExtractRepository
    {
        void Write(IExtractionArguments arguments, IExtractionResults extractionResults, string sourceSystem);        
        IExtractionResults Read(IExtractionArguments arguments, string sourceSystem);
        bool TryRead(IExtractionArguments arguments, out IExtractionResults results);

        void WriteVehicleMakeModel(IList<VehicleMakeModel> allMakesModels, string sourceSystem);
        List<VehicleMakeModel> ReadVehicleMakeModel(string sourceSystem);
    }
}
