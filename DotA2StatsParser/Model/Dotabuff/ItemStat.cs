using DotA2StatsParser.Model.Dotabuff.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff
{
    internal class ItemStat : IItemStat
    {
        /// <summary>
        /// Gets the value of the stat.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; internal set; }
        /// <summary>
        /// Gets the name of the stat.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; internal set; }
        /// <summary>
        /// Gets the type of the item stat.
        /// </summary>
        /// <value>
        /// The type of the item stat.
        /// </value>
        public ItemStatTypes ItemStatType { get; internal set; }
        /// <summary>
        /// Gets the value of the item stat type.
        /// </summary>
        /// <value>
        /// The value of the item stat type.
        /// </value>
        public ItemStatValueTypes ItemStatValueType { get; internal set; }

        public override string ToString()
        {
           string displayValue = string.Empty;

           switch (ItemStatType)
           {
               case ItemStatTypes.Attribute: displayValue = "+ " + Value + " " + Name; break;
               case ItemStatTypes.Effect: displayValue = Name + " " + Value; break;
               default: return base.ToString();
           }

           if (ItemStatValueType == ItemStatValueTypes.Percent)
               displayValue += "%";

           return displayValue;
        }
    }
}
