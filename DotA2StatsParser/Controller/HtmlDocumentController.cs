using DotA2StatsParser.Controller;
using DotA2StatsParser.Exceptions;
using DotA2StatsParser.Model.Dotabuff;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Controller
{
    internal class HtmlDocumentController
    {
        private MainController mainController;

        private dynamic DotabuffUrlPath { get { return mainController.DotabuffMappingController.UrlPath; } }
        private dynamic YaspUrlPath { get { return mainController.YaspMappingController.UrlPath; } }

        public dynamic HeroAbilitiesPath { get { return mainController.DotabuffMappingController.HeroAbilitiesPath; } }
        public dynamic PlayerMatchesPath { get { return mainController.DotabuffMappingController.PlayerMatchesPath; } }

        internal HtmlDocumentController(MainController mainController)
        {
            this.mainController = mainController;
        }

        internal WebClient CreateWebclient()
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:43.0) Gecko/20100101 Firefox/43.0");
            webClient.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            webClient.Headers.Add("Accept-Language", "de,en-US;q=0.7,en;q=0.3");
            webClient.Encoding = System.Text.Encoding.UTF8;
            return webClient;
        }

        internal HtmlNode TryLoadingDotabuff()
        {
            string dotabuffUrl = DotabuffUrlPath.Main.Value;

            HtmlDocument html = new HtmlDocument();

            try
            {
                using (WebClient webclient = mainController.HtmlDocumentController.CreateWebclient())
                {
                    html.LoadHtml(webclient.DownloadString(dotabuffUrl));
                }
            }
            catch (WebException webEx)
            {
                throw new Dota2StatParserException(dotabuffUrl + " is currently not available", webEx);
            }

            return html.DocumentNode;
        }

        internal HtmlNode TryLoadingYasp()
        {
            string yaspUrl = YaspUrlPath.Main.Value;

            HtmlDocument html = new HtmlDocument();

            try
            {
                using (WebClient webclient = mainController.HtmlDocumentController.CreateWebclient())
                {
                    html.LoadHtml(webclient.DownloadString(yaspUrl));
                }
            }
            catch (WebException webEx)
            {
                throw new Dota2StatParserException(yaspUrl + " is currently not available", webEx);
            }

            return html.DocumentNode;
        }

        internal HtmlNode GetYaspWordCloudRoot(string playerId)
        {
            return LoadDocumentNode(string.Format(YaspUrlPath.Players.WordCloud.Value, playerId));
        }

        internal HtmlNode GetDotabuffItemRoot(string itemReference)
        {
            return LoadDocumentNode(string.Format("{0}{1}", DotabuffUrlPath.Items.URL.Value, itemReference));
        }

        internal HtmlNode GetDotabuffHeroRoot(string heroReference)
        {
            return LoadDocumentNode(string.Format("{0}{1}", DotabuffUrlPath.Heroes.URL.Value, heroReference));
        }

        internal HtmlNode GetDotabuffAbilityRoot(string heroReference)
        {
            return LoadDocumentNode(string.Format(DotabuffUrlPath.Heroes.Abilities.URL.Value, heroReference));
        }

        internal HtmlNode GetDotabuffPlayerMatchesRoot(string playerId, PlayerMatchesOptions playerMatchesOptions)
        {
            return LoadDocumentNode(string.Format(DotabuffUrlPath.Players.Matches.URL.Value + mainController.QueryStringController.GetQueryString(playerMatchesOptions), playerId));
        }

        internal HtmlNode GetDotabuffSteamIdPlayerRoot(string steamId)
        {
            return LoadDocumentNode(string.Format(DotabuffUrlPath.Search.SteamId.Value, steamId));
        }

        internal HtmlNode GetDotabuffPlayerRoot(string playerId)
        {
            return LoadDocumentNode(string.Format("{0}{1}", DotabuffUrlPath.Players.URL.Value, playerId));
        }

        private HtmlNode LoadDocumentNode(string url)
        {
            HtmlDocument html = new HtmlDocument();

            using (WebClient webclient = mainController.HtmlDocumentController.CreateWebclient())
            {
                html.LoadHtml(webclient.DownloadString(url));
            }

            return html.DocumentNode;
        }
    }
}
