using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleStats.Core.Extraction;

namespace StatisticsExtractor.ViewModels
{
    public interface IExtractionArgumentsViewModel
    {        
        IExtractionArguments ExtractionArguments { get; set; }
        bool SaveResultsToDisc { get; set; }
    }
}
