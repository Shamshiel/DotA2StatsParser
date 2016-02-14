using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using DotA2StatsParser.Model;

namespace DotA2StatsParser.Controller.Yasp
{
    internal class YaspMappingController : MappingController
    {
        private dynamic dotabuffUrls;

        internal dynamic UrlPath { get { return dotabuffUrls.URL; } }

        internal YaspMappingController(JsonPaths jsonPaths)
        {
            JsonController jsonReader = new JsonController();

            dotabuffUrls = jsonReader.ReadFromFile(jsonPaths.UrlsUri);
        }
    }
}
