using DotA2StatsParser.Exceptions;
using DotA2StatsParser.Model;
using DotA2StatsParser.Model.Dotabuff;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Controller
{
    internal class VersionController
    {
        private JsonController jsonController;

        private const int MAX_RETRIES = 5;

        internal VersionController()
        {
            jsonController = new JsonController();
        }

        internal JsonPaths GetJsonPaths(ParsingWebsites parsingWebsite)
        {
            string currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            return GetLocalJsonPaths(currentVersion, parsingWebsite);
        }

        internal void RefreshJsonPaths()
        {
            dynamic versions = DownloadJsonFile(@"https://raw.githubusercontent.com/Shamshiel/DotA2StatsParser/master/DotA2StatsParser/Mapping/Version.json");

            string currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            foreach (ParsingWebsites parsingWebsite in Enum.GetValues(typeof(ParsingWebsites)))
            {
                JsonPaths jsonPaths = GetLocalJsonPaths(currentVersion, parsingWebsite);

                Dictionary<string, Dictionary<string, string>> allVersionsDictionary = GetAllVersion(versions, parsingWebsite);

                Dictionary<string, string> urlDictioanry = allVersionsDictionary[currentVersion];

                if (urlDictioanry["IsDeprecated"] == "true")
                    throw new Dota2StatParserException("This version was declared deprecated and will not work properly. Please update to a newer version!");

                RefreshJsonFiles(jsonPaths, urlDictioanry);
            }
        }

        private void RefreshJsonFiles(JsonPaths jsonPaths, Dictionary<string, string> urlDictioanry)
        {
            PropertyInfo[] propertyInfos = jsonPaths.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (string key in urlDictioanry.Keys)
            {
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    if (propertyInfo.Name.Contains(key))
                    {
                        string filePath = propertyInfo.GetValue(jsonPaths).ToString();

                        dynamic downloadedJsonObject = DownloadJsonFile(urlDictioanry[key]);
                        downloadedJsonObject.LastDownload = DateTime.Now;

                        RefreshJsonFile(filePath, downloadedJsonObject);
                    }
                }
            }
        }

        private void RefreshJsonFile(string filePath, dynamic downloadedJsonObject)
        {
            string jsonForFile = JsonConvert.SerializeObject(downloadedJsonObject);

            int tries = 0;
            bool completed = false;
            while (!completed)
            {
                try
                {
                    File.WriteAllText(filePath, jsonForFile);
                    completed = true;
                }
                catch
                {
                    tries++;

                    System.Threading.Thread.Sleep(500);

                    if (tries == MAX_RETRIES)
                        throw;
                }
            }
        }

        private dynamic DownloadJsonFile(string uri)
        {
            using (WebClient webClient = new WebClient())
            {
                string versionJson = webClient.DownloadString(uri);
                return jsonController.ReadFromString(versionJson);
            }
        }

        private JsonPaths GetLocalJsonPaths(string version, ParsingWebsites parsingWebsite)
        {
            return new JsonPaths()
            {
                EnumsUri = string.Format(@"Mapping\{0}\{1}\Enums.json", parsingWebsite, version),
                QueryStringsUri = string.Format(@"Mapping\{0}\{1}\QueryStrings.json", parsingWebsite, version),
                HtmlAttributesUri = string.Format(@"Mapping\{0}\{1}\HtmlAttributes.json", parsingWebsite, version),
                SelectorsUri = string.Format(@"Mapping\{0}\{1}\Selectors.json", parsingWebsite, version),
                UrlsUri = string.Format(@"Mapping\{0}\{1}\Urls.json", parsingWebsite, version),
                XPathsUri = string.Format(@"Mapping\{0}\{1}\XPaths.json", parsingWebsite, version)
            };
        }

        private Dictionary<string, Dictionary<string, string>> GetAllVersion(dynamic path, ParsingWebsites parsingWebsite)
        {
            Dictionary<string, Dictionary<string, string>> allVersionsDictionary = new Dictionary<string, Dictionary<string, string>>();

            Dictionary<string, object> values = path.ToObject<Dictionary<string, object>>();

            foreach (string key in values.Keys)
            {
                Dictionary<string, string> urlDictioanry = new Dictionary<string, string>();
                FillDictionaryWithVersions(values[key], urlDictioanry, parsingWebsite);
                allVersionsDictionary.Add(key, urlDictioanry);
            }

            return allVersionsDictionary;
        }

        private void FillDictionaryWithVersions(dynamic path, Dictionary<string, string> urlDictioanry, ParsingWebsites parsingWebsite)
        {
            Dictionary<string, object> values = path.ToObject<Dictionary<string, object>>();

            foreach (string key in values.Keys)
            {
                if (values[key] is string)
                {
                    urlDictioanry.Add(key, values[key].ToString());
                }
                else
                {
                    if (parsingWebsite.ToString() == key)
                    {
                        FillDictionaryWithVersions(values[key], urlDictioanry, parsingWebsite);
                    }
                }
            }
        }
    }
}
