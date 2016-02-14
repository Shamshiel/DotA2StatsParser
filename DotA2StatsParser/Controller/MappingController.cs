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
        protected Dictionary<string, string> GetPathsAsDictionary(dynamic path)
        {
            Dictionary<string, string> allPaths = new Dictionary<string, string>();

            FillDictionaryWithPaths(path, string.Empty, allPaths);

            return allPaths;
        }

        protected void FillDictionaryWithPaths(dynamic path, string keyChain, Dictionary<string, string> allPaths)
        {
            Dictionary<string, object> values = path.ToObject<Dictionary<string, object>>();

            foreach (string key in values.Keys)
            {
                if (values[key] is string)
                {
                    // Ignore everything besides XPaths (for example URLs)
                    if (!IsValidXPath(values[key].ToString()))
                        continue;

                    allPaths.Add(keyChain + "/" + key, values[key].ToString());
                }
                else
                {
                    FillDictionaryWithPaths(values[key], keyChain + "/" + key, allPaths);
                }
            }
        }

        protected bool IsValidXPath(string xPathString)
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
