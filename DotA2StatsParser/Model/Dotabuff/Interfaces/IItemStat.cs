using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff.Interfaces
{
    public interface IItemStat
    {
        /// <summary>
        /// Gets the value of the stat.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        string Value { get; }
        /// <summary>
        /// Gets the name of the stat.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }
        /// <summary>
        /// Gets the type of the item stat.
        /// </summary>
        /// <value>
        /// The type of the item stat.
        /// </value>
        ItemStatTypes ItemStatType { get; }
        /// <summary>
        /// Gets the value of the item stat type.
        /// </summary>
        /// <value>
        /// The value of the item stat type.
        /// </value>
        ItemStatValueTypes ItemStatValueType { get; }
    }
}
