namespace VehicleStats.Core.Extraction
{
    public interface IVehicle
    {
        string Title { get; set; }
        int Year { get; set; }
        string Make { get; set; }
        string Milage { get; set; }
        string Model { get; set; }
        double Price { get; set; }
    }
}
