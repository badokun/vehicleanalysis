using System.Collections.Generic;

namespace VehicleStats.Core.AutoTrader
{
    // http://json2csharp.com/# for http://www.autotrader.co.za/refresh-quick-search/makemodel/make/BMW?
    public class AutoTraderResponse
    {
        public class Model
        {
            public int count { get; set; }
            public string name { get; set; }
            public bool selected { get; set; }
            public string seoName { get; set; }
        }

        public class CarAndCommercialPriceRangesZar
        {
            public int count { get; set; }
            public string displayName { get; set; }
            public string name { get; set; }
            public bool selected { get; set; }
            public string seoName { get; set; }
        }

        public class Dimensions
        {
            public List<Model> Model { get; set; }
            public List<CarAndCommercialPriceRangesZar> CarAndCommercialPriceRangesZar { get; set; }
        }

        public class RootObject
        {
            public int totalNumberOfResults { get; set; }
            public Dimensions dimensions { get; set; }
        }
    }
}
