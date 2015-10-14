using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using log4net;
using VehicleStats.Core.Extraction;
using VehicleStats.Core.Statistics;
using VehicleStats.Data.Shared;

namespace VehicleStats.Data.File
{
    //public class FileExtractRepository : IExtractRepository
    //{
    //    private readonly ILog _log;
    //    private readonly string _rootPath;
    //    public FileExtractRepository(ILog log, string rootPath)
    //    {
    //        _log = log;
    //        _rootPath = rootPath;
    //    }

    //    public void Write(IExtractionArguments arguments, IExtractionResults extractionResults)
    //    {
    //        _log.DebugFormat("Writing extractionResults for {0}", extractionResults);
    //        var filePath = GetFilePath(arguments);
    //        if (System.IO.File.Exists(filePath))
    //            System.IO.File.Delete(filePath);

    //        using (var fs = new FileStream(filePath, FileMode.Create))
    //        {
    //            var formatter = new BinaryFormatter();
    //            try
    //            {
    //                formatter.Serialize(fs, extractionResults);
    //                _log.Debug("Results streamed to " + filePath);
    //            }
    //            catch (SerializationException e)
    //            {
    //                _log.Error("Failed to serialize. Reason: " + e.Message);
    //                throw;
    //            }
    //        }
    //    }

    //    public IExtractionResults Read(IExtractionArguments arguments)
    //    {
    //        //_log.DebugFormat("Reading " + arguments);
    //        var filePath = GetFilePath(arguments);
    //        if (!System.IO.File.Exists(filePath))
    //            throw new FileNotFoundException();

    //        using (FileStream fs = new FileStream(filePath, FileMode.Open))
    //        {
    //            BinaryFormatter formatter = new BinaryFormatter();
    //            try
    //            {
    //                return (IExtractionResults)formatter.Deserialize(fs);
    //            }
    //            catch (SerializationException e)
    //            {
    //                _log.Error("Failed to deserialize. Reason: " + e.Message);
    //                throw;
    //            }
    //        }
    //    }

    //    public void Write(IExtractionArguments arguments, IExtractionResults extractionResults, string sourceSystem)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public IExtractionResults Read(IExtractionArguments arguments, string sourceSystem)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public bool TryRead(IExtractionArguments arguments, out IExtractionResults results)
    //    {
    //        results = null;
    //        var filePath = GetFilePath(arguments);
    //        if (!System.IO.File.Exists(filePath))
    //            return false;

    //        results = Read(arguments);
    //        return true;
    //    }

    //    public void WriteVehicleMakeModel(IList<VehicleMakeModel> allMakesModels, string sourceSystem)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public List<VehicleMakeModel> ReadVehicleMakeModel(string sourceSystem)
    //    {
    //        throw new System.NotImplementedException();
    //    }


    //    public string GetFilePath(IExtractionArguments arguments)
    //    {
    //        return string.Format("{0}\\{1}[{2}] {3}_{4}.dat", _rootPath, arguments.Make, arguments.Model, arguments.From, arguments.To);
    //    }

    //}
}