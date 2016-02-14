using DotA2StatsParser.Model.Dotabuff;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DotA2StatsParser.Controller
{
    internal class QueryStringController
    {
        private MainController mainController;

        internal QueryStringController(MainController mainController)
        {
            this.mainController = mainController;
        }

        /// <summary>
        /// Gets the query string.
        /// </summary>
        /// <param name="queryStringableInstance">A query stringable instance.</param>
        /// <returns>Returns the query string of the query stringable instance</returns>
        internal string GetQueryString(IQueryStringable queryStringableInstance)
        {
            Dictionary<string, string>  queryStringMappingDictionary = GetMappingDictionary(mainController.DotabuffMappingController.QueryString);
            Dictionary<string, string> enumMappingDictionary = GetMappingDictionary(mainController.DotabuffMappingController.EnumMapping);

            NameValueCollection nameValueCollection = queryStringableInstance.CreateNameValueCollection();
            NameValueCollection changedNameValueCollection = new NameValueCollection();

            foreach (string key in nameValueCollection.Keys)
            {
                if (!queryStringMappingDictionary.ContainsKey(key)) continue;

                string value = nameValueCollection[key];
                string mappingKey = queryStringMappingDictionary[key];

                if (CheckIfEnumExists<Heroes>(value))
                    changedNameValueCollection.Add(mappingKey, mainController.HeroController.GetHeroReference(mainController.MapStringToEnum<Heroes>(value)));
                else if (CheckIfEnumExists<Skillbrackets>(value))
                    changedNameValueCollection.Add(mappingKey, enumMappingDictionary[mainController.MapStringToEnum<Skillbrackets>(value).ToString()]);
                else if (CheckIfEnumExists<Types>(value))
                    changedNameValueCollection.Add(mappingKey, enumMappingDictionary[mainController.MapStringToEnum<Types>(value).ToString()]);
                else if (CheckIfEnumExists<Modes>(value))
                    changedNameValueCollection.Add(mappingKey, enumMappingDictionary[mainController.MapStringToEnum<Modes>(value).ToString()]);
                else if (CheckIfEnumExists<Dates>(value))
                    changedNameValueCollection.Add(mappingKey, enumMappingDictionary[mainController.MapStringToEnum<Dates>(value).ToString()]);
                else if (CheckIfEnumExists<Factions>(value))
                    changedNameValueCollection.Add(mappingKey, enumMappingDictionary[mainController.MapStringToEnum<Factions>(value).ToString()]);
                else if (CheckIfEnumExists<Regions>(value))
                    changedNameValueCollection.Add(mappingKey, enumMappingDictionary[mainController.MapStringToEnum<Regions>(value).ToString()]);
                else if (CheckIfEnumExists<Durations>(value))
                    changedNameValueCollection.Add(mappingKey, enumMappingDictionary[mainController.MapStringToEnum<Durations>(value).ToString()]);
            }

            return CreateQueryString(changedNameValueCollection);
        }

        private bool CheckIfEnumExists<T>(string value) where T : struct, IConvertible
        {
            return Convert.ToInt32(mainController.MapStringToEnum<T>(value)) != 0;
        }

        private string CreateQueryString(NameValueCollection nvc)
        {
            var array = (from key in nvc.AllKeys
                         from value in nvc.GetValues(key).Where(v => !v.Contains(MainController.UNDEFINED))
                         select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value)))
                .ToArray();
            return "?" + string.Join("&", array);
        }

        private Dictionary<string, string> GetMappingDictionary(dynamic path)
        {
            Dictionary<string, string> allMappingsDictionary = new Dictionary<string, string>();

            FillDictionaryWithMapping(path, allMappingsDictionary);

            return allMappingsDictionary;
        }

        private void FillDictionaryWithMapping(dynamic path, Dictionary<string, string> allMappingsDictionary)
        {
            Dictionary<string, object> values = path.ToObject<Dictionary<string, object>>();

            foreach (string key in values.Keys)
            {
                if (values.ContainsKey(MainController.MAPPING_DOTABUFF) && values.ContainsKey(MainController.MAPPING_PARSER))
                {
                    allMappingsDictionary.Add(values[MainController.MAPPING_PARSER].ToString(), values[MainController.MAPPING_DOTABUFF].ToString());

                    return;
                }
                else
                {
                    FillDictionaryWithMapping(values[key], allMappingsDictionary);
                }
            }
        }
    }
}
