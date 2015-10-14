using System;
using System.Globalization;
using System.IO;
using System.Linq;
using VehicleStats.Core.AutoTrader;
using VehicleStats.Core.Extraction;
using VehicleStats.Core.GooNet;
using VehicleStats.Core.Statistics;
using log4net;
using VehicleStats.Data;
using VehicleStats.Data.Shared;

namespace VehicleStatsApp
{
    class Program
    {
        private static ILog _log;
        private static IHtmlWebWrapper _htmlWebWrapper;
        private static IExtractRepository _sqlExtractRepository;
        private static GooNetPageScraper _gooNetPageScraper;
        private static IArgumentBuilder _gooNetArgBuilder;
        private static HtmlWebWrapper _japaneseHtmlWebWrapper;
        private static IAutoTraderZaPageScraper _autoTraderScraper;
        private static string _sourceSystem;
        private const string AutoTraderConst = "AutoTrader za";
        private const string GooNetConst = "GooNet jp";

        static void Main(string[] args)
        {
            Init();
            _log.Debug("Starting...");

            Console.WriteLine("SELECT SOURCE");
            Console.WriteLine("1. AutoTrader za");
            Console.WriteLine("2. GooNet jp");
            switch (Console.ReadLine())
            {
                case "1" :
                    _sourceSystem = AutoTraderConst;
                    break;
                case "2" :
                    _sourceSystem = GooNetConst;
                    break;
                default:throw new ArgumentException();
            }

            ExtractDataSequence();
        }

        private static void ExtractDataSequence()
        {
            bool more = true;
            while (more)
            {
                Console.WriteLine("ENTER MAKE");
                var make = Console.ReadLine();
                Console.WriteLine("ENTER MODEL");
                var model = Console.ReadLine();
                
                var from = 2001;
                var to = 2014;

                var mm = _sqlExtractRepository.ReadVehicleMakeModel(_sourceSystem).FirstOrDefault(p => p.Make == make && p.Models.Contains(model));
                if (mm == null)
                {
                    Console.WriteLine("Make and Model does not exist");
                    continue;
                }
                
                IExtractionArguments args = ExtractionArguments.Create(make, model, from, to);
                IExtractionResults results = _sqlExtractRepository.Read(args, _sourceSystem);

                if (results == null)
                {
                    results = new ExtractionResults();
                    IExtractionEngine engine;
                    if (_sourceSystem == GooNetConst)
                        engine = new GooExtractionEngine(_japaneseHtmlWebWrapper, _log, _gooNetPageScraper);
                    else
                    {
                        engine = new AutoTraderExtractionEngine(_htmlWebWrapper, _log, _autoTraderScraper);
                    }

                    engine.Extract(args, results);

                    _sqlExtractRepository.Write(args, results, _sourceSystem);
                }
                
                StatisticsPrinter.Print(StatisticsFactory.GetStatistics(results.Vehicles));
                StatisticsPrinter.SaveToCsv(StatisticsFactory.GetStatistics(results.Vehicles),_sourceSystem, "C:\\temp\\VehicleStatsApp");
                 
                Console.WriteLine("MORE? Y\\N");
                if (Console.ReadLine().ToUpper() == "N")
                    more = false;
            }
        }
        
        private static void Init()
        {
            log4net.Config.XmlConfigurator.Configure();
            _log = LogManager.GetLogger(typeof(Program));
            _htmlWebWrapper = new HtmlWebWrapper(_log);
            _japaneseHtmlWebWrapper = new HtmlWebWrapper(_log, System.Text.Encoding.GetEncoding("EUC-JP"));
            var sqlPath = Path.GetFullPath(@"..\..\..\VehicleStatsData\SQLDatabase.mdf");
            _sqlExtractRepository = RepositoryFactory.GetRepository("SqlExtractRepository", new object[] { _log,  @"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + sqlPath + ";Integrated Security=True" });
            //_fileStatsRepository = RepositoryFactory.GetRepository("FileExtractRepository", new object[] { _log, outputPath });
            //_argBuilder = new AutoTraderArgumentBuilder(_htmlWebWrapper, _log);
            _gooNetArgBuilder = new GooNetArgumentBuilder(_japaneseHtmlWebWrapper, _log);
            _autoTraderScraper = new AutoTraderZaPageScraper(_log);
            _gooNetPageScraper = new GooNetPageScraper();
        }
    }
}