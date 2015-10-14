using System;
using System.Collections.Generic;

namespace VehicleStats.Core.Extraction
{
    public interface IExtractionResults
    {
        List<IVehicle> Vehicles { get; }
        TimeSpan OperationDuration { get; }

        void Start();
        void Stop();

        int LowestYear {get ;}
        int HighestYear { get; }
    }

   
}