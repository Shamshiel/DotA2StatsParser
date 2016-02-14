using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff.Interfaces
{
    public interface IItemDetais
    {
        /// <summary>
        /// Gets the cost.
        /// </summary>
        /// <value>
        /// The cost.
        /// </value>
        int Cost { get; }
        /// <summary>
        /// Gets the item stats.
        /// </summary>
        /// <value>
        /// The item stats.
        /// </value>
        IEnumerable<IItemStat> ItemStats { get; }
    }
}
