using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotA2StatsParser.Model.Dotabuff;
using DotA2StatsParser.Model.Dotabuff.Path;
using HtmlAgilityPack;

namespace DotA2StatsParser.Controller.Dotabuff
{
    internal class MatchController
    {
        private MainController mainController;

        private dynamic PlayerPath { get { return mainController.DotabuffMappingController.PlayerPath; } }
        private dynamic HtmlAttributes { get { return mainController.DotabuffMappingController.HtmlAttributes; } }

        internal MatchController(MainController mainController)
        {
            this.mainController = mainController;
        }

        internal List<Match> FetchLatestMatches(string playerId)
        {
            HtmlNode root = mainController.HtmlDocumentController.GetDotabuffPlayerRoot(playerId);

            IEnumerable<HtmlNode> latestMatches = root.SelectNodes(PlayerPath.LatestMatches.List.Value);

            List<Match> matchList = new List<Match>();

            MatchPath matchPath = new MatchPath
            {
                Duration = PlayerPath.LatestMatches.Duration.Value,
                Hero = PlayerPath.LatestMatches.Hero.Value,
                Id = PlayerPath.LatestMatches.Id.Value,
                Kda = PlayerPath.LatestMatches.Kda.Value,
                Mode = PlayerPath.LatestMatches.Mode.Value,
                Result = PlayerPath.LatestMatches.Result.Value,
                Skillbracket = PlayerPath.LatestMatches.Skillbracket.Value,
                TimeAgo = PlayerPath.LatestMatches.TimeAgo.Value,
                Type = PlayerPath.LatestMatches.Type.Value,
                IdAttribute = HtmlAttributes.Match.Attribute.Value,
                IdReplace = HtmlAttributes.Match.Replace.Value
            };

            for (int i = 1; i < latestMatches.Count() + 1; i++)
            {
                Match match = MapHtmlNodeToMatch(root, matchPath, i);
                matchList.Add(match);
            }

            return matchList;
        }

        internal Match MapHtmlNodeToMatch(HtmlNode root, MatchPath matchPath, int currentCount)
        {
            Match match = new Match();
            match.Id = HtmlEntity.DeEntitize(root.SelectSingleNode(mainController.CombinePathWithListCount(matchPath.Id, currentCount)).Attributes[matchPath.IdAttribute].Value).Replace(matchPath.IdReplace, "");

            HtmlNode heroNode = root.SelectSingleNode(mainController.CombinePathWithListCount(matchPath.Hero, currentCount));
            if (heroNode != null)
            {
                string heroReference = root.SelectSingleNode(mainController.CombinePathWithListCount(matchPath.Hero, currentCount)).Attributes[HtmlAttributes.Hero.Attribute.Value].Value.Replace(HtmlAttributes.Hero.Replace.Value, "");
                match.Hero = mainController.HeroController.GetHero(heroReference);
            }

            match.Result = mainController.MapStringToEnum<Results>(root.SelectSingleNode(mainController.CombinePathWithListCount(matchPath.Result, currentCount)).InnerText);
            match.TimeAgo = DateTime.Parse(root.SelectSingleNode(mainController.CombinePathWithListCount(matchPath.TimeAgo, currentCount)).Attributes[MainController.HTML_ATTRIBUTE_DATETIME].Value);
            match.Type = mainController.MapStringToEnum<Types>(root.SelectSingleNode(mainController.CombinePathWithListCount(matchPath.Type, currentCount)).InnerText);
            match.Mode = mainController.MapStringToEnum<Modes>(root.SelectSingleNode(mainController.CombinePathWithListCount(matchPath.Mode, currentCount)).InnerText);

            HtmlNode skillBracketNode = root.SelectSingleNode(mainController.CombinePathWithListCount(matchPath.Skillbracket, currentCount));
            if (skillBracketNode != null)
                match.Skillbracket = mainController.MapStringToEnum<Skillbrackets>(HtmlEntity.DeEntitize(skillBracketNode.InnerText));

            match.Duration = mainController.ConvertStringToTimeSpan(root.SelectSingleNode(mainController.CombinePathWithListCount(matchPath.Duration, currentCount)).InnerText);
            match.Kda = mainController.ConvertStringToKda(root.SelectSingleNode(mainController.CombinePathWithListCount(matchPath.Kda, currentCount)).InnerText);
            return match;
        }
    }
}
