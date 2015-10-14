using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using log4net;
using VehicleStats.Core.Extraction;
using VehicleStats.Core.Statistics;

namespace VehicleStats.Core.GooNet
{
    public class GooNetArgumentBuilder : ArgumentBuilderBase
    {
        private readonly IHtmlWebWrapper _htmlWebWrapper;
        private readonly ILog _log;
        public const string RootUrl = "http://www.goo-net.com/usedcar";

        public GooNetArgumentBuilder(IHtmlWebWrapper htmlWebWrapper, ILog log)
        {
            _log = log;
            _htmlWebWrapper = htmlWebWrapper;
        }


        private IList<string> _makes;

        public override string SourceSystem
        {
            get { return "GooNet jp"; }
        }

        public override IList<string> GetAllMakes()
        {
            if (_makes == null)
            {
                var rootDoc = _htmlWebWrapper.Load(RootUrl);
                var allMakes = rootDoc.DocumentNode.SelectNodes("//input[@name='maker_cd[]']");
                _makes = allMakes.Select(m => ExtractMake(m.NextSibling.Attributes["href"])).Where(s => !string.IsNullOrEmpty(s)).ToList();
            }

            return _makes;
        }

        private IDictionary<string, IList<string>> _models = new Dictionary<string, IList<string>>();
        public override IList<string> GetAllModels(string make)
        {
            if (!_models.ContainsKey(make))
            {
                var modelUrl = string.Format("{0}/{1}/index.html", RootUrl, make);
                var modelDoc = _htmlWebWrapper.Load(modelUrl);
                _models[make] = modelDoc.DocumentNode
                    .SelectNodes("//div[@class='box_roundWhite']/div[@class='list']/p/a")
                    .Select(i => ExtractModel(make, i.Attributes["href"]))
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Distinct()
                    .ToList();
            }
            return _models[make];
        }

        private static string ExtractMake(HtmlAttribute htmlAttribute)
        {
            if (htmlAttribute != null)
                return htmlAttribute.Value.Replace(@"/usedcar/", string.Empty).Replace(@"/index.html", string.Empty).ToUpper();
            return string.Empty;
        }

        private string ExtractModel(string make, HtmlAttribute htmlAttribute)
        {
            if (htmlAttribute != null)
            {
                var stringToReplace = string.Format(@"/usedcar/{0}__", make);
                return htmlAttribute.Value.Replace(stringToReplace, string.Empty).Replace(".html", string.Empty);
            }

            return string.Empty;
        }

    }
}
