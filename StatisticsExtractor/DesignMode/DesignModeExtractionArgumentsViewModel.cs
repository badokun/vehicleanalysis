using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleStats.Core.Extraction;

namespace StatisticsExtractor.DesignMode
{
    public class DesignModeExtractionArgumentsViewModel
    {
        public DesignModeExtractionArgumentsViewModel()
        {
            SaveResultsToDisc = true;
        }
        public IExtractionArguments ExtractionArguments 
        {
            get
            {
                return VehicleStats.Core.Extraction.ExtractionArguments.Create("Opel", "Kadet", 2001, 2002);
            }
        }

        public bool SaveResultsToDisc { get; set; }

        public List<RecentSearch> RecentSearches
        {
            get
            {
                return new List<RecentSearch>() 
                {
                    new RecentSearch() { LocationOnDisc = "c:\\test", SearchedOn = DateTime.Now } ,
                    new RecentSearch() { LocationOnDisc = "c:\\test2", SearchedOn = DateTime.Now } 
                }
                ;
            }
        }
    }

    public class RecentSearch
    {
        public DateTime SearchedOn { get; set; }
        public string LocationOnDisc { get; set; }
    }
}
