using DotA2StatsParser.Model.Yasp;
using DotA2StatsParser.Model.Yasp.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotA2StatsParser.Controller.Yasp
{
    internal class WordCloudController
    {
        private MainController mainController;

        internal WordCloudController(MainController mainController)
        {
            this.mainController = mainController;
        }

        internal IEnumerable<IWordCloud> GetPlayerWordCloud(string playerId)
        {
            HtmlNode wordCloudNode = mainController.HtmlDocumentController.GetYaspWordCloudRoot(playerId);

            var result = Regex.Split(wordCloudNode.InnerHtml, "\r\n|\r|\n").ToList();

            string wordCountString = result.SingleOrDefault(r => r.Contains("var my_counts"));
            wordCountString = wordCountString.Remove(0, 16);
            wordCountString = wordCountString.Remove(wordCountString.Length - 1, 1);

            JsonController jsonController = new JsonController();
            dynamic wordCount = jsonController.ReadFromString(wordCountString);

            Dictionary<string, object> wordCountDictionary = wordCount.ToObject<Dictionary<string, object>>();

            List<IWordCloud> wordCountList = new List<IWordCloud>();
            foreach (string key in wordCountDictionary.Keys)
            {
                wordCountList.Add(new WordCloud() { Word = key, Count = int.Parse(wordCountDictionary[key].ToString()) });
            }

            return wordCountList.OrderByDescending(w => w.Count).ThenBy(w => w.Word);
        }
    }
}
