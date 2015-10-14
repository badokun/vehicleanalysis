using System;
using VehicleStats.Core.Extraction;

namespace VehicleStats.Core.Statistics
{
    public class OutlierVehicle
    {
        public IVehicle Vehicle { get; set; }
        public OutlierType OutlierType { get; set; }
        public double Limit { get; set; }
        

        public OutlierVehicle(IVehicle vehicle, OutlierType outlierType, double limit)
        {
            Vehicle = vehicle;
            OutlierType = outlierType;
            Limit = limit;
        }

        public string OutlierReason
        {
            get
            {
                switch (OutlierType)
                {
                    case OutlierType.Above:
                        return "Price above " + Limit;
                    case OutlierType.Under:
                        return "Price under " + Limit;
                        case OutlierType.ZeroPrice:
                        return "Price is zero";
                    default :
                        throw new NotImplementedException("OutlierType not implemented");
                }
            }
        }
    }
}