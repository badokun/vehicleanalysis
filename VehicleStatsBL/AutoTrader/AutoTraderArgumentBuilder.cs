using System.Collections.Generic;
using System.Linq;
using log4net;
using Newtonsoft.Json;
using VehicleStats.Core.Extraction;
using VehicleStats.Core.Statistics;

namespace VehicleStats.Core.AutoTrader
{
    public class AutoTraderArgumentBuilder : ArgumentBuilderBase
    {
        private readonly IHtmlWebWrapper _htmlWebWrapper;
        private readonly ILog _log;
        private const string RootUrl = "http://www.autotrader.co.za/";

        public AutoTraderArgumentBuilder(IHtmlWebWrapper htmlWebWrapper, ILog log)
        {
            _htmlWebWrapper = htmlWebWrapper;
            _log = log;
        }

        public override string SourceSystem
        {
            get { return "AutoTrader za"; }
        }

        public override IList<string> GetAllMakes()
        {
            var rootDoc = _htmlWebWrapper.Load(RootUrl);
            if (rootDoc.DocumentNode.HasChildNodes)
            {
                var makes = rootDoc.DocumentNode.SelectNodes("//select[@name='Make']/option")
                    .Where(make => make.Attributes["value"].Value != string.Empty)
                    .Select(make => make.Attributes["value"].Value.Replace(" ", "-").ToLower())
                    .ToList();

                _log.DebugFormat("Getting all makes returned {0} makes", makes.Count);
                return makes;
            }
            else
            {
                throw new IpBlockedException();
            }
        }

        public override IList<string> GetAllModels(string make)
        {
            var makeRequestUrl = string.Format("{0}refresh-quick-search/makemodel/make/{1}?", RootUrl, make);
            var json = _htmlWebWrapper.DownloadString(makeRequestUrl);
            var models = JsonConvert.DeserializeObject<AutoTraderResponse.RootObject>(json).dimensions.Model.Select(model => model.seoName).ToList();
            
            _log.DebugFormat("Getting all models for {0} returned {1} models", make, models.Count);
            return models;

        }
       
    }
}
