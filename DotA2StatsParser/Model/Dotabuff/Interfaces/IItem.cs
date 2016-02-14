using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff.Interfaces
{
    public interface IItem
    {
        /// <summary>
        /// Gets the name of the item.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }
        /// <summary>
        /// Gets the reference (url representation at dotabuff) of the item.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        string Reference { get; }
        /// <summary>
        /// Gets the item enum.
        /// </summary>
        /// <value>
        /// The item enum.
        /// </value>
        Items ItemEnum { get; }
        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        byte[] Image { get; }
        /// <summary>
        /// Gets the item details.
        /// </summary>
        /// <value>
        /// The item details.
        /// </value>
        IItemDetais ItemDetails { get; }
        /// <summary>
        /// Gets the win rate.
        /// </summary>
        /// <value>
        /// The win rate.
        /// </value>
        IWinRate WinRate { get; }
        /// <summary>
        /// Gets the popularity.
        /// </summary>
        /// <value>
        /// The popularity.
        /// </value>
        IPopularity Popularity { get; }
        /// <summary>
        /// Gets the items that are need to build this item.
        /// </summary>
        /// <value>
        /// The item builds from.
        /// </value>
        IEnumerable<IItem> BuildsFrom { get; }
        /// <summary>
        /// Gets the items that this item can build into.
        /// </summary>
        /// <value>
        /// The itms builds into.
        /// </value>
        IEnumerable<IItem> BuildsInto { get; }
    }
}
