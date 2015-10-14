namespace VehicleStats.Core.Extraction
{
    public interface IExtractionEngine
    {
        void Extract(IExtractionArguments args, IExtractionResults extractionResults);
    }
}