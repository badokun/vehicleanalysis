using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using log4net;

namespace VehicleStats.Core.Extraction
{
    public class HtmlWebWrapper : IHtmlWebWrapper
    {

        private const int maxRetries = 5;

        private HtmlWeb _htmlWeb;
        private ILog _log;

        public HtmlWebWrapper(ILog log, Encoding encoding) : this(log)
        {
            _htmlWeb.OverrideEncoding = encoding;
        }

        public HtmlWebWrapper(ILog log)
        {
            _htmlWeb = new HtmlWeb();
            _log = log;
        }

        public HtmlDocument Load(string url)
        {
            int retryCount = 0;
            bool loaded = false;
            HtmlDocument returnDocument = new HtmlDocument();
            while (!loaded && retryCount <= maxRetries)
            {
                try
                {
                    _log.DebugFormat("Loading {0}", url);
                    returnDocument = _htmlWeb.Load(url);

                    loaded = true;
                }
                catch (Exception ex)
                {
                    _log.Error("Failed to load the document", ex);
                    _log.DebugFormat("Will retry. {0}/{1}", retryCount, maxRetries);
                    retryCount++;
                }
            }
            return returnDocument;
        }

        public string DownloadString(string url)
        {
            var client = new WebClient();
            return client.DownloadString(url);
        }

        public HtmlDocument Post(string url, NameValueCollection postData)
        {
            _log.DebugFormat("POST to {0}", url);

            var doc = _htmlWeb.SubmitFormValues(postData, url);

            //   http://dbanck.de/2010/01/09/post-data-with-net-html-agility-pack/
            return doc;
        }

    }


}

