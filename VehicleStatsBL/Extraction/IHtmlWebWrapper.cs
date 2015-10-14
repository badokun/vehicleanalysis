using System.Collections.Specialized;
using HtmlAgilityPack;

namespace VehicleStats.Core.Extraction
{
    public interface IHtmlWebWrapper
    {
        HtmlDocument Load(string url);
        string DownloadString(string url);
        HtmlDocument Post(string url, NameValueCollection page);
    }
    
}
