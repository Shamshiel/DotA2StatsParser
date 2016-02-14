using DotA2StatsParser.Model.Dotabuff;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Controller.Dotabuff
{
    internal class ItemDetailsController
    {
         private MainController mainController;

        private dynamic ItemsPath { get { return mainController.DotabuffMappingController.ItemsPath; } }
        private dynamic ItemPath { get { return mainController.DotabuffMappingController.ItemPath; } }
        private dynamic ItemSelector { get { return mainController.DotabuffMappingController.Selector.Item; } }

        internal ItemDetailsController(MainController mainController)
         {
             this.mainController = mainController;
         }

        internal ItemDetails FetchItemDetails(string itemReference)
         {
             HtmlNode root = mainController.HtmlDocumentController.GetDotabuffItemRoot(itemReference);

             ItemDetails itemDetails = new ItemDetails();

             HtmlNode costNode = root.SelectSingleNode(ItemPath.Details.Cost.Value);

             if (costNode != null)
             {
                 string cost = costNode.InnerText;
                 itemDetails.Cost = int.Parse(cost);
             }

             IEnumerable<HtmlNode> statNodess = root.SelectNodes(ItemPath.Details.Stats.Value);
             List<ItemStat> itemStatList = new List<ItemStat>();
             if (statNodess != null)
             {
                 foreach (HtmlNode node in statNodess)
                 {
                     ItemStat itemStat = new ItemStat();

                     string value = string.Empty;
                     if (node.Descendants(MainController.HTML_SPAN_TAG).Any(d => d.Attributes.Contains(MainController.HTML_ATTRIBUTE_CLASS) && d.Attributes[MainController.HTML_ATTRIBUTE_CLASS].Value.Contains(ItemSelector.Details.Stats.Value.Value)))
                     {
                         value = node.Descendants(MainController.HTML_SPAN_TAG).Where(d => d.Attributes.Contains(MainController.HTML_ATTRIBUTE_CLASS) 
                             && d.Attributes[MainController.HTML_ATTRIBUTE_CLASS].Value.Contains(ItemSelector.Details.Stats.Value.Value)).First().InnerText;
                     }
                     else
                     {
                         List<string> valueList = node.Descendants(MainController.HTML_SPAN_TAG).Where(d => d.Attributes.Contains(MainController.HTML_ATTRIBUTE_CLASS)
                             && d.Attributes[MainController.HTML_ATTRIBUTE_CLASS].Value.Contains("other")).Select(s => s.InnerText).ToList();

                         valueList.ForEach(s => value += s + " ");
                     }

                     if (!mainController.IsDigitsOnly(value))
                     {
                         itemStat.ItemStatValueType = ItemStatValueTypes.Percent;
                     }
                     else
                     {
                         itemStat.ItemStatValueType = ItemStatValueTypes.Normal;
                     }


                     itemStat.Value = value;
                     itemStat.Name = node.Descendants(MainController.HTML_SPAN_TAG).Where(d => d.Attributes.Contains(MainController.HTML_ATTRIBUTE_CLASS) && d.Attributes[MainController.HTML_ATTRIBUTE_CLASS].Value.Contains(ItemSelector.Details.Stats.Name.Value)).First().InnerText;

                     string itemStatType = node.Attributes[MainController.HTML_ATTRIBUTE_CLASS].Value;
                     itemStat.ItemStatType = mainController.MapStringToEnum<ItemStatTypes>(itemStatType.Replace("stat ", ""));

                     itemStatList.Add(itemStat);
                 }
             }

             itemDetails.ItemStats = itemStatList;

             return itemDetails;
         }
    }
}
