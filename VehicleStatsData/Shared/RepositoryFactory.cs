using System;
using log4net;
using VehicleStats.Data.File;
using VehicleStats.Data.SQL;

namespace VehicleStats.Data.Shared
{
    public static class RepositoryFactory
    {
        public static IExtractRepository GetRepository(string type, object[] constructorArgs)
        {
            IExtractRepository repository = null;
            switch (type)
            {
                case "SqlExtractRepository": repository = new SqlExtractRepository((ILog)constructorArgs[0], (string)constructorArgs[1]);
                    break;
                //case "FileExtractRepository": repository = new FileExtractRepository((ILog)constructorArgs[0], (string)constructorArgs[1]);
                //    break;

                default: throw new ArgumentException("Could not resolve type " + type);
            }

            return repository;
        }
    }
}
