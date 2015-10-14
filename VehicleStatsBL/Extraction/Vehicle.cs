using System;

namespace VehicleStats.Core.Extraction
{
    [Serializable]
    public class Vehicle : IVehicle
    {
        public string Title { get; set; }

        public double Price { get; set; }
        
        public int Year { get; set; }

        public string Milage { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public override string ToString()
        {
            return string.Format("Title {0} \nMake {4} \nModel {5} \nPrice {1} \nYear {2} \nMilage {3}", Title, Price, Year, Milage, Make, Model);
        }

        
    }
}
