using DotA2StatsParser.Model.Dotabuff;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Controller.Dotabuff
{
    internal class AttributeController
    {
        private MainController mainController;

        private dynamic HeroPath { get { return mainController.DotabuffMappingController.HeroPath; } }

        internal AttributeController(MainController mainController)
        {
            this.mainController = mainController;
        }

        internal HeroAttributes FetchAttributes(string heroReference)
        {
            HtmlNode root = mainController.HtmlDocumentController.GetDotabuffHeroRoot(heroReference);

            string strength = root.SelectSingleNode(HeroPath.Attributes.Main.Strength.Value).InnerText;
            string agility = root.SelectSingleNode(HeroPath.Attributes.Main.Agility.Value).InnerText;
            string intelligence = root.SelectSingleNode(HeroPath.Attributes.Main.Intelligence.Value).InnerText;

            string[] strengthSplit = strength.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);
            string[] agilitySplit = agility.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);
            string[] intelligenceSplit = intelligence.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);

            HeroAttributes attributes = new HeroAttributes();

            attributes.Strength = float.Parse(strengthSplit[0].Replace(".", ","));
            attributes.Agility = float.Parse(agilitySplit[0].Replace(".", ","));
            attributes.Intelligence = float.Parse(intelligenceSplit[0].Replace(".", ","));

            attributes.StrengthGain = float.Parse(strengthSplit[1].Replace(".",","));
            attributes.AgilityGain = float.Parse(agilitySplit[1].Replace(".", ","));
            attributes.IntelligenceGain = float.Parse(intelligenceSplit[1].Replace(".", ","));


            string movementSpeed = root.SelectSingleNode(HeroPath.Attributes.Other.MovementSpeed.Value).InnerText;
            string sightRange = HtmlEntity.DeEntitize(root.SelectSingleNode(HeroPath.Attributes.Other.SightRange.Value).InnerText);
            string armor = root.SelectSingleNode(HeroPath.Attributes.Other.Armor.Value).InnerText;
            string baseAttackTime = root.SelectSingleNode(HeroPath.Attributes.Other.BaseAttackTime.Value).InnerText;
            string damage = root.SelectSingleNode(HeroPath.Attributes.Other.Damage.Value).InnerText;
            string attackPoint = root.SelectSingleNode(HeroPath.Attributes.Other.AttackPoint.Value).InnerText;

            string[] sightRangeSplit = sightRange.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            string[] damageSplit = damage.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

            attributes.MovementSpeed = int.Parse(movementSpeed);
            attributes.SightRangeDay = int.Parse(sightRangeSplit[0]);
            attributes.SightRangeNight = int.Parse(sightRangeSplit[1]);
            attributes.Armor = float.Parse(armor);
            attributes.BaseAttackTime = float.Parse(baseAttackTime);
            attributes.Damage = new Damage(int.Parse(damageSplit[0]), int.Parse(damageSplit[1]));
            attributes.AttackPoint = float.Parse(attackPoint);

            return attributes;
        }

    }
}
