using DotA2StatsParser.Controller;
using DotA2StatsParser.Model.Dotabuff;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Controller.Dotabuff
{
    internal class VersusController
    {
        private MainController mainController;

        private dynamic HeroPath { get { return mainController.DotabuffMappingController.HeroPath; } }
        private dynamic HtmlAttributes { get { return mainController.DotabuffMappingController.HtmlAttributes; } }

        internal VersusController(MainController mainController)
        {
            this.mainController = mainController;
        }

        internal List<Versus> FetchBestVersus(string heroReference)
        {
            return FetchVersus(heroReference, HeroPath.BestVersus.Value);
        }

        internal List<Versus> FetchWorstVersus(string heroReference)
        {
            return FetchVersus(heroReference, HeroPath.WorstVersus.Value);
        }

        private List<Versus> FetchVersus(string heroReference, string path)
        {
            HtmlNode root = mainController.HtmlDocumentController.GetDotabuffHeroRoot(heroReference);

            IEnumerable<HtmlNode> heroesTableNodes = root.SelectSingleNode(path).ChildNodes;
            List<HtmlNode> heroesTableList = heroesTableNodes.ToList();

            List<Versus> versusList = new List<Versus>();

            for (int i = 1; i < heroesTableList.Count + 1; i++)
            {
                HtmlNode node = heroesTableList.ToList()[i - 1];

                Versus versus = new Versus();
                versus.Hero = new Hero { Reference = node.Descendants(MainController.HTML_A_TAG).First().Attributes[MainController.HTML_ATTRIBUTE_HREF].Value.Replace(HtmlAttributes.Hero.Replace.Value, "") };
                versus.Advantage = node.ChildNodes[2].InnerText;
                versus.WinRate = node.ChildNodes[3].InnerText;
                versus.Matches = int.Parse(node.ChildNodes[4].InnerText.Replace(",", ""));

                versusList.Add(versus);
            }

            return versusList;
        }
    }
}
