using DotA2StatsParser.Model.Dotabuff;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Controller
{
    internal class JsonController
    {
        internal dynamic ReadFromFile(string filePath)
        {
            string jsonString = System.IO.File.ReadAllText(filePath);

            return ReadFromString(jsonString);
        }

        internal dynamic ReadFromString(string jsonString)
        {
            dynamic dynamicObject = JsonConvert.DeserializeObject(jsonString);

            return dynamicObject;
        }
    }
}
