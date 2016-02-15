using DotA2StatsParser.Controller;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DotA2StatsParser.Exceptions;
using DotA2StatsParser.Model.Dotabuff;
using DotA2StatsParser.Model.Dotabuff.Interfaces;
using DotA2StatsParser.Model.Dotabuff.Path;

namespace DotA2StatsParser.Controller.Dotabuff
{
    internal class PlayerController
    {
        private MainController mainController;
        private AliasController aliasController;
        private FriendController friendController;
        private MatchController matchController;
        private LifetimeStatController lifetimeStatController;

        private dynamic PlayerPath { get { return mainController.DotabuffMappingController.PlayerPath; } }
        private dynamic PlayerMatchesPath { get { return mainController.DotabuffMappingController.PlayerMatchesPath; } }
        private dynamic HtmlAttributes { get { return mainController.DotabuffMappingController.HtmlAttributes; } }

        internal PlayerController(MainController mainController)
        {
            this.mainController = mainController;

            this.aliasController = new AliasController(mainController);
            this.friendController = new FriendController(mainController);
            this.matchController = new MatchController(mainController);
            this.lifetimeStatController = new LifetimeStatController(mainController);
        }

        internal Player GetPlayerBySteamId(string steamId)
        {
            HtmlNode root = mainController.HtmlDocumentController.GetDotabuffSteamIdPlayerRoot(steamId);

            if (root.InnerHtml.Contains("Search Results"))
            {
                throw new PlayerNotFoundException(string.Format("The player with the steam id '{0}' doesn't exist", steamId));
            }

            string playerId = root.SelectSingleNode(PlayerPath.PlayerId.Value).Attributes[HtmlAttributes.PlayerId.Attribute.Value].Value.Replace(
                                    HtmlAttributes.PlayerId.Replace.Value, ""); 

            return GetPlayer(playerId);
        }

        internal Player GetPlayer(string playerId)
        {
            HtmlNode root = mainController.HtmlDocumentController.GetDotabuffPlayerRoot(playerId);

            Player player = new Player
            {
                Id = playerId,
                Name = FetchPlayerName(playerId),
                SteamUser = FetchSteamUser(playerId),
                MostPlayedHeros = mainController.HeroController.FetchMostPlayedHeroes(playerId),
                LatestMatches = matchController.FetchLatestMatches(playerId),
                LifetimeStats = lifetimeStatController.FetchLifetimeStats(playerId),
                ProfilePicture = FetchPlayerImage(playerId),
                Aliases = aliasController.FetchAliases(playerId),
                Friends = friendController.FetchFriends(playerId)
            };

            return player;
        }

        internal IEnumerable<IMatchExtended> GetMatchesFromPlayer(string playerId, PlayerMatchesOptions playerMatchesOptions)
        {
            HtmlNode root = mainController.HtmlDocumentController.GetDotabuffPlayerMatchesRoot(playerId, playerMatchesOptions);

            List <IMatchExtended> playerMatches = new List<IMatchExtended>();

            MatchPath machPath = new MatchPath
            {
                Duration = PlayerMatchesPath.Duration.Value,
                Hero = PlayerMatchesPath.Hero.Value,
                Id = PlayerMatchesPath.Id.Value,
                Kda = PlayerMatchesPath.Kda.Value,
                Mode = PlayerMatchesPath.Mode.Value,
                Result = PlayerMatchesPath.Result.Value,
                Skillbracket = PlayerMatchesPath.Skillbracket.Value,
                TimeAgo = PlayerMatchesPath.TimeAgo.Value,
                Type = PlayerMatchesPath.Type.Value,
                IdAttribute = HtmlAttributes.PlayerMatches.Attribute.Value,
                IdReplace = HtmlAttributes.PlayerMatches.Replace.Value
            };

            int counter = 1;
            IEnumerable<HtmlNode> matchesNodes = root.SelectNodes(PlayerMatchesPath.Table.Value);

            if (matchesNodes != null)
            {
                foreach (HtmlNode matchNode in matchesNodes)
                {
                    Match match = matchController.MapHtmlNodeToMatch(root, machPath, counter);

                    List<IItem> items = new List<IItem>();
                    IEnumerable<HtmlNode> itemNodes = root.SelectNodes(mainController.CombinePathWithListCount(PlayerMatchesPath.Items.Value, counter));

                    if (itemNodes != null)
                    {
                        foreach (HtmlNode itemNode in itemNodes)
                        {
                            string itemReference =
                                itemNode.Attributes[HtmlAttributes.Item.Attribute.Value].Value.Replace(
                                    HtmlAttributes.Item.Replace.Value, "");
                            items.Add(mainController.ItemController.GetItem(itemReference));
                        }
                    }

                    match.Items = items;

                    playerMatches.Add(match);

                    counter++;
                }
            }

            return playerMatches;
        }

        private SteamUser FetchSteamUser(string playerId)
        {
            HtmlNode root = mainController.HtmlDocumentController.GetDotabuffPlayerRoot(playerId);

            HtmlNode steamIdNode = root.SelectSingleNode(PlayerPath.SteamData.Id.Value);
            HtmlNode steamProfileUrlNode = root.SelectSingleNode(PlayerPath.SteamData.ProfileUrl.Value);

            SteamUser steamUser = new SteamUser();
            steamUser.Id = steamIdNode.InnerText;
            steamUser.ProfileUrl = steamProfileUrlNode.Attributes[MainController.HTML_ATTRIBUTE_HREF].Value;

            return steamUser;
        }

        private string FetchPlayerName(string playerId)
        {
            HtmlNode root = mainController.HtmlDocumentController.GetDotabuffPlayerRoot(playerId);

            HtmlNode playerNode = root.SelectSingleNode(PlayerPath.PlayerName.Value);

            return playerNode.InnerText;
        }

        private byte[] FetchPlayerImage(string playerId)
        {
            HtmlNode root = mainController.HtmlDocumentController.GetDotabuffPlayerRoot(playerId);

            HtmlNode imageNode = root.SelectSingleNode(PlayerPath.PlayerImage.Value);
            string imageUrl = imageNode.Attributes[MainController.HTML_ATTRIBUTE_SRC].Value;

            byte[] playerImage = null;
            using (WebClient webclient = mainController.HtmlDocumentController.CreateWebclient())
            {
                playerImage = webclient.DownloadData(imageUrl);
            }
            return playerImage;
        }
    }
}
