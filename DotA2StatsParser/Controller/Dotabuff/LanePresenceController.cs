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
    internal class LanePresenceController
    {
        private MainController mainController;

        private dynamic HeroPath { get { return mainController.DotabuffMappingController.HeroPath; } }

        internal LanePresenceController(MainController mainController)
        {
            this.mainController = mainController;
        }

        internal List<LanePresence> FetchLanePresence(string heroReference)
        {
            HtmlNode root = mainController.HtmlDocumentController.GetDotabuffHeroRoot(heroReference);

            HtmlNode lanePresenceTable = root.SelectSingleNode(HeroPath.LanePresence.Value);

            var query = from table in lanePresenceTable.ChildNodes
                        from row in table.SelectNodes(MainController.HTML_TABLE_TR).Cast<HtmlNode>()
                        select row;

            List<LanePresence> lanePresenceList = new List<LanePresence>();

            foreach (HtmlNode row in query.Skip(1))
            {
                lanePresenceList.Add(MapHtmlNode(row));
            }

            return lanePresenceList;
        }

        private LanePresence MapHtmlNode(HtmlNode tableRow)
        {
            return new LanePresence()
            {
                Lane = mainController.MapStringToEnum<Lanes>(tableRow.ChildNodes[0].InnerText),
                Presence = tableRow.ChildNodes[1].InnerText,
                WinRate  = tableRow.ChildNodes[2].InnerText,
                Kda = tableRow.ChildNodes[3].InnerText,
                GPM = tableRow.ChildNodes[4].InnerText,
                XPM = tableRow.ChildNodes[5].InnerText
            };
        }
    }
}
