using DotA2StatsParser.Controller;
using DotA2StatsParser.Model.Dotabuff;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Controller.Dotabuff
{
    internal class FriendController
    {
        private MainController mainController;

        private dynamic PlayerPath { get { return mainController.DotabuffMappingController.PlayerPath; } }

        private dynamic HtmlAttributes { get { return mainController.DotabuffMappingController.HtmlAttributes; } }

        internal FriendController(MainController mainController)
        {
            this.mainController = mainController;
        }

        internal List<Friend> FetchFriends(string playerId)
        {
            HtmlNode root = mainController.HtmlDocumentController.GetDotabuffPlayerRoot(playerId);

            IEnumerable<HtmlNode> friendsNode = root.SelectNodes(PlayerPath.Friends.Value);

            List<Friend> friendList = new List<Friend>();
            foreach (HtmlNode friendNode in friendsNode)
            {
                using (WebClient webClient = mainController.HtmlDocumentController.CreateWebclient())
                {
                    friendList.Add(new Friend()
                                    {
                                        PlayerImage = webClient.DownloadData(friendNode.Descendants(MainController.HTML_IMG_TAG).First().Attributes[MainController.HTML_ATTRIBUTE_SRC].Value),
                                        Id = HtmlEntity.DeEntitize(friendNode.Attributes[HtmlAttributes.Player.Attribute.Value].Value).Replace(HtmlAttributes.Player.Replace.Value, ""),
                                        Name = friendNode.ChildNodes[1].InnerText,
                                        Matches = int.Parse(friendNode.ChildNodes[2].InnerText.Replace(",", "")),
                                        WinRate = mainController.ConvertStringToWinRate(friendNode.ChildNodes[3].InnerText)

                                    });
                }
            }

            return friendList;
        }
    }
}
