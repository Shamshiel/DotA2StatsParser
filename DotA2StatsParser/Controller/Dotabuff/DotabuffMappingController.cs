using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using DotA2StatsParser.Model;

namespace DotA2StatsParser.Controller.Dotabuff
{
    internal class DotabuffMappingController : MappingController
    {
        private dynamic dotabuffXPaths;
        private dynamic dotabuffQueryStrings;
        private dynamic dotabuffEnums;
        private dynamic dotabuffSelectors;
        private dynamic dotabuffHtmlAttributes;
        private dynamic dotabuffUrls;

        internal dynamic PlayersPath { get { return dotabuffXPaths.Players; } }
        internal dynamic PlayerPath { get { return dotabuffXPaths.Players.Player; } }
        internal dynamic PlayerMatchesPath { get { return dotabuffXPaths.Matches; } }

        internal dynamic HeroesPath { get { return dotabuffXPaths.Heroes; } }
        internal dynamic HeroPath { get { return dotabuffXPaths.Heroes.Hero; } }
        internal dynamic HeroAbilitiesPath { get { return dotabuffXPaths.Abilities; } }

        internal dynamic ItemsPath { get { return dotabuffXPaths.Items; } }
        internal dynamic ItemPath { get { return dotabuffXPaths.Items.Item; } }

        internal dynamic HtmlAttributes { get { return dotabuffHtmlAttributes.Attributes; } }
        internal dynamic Selector { get { return dotabuffSelectors.Selector; } }
        internal dynamic QueryString { get { return dotabuffQueryStrings.QueryStringMapping; } }
        internal dynamic EnumMapping { get { return dotabuffEnums.Enums; } }
        internal dynamic UrlPath { get { return dotabuffUrls.URL; } }

        internal DotabuffMappingController(JsonPaths jsonPaths)
        {
            JsonController jsonReader = new JsonController();

            dotabuffXPaths = jsonReader.ReadFromFile(jsonPaths.XPathsUri);
            dotabuffQueryStrings = jsonReader.ReadFromFile(jsonPaths.QueryStringsUri);
            dotabuffEnums = jsonReader.ReadFromFile(jsonPaths.EnumsUri);
            dotabuffSelectors = jsonReader.ReadFromFile(jsonPaths.SelectorsUri);
            dotabuffHtmlAttributes = jsonReader.ReadFromFile(jsonPaths.HtmlAttributesUri);
            dotabuffUrls = jsonReader.ReadFromFile(jsonPaths.UrlsUri);
        }

        internal Dictionary<string, string> GetPlayerPathsAsDictionary()
        {
            return GetXPathsAsDictionary(PlayerPath);
        }

        internal Dictionary<string, string> GetHeroPathsAsDictionary()
        {
            return GetXPathsAsDictionary(HeroPath);
        }

        internal Dictionary<string, string> GetItemPathsAsDictionary()
        {
            return GetXPathsAsDictionary(ItemPath);
        }
    }
}
