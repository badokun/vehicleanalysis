using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using log4net;
using VehicleStats.Core.Extraction;
using VehicleStats.Core.Statistics;
using VehicleStats.Data.Shared;

namespace VehicleStats.Data.SQL
{
    public class SqlExtractRepository : IExtractRepository
    {
        private readonly ILog _log;
        private SqlDCDataContext _dataContext;
        public SqlExtractRepository(ILog log, string connectionString)
        {
            _log = log;
            _dataContext = new SqlDCDataContext(connectionString);
        }

        public void Write(IExtractionArguments arguments, IExtractionResults extractionResults, string sourceSystem)
        {
            _log.DebugFormat("Writing extractionResults for {0}", extractionResults);

            var sourceSystemEntity = _dataContext.SourceSystems.FirstOrDefault(p => p.Name == sourceSystem);
            if (sourceSystemEntity == null)
                throw new ArgumentException("SourceSystem does not exist in the database", "sourceSystem");

            var makeEntity = _dataContext.Makes.FirstOrDefault(p => p.MakeName == arguments.Make && p.SourceSystem == sourceSystemEntity);
            if (makeEntity == null)
                throw new ArgumentException("Make does not exist in the database", "arguments");

            var modelEntity = makeEntity.Models.FirstOrDefault(p => p.ModelName == arguments.Model);
            if (modelEntity == null)
                throw new ArgumentException("Model does not exist in the database", "arguments");

            var resultEntity = new ExtractionResult() { Model = modelEntity, From = arguments.From, To = arguments.To, ExtractionDateTime = DateTime.Now };

            _dataContext.ExtractionResults.InsertOnSubmit(resultEntity);
            _dataContext.SubmitChanges();

            foreach (var vehicle in extractionResults.Vehicles)
            {
                _dataContext.Vehicles.InsertOnSubmit(new Vehicle()
                {
                    ExtractionResult = resultEntity,
                    Milage = SafeInt( vehicle.Milage),
                    Year = vehicle.Year,
                    Price = Convert.ToDecimal( vehicle.Price),
                    Title = vehicle.Title
                });
            }
        
            _dataContext.SubmitChanges();
            
        }

        private int SafeInt(string milage)
        {
            int m = 0;
            if (milage.Contains("万km"))
            {
                var doubleMilage = Convert.ToDouble(milage.Replace("万km", string.Empty)) * 10000;
                m = Convert.ToInt32(doubleMilage);
            }
            else
            { int.TryParse(milage, out m); }
            return m;
        }

        public IExtractionResults Read(IExtractionArguments arguments, string sourceSystem)
        {
            IExtractionResults results = new ExtractionResults();
            
            var matchingExtractionResult = _dataContext.ExtractionResultsViews.Where(sqlResult => sqlResult.MakeName == arguments.Make &&
                sqlResult.ModelName == arguments.Model &&
                sqlResult.From == arguments.From &&
                sqlResult.To == arguments.To &&
                sqlResult.SourceSystem == sourceSystem);

            if (!matchingExtractionResult.Any()) return null;
            var mostRecentExtractionResult = matchingExtractionResult.OrderByDescending(p => p.Id).First();


            var vehicles = _dataContext.Vehicles.Where(p => p.ExtractionResults_FK == mostRecentExtractionResult.Id);
            foreach (var vehicle in vehicles)
            {
                results.Vehicles.Add(new Core.Extraction.Vehicle()
                {
                    Make = mostRecentExtractionResult.MakeName,
                    Model = mostRecentExtractionResult.ModelName,
                    Milage = vehicle.Milage.ToString(),
                    Price = Convert.ToDouble(vehicle.Price),
                    Title = vehicle.Title,
                    Year = vehicle.Year
                });
            }

            return results;
        }


        public bool TryRead(IExtractionArguments arguments, out IExtractionResults results)
        {
            throw new NotImplementedException();
        }

        public void WriteVehicleMakeModel(IList<VehicleMakeModel> allMakesModels, string sourceSystem)
        {
            _log.DebugFormat("Writing all VehicleMakeModels to SQL");
            var sourceSystemEntity = _dataContext.SourceSystems.FirstOrDefault(p => p.Name == sourceSystem);
            if (sourceSystemEntity == null)
                throw new ArgumentException("SourceSystem does not exist in the database", "sourceSystem");

            var insertDateTime = DateTime.Now;

            foreach (var modelDictionary in allMakesModels)
            {
                var make = _dataContext.Makes.FirstOrDefault(p => p.MakeName == modelDictionary.Make);
                if (make == null)
                {
                    _dataContext.Makes.InsertOnSubmit(new Make()
                    {
                        MakeName = modelDictionary.Make,
                        InsertDate = insertDateTime,
                        SourceSystemId = sourceSystemEntity.Id
                    });

                    _dataContext.Makes.InsertOnSubmit(make);
                    _dataContext.SubmitChanges();
                }
                
                foreach (var model in modelDictionary.Models)
                {
                    _dataContext.Models.InsertOnSubmit(new Model() { ModelName = model, Make = make, InsertDate = insertDateTime });
                    _dataContext.SubmitChanges();
                }
            }
        }

        public List<VehicleMakeModel> ReadVehicleMakeModel(string sourceSystem)
        {
            _log.DebugFormat("Reading VehicleMakeModel from SQL");

            var makeModelDictionaryList = new List<VehicleMakeModel>();
            var mm = _dataContext.MakeModelViews.Where(vm => vm.SourceSystem == sourceSystem);
            foreach (var makeModelView in mm)
            {
                var vmm = makeModelDictionaryList.FirstOrDefault(p => p.Make == makeModelView.MakeName) ?? new VehicleMakeModel(makeModelView.MakeName);
                vmm.Models.Add(makeModelView.ModelName);
                makeModelDictionaryList.Add(vmm);
            }
            
            return makeModelDictionaryList;
        }
    }
}
