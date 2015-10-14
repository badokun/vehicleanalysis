using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleStats.Core.Extraction;

namespace StatisticsExtractor.ViewModels
{
    public class ExtractionArgumentsViewModel : IExtractionArgumentsViewModel
    {
        public IExtractionArguments ExtractionArguments { get; set; }

        public bool SaveResultsToDisc
        {
            get;
            set;
        }
    }
}
