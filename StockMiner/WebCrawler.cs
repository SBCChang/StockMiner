using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace StockMiner
{
    public class WebCrawler
    {

        private static HttpClient _client = new HttpClient();

        public WebCrawler(string url, string xPath)
        {
            Url = url;
            XPath = XPath;
        }

        public string Url { get; set; }

        public string XPath { get; set; }

        public async Task<HtmlNode> GetSingleNode()
        {
            var document = await GetHtmlDocument();
            return document.DocumentNode.SelectSingleNode(XPath);
        }

        public async Task<HtmlNodeCollection> GetMultiNodes()
        {
            var document = await GetHtmlDocument();
            return document.DocumentNode.SelectNodes(XPath);
        }

        private async Task<HtmlDocument> GetHtmlDocument()
        {
            var result = new HtmlDocument();
            var stream = await _client.GetStreamAsync(Url);
            var manager = ServicePointManager.FindServicePoint(new Uri(new Uri(Url).GetLeftPart(UriPartial.Authority)));
            manager.ConnectionLeaseTimeout = 5 * 60 * 1000;

            result.Load(stream, Encoding.Default);
            return result;
        }

    }
}
