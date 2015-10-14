using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using VehicleStats.Core.Extraction;

namespace VehicleStats.Core.GooNet
{
    public class GooNetPageScraper 
    {
        public IList<IVehicle> Scrape(IExtractionArguments args, HtmlDocument page)
        {
            IList<IVehicle> vehicles = new List<IVehicle>();

            var nodes = page.DocumentNode.SelectNodes("//div[@class='box_item_detail section ']");
            var gooNodes = page.DocumentNode.SelectNodes("//div[@class='box_item_detail section no_goo_area']");

            if (nodes == null)
                nodes = gooNodes;
            else if (gooNodes != null)
            {
                foreach (var selectNode in gooNodes)
                {
                    nodes.Add(selectNode);
                }
            }

            foreach (var node in nodes)
            {
                Vehicle vehicle = new Vehicle();
                vehicle.Title =
                    node.SelectNodes("div/div[@class='heading_inner']")
                        .First()
                        .InnerText.Trim()
                        .Replace("&nbsp;", " ")
                        .Replace("\t", string.Empty)
                        .Replace("\n", string.Empty);
                vehicle.Make = args.Make;
                vehicle.Model = args.Model;
                vehicle.Milage = node.SelectNodes("div/div/table/tr/td[@class='w63']").First().InnerText;

                double price = 0;
                double.TryParse(node.SelectNodes("div/div/table/tr/td/div[@class='priceInfo']/p/em").First().InnerText,
                  out price);
                vehicle.Price = price;
                
                var dirtyYear = node.SelectNodes("div/div/table/tr/td[@class='w66']")[0].InnerText;
                dirtyYear = dirtyYear.Substring(0, dirtyYear.IndexOf("("));

                vehicle.Year = int.Parse(dirtyYear);
                vehicles.Add(vehicle);
            }

            return vehicles;
        }

       

        public Uri GetFirstPageUrl(IExtractionArguments extractionArgs)
        {
            //http://www.goo-net.com/usedcar/LEXUS__IS_F.html
            var uri = string.Format("{0}/{1}__{2}.html", GooNetArgumentBuilder.RootUrl, extractionArgs.Make,
                extractionArgs.Model);

            return new Uri(uri);
        }

        public int GetPageCount(HtmlDocument firstPage)
        {
            var nodeCollection = firstPage.DocumentNode.SelectNodes("//div[@class='page_ctrl']/ul/li");
            var elementCount = nodeCollection.Count-2;
            var emptyNode = nodeCollection.FirstOrDefault(e => e.InnerText == string.Empty);
            if (emptyNode == null)
                emptyNode = nodeCollection.FirstOrDefault(e => e.InnerText.Contains("次へ"));

            var emptyNodeIndex = nodeCollection.IndexOf(emptyNode);

            return int.Parse(nodeCollection[emptyNodeIndex-1].InnerText);
        }
    }
}
