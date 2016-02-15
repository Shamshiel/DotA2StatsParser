using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace DotA2StatsParser.Controller
{
    internal class MappingController
    {
        private enum MappingOptions
        {
            All,
            OnlyXPath
        };

        protected Dictionary<string, string> GetXPathsAsDictionary(dynamic path)
        {
            Dictionary<string, string> allPaths = new Dictionary<string, string>();

            FillDictionaryWithMappings(path, string.Empty, allPaths, MappingOptions.OnlyXPath);

            return allPaths;
        }

        protected Dictionary<string, string> GetMappingAsDictionary(dynamic path)
        {
            Dictionary<string, string> allPaths = new Dictionary<string, string>();

            FillDictionaryWithMappings(path, string.Empty, allPaths, MappingOptions.All);

            return allPaths;
        }


        private void FillDictionaryWithMappings(dynamic path, string keyChain, Dictionary<string, string> allPaths, MappingOptions mappingOptions)
        {
            Dictionary<string, object> values = path.ToObject<Dictionary<string, object>>();

            foreach (string key in values.Keys)
            {
                if (values[key] is string)
                {
                    // Ignore everything besides XPaths (for example URLs)
                    if (!IsValidXPath(values[key].ToString()) && mappingOptions == MappingOptions.OnlyXPath)
                        continue;

                    allPaths.Add(keyChain + "/" + key, values[key].ToString());
                }
                else
                {
                    FillDictionaryWithMappings(values[key], keyChain + "/" + key, allPaths, mappingOptions);
                }
            }
        }

        private bool IsValidXPath(string xPathString)
        {
            XmlDocument doc = new XmlDocument();
            XPathNavigator nav = doc.CreateNavigator();
            try
            {
                XPathExpression expr = nav.Compile(xPathString);
                return true;
            }
            catch (XPathException)
            {
                return false;
            }
        }
    }
}
