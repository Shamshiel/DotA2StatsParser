using DotA2StatsParser.Controller;
using DotA2StatsParser.Model.Dotabuff;
using DotA2StatsParser.Model.Dotabuff.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Controller.Dotabuff
{
    internal class HeroController
    {
        private MainController mainController;

        private AttributeController attributeController;
        private LanePresenceController lanePresenceController;
        private StatController statController;
        private VersusController versusController;
        private AbilityController abilityController;

        private List<Hero> mappedHeros;
        private HtmlDocumentController HtmlDocumentController { get { return mainController.HtmlDocumentController; } }
        private dynamic PlayerPath { get { return mainController.DotabuffMappingController.PlayerPath; } }
        private dynamic HeroPath { get { return mainController.DotabuffMappingController.HeroPath; } }
        private dynamic HeroesPath { get { return mainController.DotabuffMappingController.HeroesPath; } }
        private dynamic UrlPath { get { return mainController.DotabuffMappingController.UrlPath; } }
        private dynamic HtmlAttributes { get { return mainController.DotabuffMappingController.HtmlAttributes; } }

        public HeroController(MainController mainController)
        {
            this.mainController = mainController;

            this.attributeController = new AttributeController(mainController);
            this.lanePresenceController = new LanePresenceController(mainController);
            this.statController = new StatController(mainController);
            this.versusController = new VersusController(mainController);
            this.abilityController = new AbilityController(mainController);

            this.mappedHeros = new List<Hero>();

            LoadAllHeroes();
        }

        private void LoadAllHeroes()
        {
           dynamic allHerosDynamic = mainController.DotabuffMappingController.EnumMapping.Heroes;

           Dictionary<string, object> allHerosDictionary = allHerosDynamic.ToObject<Dictionary<string, object>>();

           foreach (dynamic hero in allHerosDictionary.Keys)
           {
               Dictionary<string, object> heroDictionary = allHerosDictionary[hero].ToObject<Dictionary<string, object>>();

               Hero heroForList = new Hero();
               foreach (string key in heroDictionary.Keys)
               {
                   switch (key)
                   {
                       case MainController.MAPPING_DOTABUFF: heroForList.Reference = heroDictionary[key].ToString(); break;
                       case MainController.MAPPING_PARSER: heroForList.HeroEnum = mainController.MapStringToEnum<Heroes>(heroDictionary[key].ToString()); break;
                       case MainController.MAPPING_NAME: heroForList.Name = heroDictionary[key].ToString(); break;
                   }
               }
               mappedHeros.Add(heroForList);
           }
        }

        public Hero GetExtendedHero(Heroes heroEnum)
        {
            Hero hero = mappedHeros.Find(h => h.HeroEnum == heroEnum);

            if (hero.WinRate == null || hero.Popularity == null || hero.Image == null)
            {
                HtmlNode root = HtmlDocumentController.GetDotabuffHeroRoot(hero.Reference);

                hero.WinRate = mainController.ConvertStringToWinRate(root.SelectSingleNode(HeroPath.General.WinRate.Value).InnerText);
                hero.Popularity = mainController.ConvertStringToPopularity(root.SelectSingleNode(HeroPath.General.Popularity.Value).InnerText);
                
                string url = UrlPath.Main + root.SelectSingleNode(HeroPath.General.Image.Value).Attributes[MainController.HTML_ATTRIBUTE_SRC].Value;
                using (WebClient webclient = HtmlDocumentController.CreateWebclient())
                {
                    hero.Image = webclient.DownloadData(url);
                }
            }

            if (hero.Abilities == null)
                hero.Abilities = abilityController.FetchAbilities(hero.Reference);

            if (hero.MostPopularAbilityBuild == null)
                hero.MostPopularAbilityBuild = abilityController.FetchMostPopularAbilityBuild(hero.Reference);

            if (hero.Roles == null)
                hero.Roles = FetchRoles(hero.Reference);

            if (hero.Attributes == null)
                hero.Attributes = attributeController.FetchAttributes(hero.Reference);

            if (hero.LanePresence == null)
                hero.LanePresence = lanePresenceController.FetchLanePresence(hero.Reference);

            if (hero.BestVersus == null)
                hero.BestVersus = AttachHeroesToVersus(versusController.FetchBestVersus(hero.Reference), hero.Reference);

            if (hero.WorstVersus == null)
                hero.WorstVersus = AttachHeroesToVersus(versusController.FetchWorstVersus(hero.Reference),
                    hero.Reference);

            if (hero.MostUsedItems == null)
                hero.MostUsedItems = FetchMostUsedItems(hero.Reference);

            return hero;
        }

        private List<Item> FetchMostUsedItems(string heroReference)
        {
            HtmlNode root = HtmlDocumentController.GetDotabuffHeroRoot(heroReference);

            IEnumerable<HtmlNode> mostUsedItemNodes = root.SelectNodes(HeroPath.MostUsedItems.List.Value);

            List<Item> mostUsedItems = new List<Item>();

            foreach (HtmlNode node in mostUsedItemNodes)
            {
                string itemReference = HtmlEntity.DeEntitize(node.Attributes[HtmlAttributes.MostUsedItem.Attribute.Value].Value).Replace(HtmlAttributes.MostUsedItem.Replace.Value, "");
                mostUsedItems.Add(mainController.ItemController.GetItem(itemReference));
            }

            return mostUsedItems;
        }

        private List<Roles> FetchRoles(string heroReference)
        {
            HtmlNode root = HtmlDocumentController.GetDotabuffHeroRoot(heroReference);

            string roles = root.SelectSingleNode(HeroPath.General.Roles.Value).InnerText;
            string[] rolesSplit = roles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            List<Roles> roleList = new List<Roles>();
            foreach (string role in rolesSplit)
            {
                roleList.Add(mainController.MapStringToEnum<Roles>(role));
            }

            return roleList;
        }

        private IEnumerable<Versus> AttachHeroesToVersus(IEnumerable<Versus> versus, string heroReference)
        {
            foreach (Versus v in versus)
                v.Hero = GetHero(v.Hero.Reference);

            return versus;
        }

        public string GetHeroReference(Heroes heroEnum)
        {
            if (mappedHeros.Any(h => h.HeroEnum == heroEnum))
                return mappedHeros.Find(h => h.HeroEnum == heroEnum).Reference;

            throw new Exception(string.Format("Couldn't find Hero: {0}", heroEnum));
        }

        private Hero GetHero(Heroes heroEnum)
        {
            if (mappedHeros.Any(h => h.HeroEnum == heroEnum))
                return mappedHeros.Find(h => h.HeroEnum == heroEnum);

            throw new Exception(string.Format("Couldn't find Hero: {0}", heroEnum));
        }

        public Hero GetHero(string heroReference)
        {
            if (mappedHeros.Any(h => h.Reference == heroReference))
                return mappedHeros.Find(h => h.Reference == heroReference);

            throw new Exception(string.Format("Couldn't find Hero: {0}", heroReference));
        }

        public Dictionary<IHero, IStat> FetchMostPlayedHeroes(string playerId)
        {
            HtmlNode root = mainController.HtmlDocumentController.GetDotabuffPlayerRoot(playerId);

            IEnumerable<HtmlNode> mostPlayedHeroesNode = root.SelectNodes(PlayerPath.MostPlayedHeroes.List.Value);

            Dictionary<IHero, IStat> mostPlayedHeros = new Dictionary<IHero, IStat>();

            for (int i = 1; i < mostPlayedHeroesNode.Count() + 1; i++)
            {
                string heroReference =
                    HtmlEntity.DeEntitize(
                        root.SelectSingleNode(mainController.CombinePathWithListCount(PlayerPath.MostPlayedHeroes.Hero.Value, i))
                            .Attributes[HtmlAttributes.LastPlayedMatches.ReferenceAttribute.Value].Value).Replace(HtmlAttributes.Hero.Replace.Value, "");
                Hero hero = GetHero(heroReference);
                Stat stat = statController.MapHtmlNode(root, i);
                mostPlayedHeros.Add(hero, stat);
            }

            return mostPlayedHeros;
        }
    }
}
