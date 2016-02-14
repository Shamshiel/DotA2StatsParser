using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotA2StatsParser.Model.Dotabuff;
using HtmlAgilityPack;

namespace DotA2StatsParser.Controller.Dotabuff
{
    internal class AbilityController
    {
        private MainController mainController;

        private HtmlDocumentController HtmlDocumentController { get { return mainController.HtmlDocumentController; } }

        private dynamic HeroPath { get { return mainController.DotabuffMappingController.HeroPath; } }
        private dynamic HeroAbilitiesPath { get { return mainController.DotabuffMappingController.HeroAbilitiesPath; } }
        private dynamic UrlPath { get { return mainController.DotabuffMappingController.UrlPath; } }

        internal AbilityController(MainController mainController)
        {
            this.mainController = mainController;
        }

        internal List<Ability> FetchAbilities(string heroReference)
        {
            HtmlNode root = HtmlDocumentController.GetDotabuffAbilityRoot(heroReference);

            IList<HtmlNode> abilitiesNodes = root.SelectNodes(HeroAbilitiesPath.List.Value);
            IList<HtmlNode> referenceNodes = root.SelectNodes(HeroAbilitiesPath.Image.Value);

            List<Ability> abilityList = new List<Ability>();

            int counter = 0;
            foreach (HtmlNode node in abilitiesNodes)
            {
                Ability ability = new Ability();

                HtmlNode headerNode = node.Descendants("header").First();

                ability.Name = headerNode.ChildNodes[0].InnerText;
                if (headerNode.ChildNodes.Count > 1 && !string.IsNullOrEmpty(headerNode.ChildNodes[1].InnerText))
                    ability.Key = Convert.ToChar(headerNode.ChildNodes[1].InnerText);

                byte[] abilityImage =
                    HtmlDocumentController.CreateWebclient()
                        .DownloadData(UrlPath.Main.Value +
                                      referenceNodes[counter].Attributes[MainController.HTML_ATTRIBUTE_SRC].Value);


                ability.Image = abilityImage;

                abilityList.Add(ability);

                counter++;
            }

            abilityList.Add(new Ability() { Name = "Attribute Bonus" });

            return abilityList;
        }

        internal List<AbilityBuild> FetchMostPopularAbilityBuild(string heroReference)
        {
            HtmlNode root = HtmlDocumentController.GetDotabuffHeroRoot(heroReference);

            IList<HtmlNode> mostPopularAbilityBuildNodes = root.SelectNodes(HeroPath.MostPopularAbilitBuild.Abilities.Value);
            IList<HtmlNode> mostPopularAbilityBuildIconNodes = root.SelectNodes(HeroPath.MostPopularAbilitBuild.Icons.Value);

            List<Ability> abilities = FetchAbilities(heroReference);

            List<AbilityBuild> abilityBuildList = new List<AbilityBuild>();

            for (int i = 0; i < mostPopularAbilityBuildNodes.Count(); i++)
            {
                AbilityBuild abilityBuild = new AbilityBuild();

                string abilityName = HtmlEntity.DeEntitize(mostPopularAbilityBuildIconNodes[i].Attributes["alt"].Value);

                abilityBuild.Ability = abilities.First(ability => ability.Name.Contains(abilityName));



                abilityBuild.LevelBuild = new List<int>();
                IEnumerable<HtmlNode> levelBuildNodes =
                    mostPopularAbilityBuildNodes[i].Descendants("div")
                        .Where(
                            d => d.Attributes.Contains("class") && d.Attributes["class"].Value.Contains("entry choice"));


                List<int> abilityLevelBuild = new List<int>();
                foreach (HtmlNode levelBuildNode in levelBuildNodes)
                {
                    abilityLevelBuild.Add(int.Parse(levelBuildNode.InnerText));
                }

                abilityBuild.LevelBuild = abilityLevelBuild;
                abilityBuildList.Add(abilityBuild);
            }

            return abilityBuildList;
        }
    }
}
