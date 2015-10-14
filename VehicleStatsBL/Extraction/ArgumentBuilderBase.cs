using System.Collections.Generic;
using System.Linq;
using VehicleStats.Core.Statistics;

namespace VehicleStats.Core.Extraction
{
    public abstract class ArgumentBuilderBase : IArgumentBuilder
    {
        public abstract string SourceSystem { get; }
        public abstract IList<string> GetAllMakes();

        public abstract IList<string> GetAllModels(string make);

        public List<VehicleMakeModel> GetVehicleMakeModels()
        {
            var allMakesModels = GetAllMakes()
                .Select(make => new VehicleMakeModel(make)).ToList();

            allMakesModels.ForEach(makeModelDictionary => makeModelDictionary.Models.AddRange(GetAllModels(makeModelDictionary.Make)));
            return allMakesModels;
        }
    }
}