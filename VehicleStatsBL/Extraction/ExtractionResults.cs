using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace VehicleStats.Core.Extraction
{
    [Serializable]
    public class ExtractionResults : IExtractionResults
    {
        private List<IVehicle> _vehicles;
        
        [NonSerialized]
        private Stopwatch _stopwatch;

        public ExtractionResults()
        {
            _stopwatch = new Stopwatch();
            _vehicles = new List<IVehicle>();
        }

        public List<IVehicle> Vehicles
        {
            get { return _vehicles; }
        }

        public TimeSpan OperationDuration
        {
            get { return _stopwatch.Elapsed; }
        }

        public void Stop()
        {
            if (_stopwatch != null)
                _stopwatch.Stop();
        }

        public void Start()
        {
            _stopwatch.Start();
        }

        public int LowestYear
        {
            get
            { return _vehicles.OrderBy(p => p.Year).FirstOrDefault().Year; }
        }

        public int HighestYear
        {
            get
            { return _vehicles.OrderByDescending(p => p.Year).FirstOrDefault().Year; }
        }
               
    }
}
