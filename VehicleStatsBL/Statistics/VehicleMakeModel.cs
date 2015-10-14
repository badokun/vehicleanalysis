using System.Collections.Generic;

namespace VehicleStats.Core.Statistics
{
    public class VehicleMakeModel
    {
        public string Make { get; set; }

        public VehicleMakeModel(string make)
        {
            Make = make;
            Models = new List<string>();
        }

        public List<string> Models { get; set; }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Make, Models.Count);
        }
    }
}
