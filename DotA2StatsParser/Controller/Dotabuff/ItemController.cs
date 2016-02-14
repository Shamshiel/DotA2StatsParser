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
    internal class ItemController
    {
        private MainController mainController;

        private ItemDetailsController itemDetailsController;

        private List<Item> mappedItems;

        private HtmlDocumentController HtmlDocumentController { get { return mainController.HtmlDocumentController; } }
        private dynamic ItemsPath { get { return mainController.DotabuffMappingController.ItemsPath; } }
        private dynamic ItemPath { get { return mainController.DotabuffMappingController.ItemPath; } }
        private dynamic UrlPath { get { return mainController.DotabuffMappingController.UrlPath; } }
        private dynamic HtmlAttributes { get { return mainController.DotabuffMappingController.HtmlAttributes; } }

        internal ItemController(MainController mainController)
        {
            this.mainController = mainController;

            this.itemDetailsController = new ItemDetailsController(mainController);

            this.mappedItems = new List<Item>();

            LoadAllItems();
        }

        private void LoadAllItems()
        {
            dynamic allItemsDynamic = mainController.DotabuffMappingController.EnumMapping.Items;

            Dictionary<string, object> allItemsDictionary = allItemsDynamic.ToObject<Dictionary<string, object>>();

            foreach (dynamic item in allItemsDictionary.Keys)
            {
                Dictionary<string, object> itemDictionary = allItemsDictionary[item].ToObject<Dictionary<string, object>>();

                Item itemForList = new Item();
                foreach (string key in itemDictionary.Keys)
                {
                    switch (key)
                    {
                        case MainController.MAPPING_DOTABUFF: itemForList.Reference = itemDictionary[key].ToString(); break;
                        case MainController.MAPPING_PARSER: itemForList.ItemEnum = mainController.MapStringToEnum<Items>(itemDictionary[key].ToString()); break;
                        case MainController.MAPPING_NAME: itemForList.Name = itemDictionary[key].ToString(); break;
                    }
                }
                mappedItems.Add(itemForList);
            }
        }

        internal Item GetItem(string itemReference)
        {
            if (mappedItems.Any(h => h.Reference == itemReference))
                return mappedItems.Find(h => h.Reference == itemReference);

            if (itemReference.Contains("recipe")) {


                System.Diagnostics.Debug.WriteLine(itemReference);



                return new Item();
            }

            throw new Exception(string.Format("Couldn't find item: {0}", itemReference));
        }

        private Item GetItem(Items itemEnum)
        {
            if (mappedItems.Any(h => h.ItemEnum == itemEnum))
                return mappedItems.Find(h => h.ItemEnum == itemEnum);

            throw new Exception(string.Format("Couldn't find item: {0}", itemEnum));
        }

        internal Item GetExtendedItem(Items itemEnum)
        {
            Item item = GetItem(itemEnum);

            HtmlNode root = HtmlDocumentController.GetDotabuffItemRoot(item.Reference);

            if (item.WinRate == null || item.Popularity == null)
            {
                item.WinRate = mainController.ConvertStringToWinRate(root.SelectSingleNode(ItemPath.General.WinRate.Value).InnerText);
                item.Popularity = mainController.ConvertStringToPopularity(root.SelectSingleNode(ItemPath.General.Popularity.Value).InnerText);

                string url = UrlPath.Main + root.SelectSingleNode(ItemPath.General.Image.Value).Attributes[MainController.HTML_ATTRIBUTE_SRC].Value;
                using (WebClient webclient = HtmlDocumentController.CreateWebclient())
                {
                    item.Image = webclient.DownloadData(url);
                }
            }

            if (item.ItemDetails == null)
                item.ItemDetails = itemDetailsController.FetchItemDetails(item.Reference);

            item.BuildsInto = FetchBuildsList(root, ItemPath.Details.BuildsInto.Value);

            item.BuildsFrom = FetchBuildsList(root, ItemPath.Details.BuildsFrom.Value);

            return item;
        }

        private List<Item> FetchBuildsList(HtmlNode root, string xPath)
        {
            IEnumerable<HtmlNode> nodes = root.SelectNodes(xPath);
            List<Item> buildsList = new List<Item>();

            if (nodes == null) return buildsList;

            foreach (HtmlNode node in nodes)
            {
                string itemReference = node.Attributes[MainController.HTML_ATTRIBUTE_HREF].Value.Replace(HtmlAttributes.Item.Replace.Value, "");
                buildsList.Add(GetItem(itemReference));
            }
            return buildsList;
        }
    }
}
