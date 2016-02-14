using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotA2StatsParser.Model.Dotabuff.Interfaces;

namespace DotA2StatsParser.Model.Dotabuff
{
    internal class Item : IItem
    {
        /// <summary>
        /// Gets the name of the item.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; internal set; }
        /// <summary>
        /// Gets the reference (url representation at dotabuff) of the item.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        public string Reference { get; internal set; }
        /// <summary>
        /// Gets the item enum.
        /// </summary>
        /// <value>
        /// The item enum.
        /// </value>
        public Items ItemEnum { get; internal set; }
        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        public byte[] Image { get; internal set; }
        /// <summary>
        /// Gets the item details.
        /// </summary>
        /// <value>
        /// The item details.
        /// </value>
        public IItemDetais ItemDetails { get; internal set; }
        /// <summary>
        /// Gets the win rate.
        /// </summary>
        /// <value>
        /// The win rate.
        /// </value>
        public IWinRate WinRate { get; internal set; }
        /// <summary>
        /// Gets the popularity.
        /// </summary>
        /// <value>
        /// The popularity.
        /// </value>
        public IPopularity Popularity { get; internal set; }
        /// <summary>
        /// Gets the items that are need to build this item.
        /// </summary>
        /// <value>
        /// The item builds from.
        /// </value>
        public IEnumerable<IItem> BuildsFrom { get; internal set; }
        /// <summary>
        /// Gets the items that this item can build into.
        /// </summary>
        /// <value>
        /// The itms builds into.
        /// </value>
        public IEnumerable<IItem> BuildsInto { get; internal set; }

        internal Item Copy()
        {
            Item defensiveCopy = new Item();
            defensiveCopy.Name = this.Name;
            defensiveCopy.Reference = this.Reference;
            defensiveCopy.ItemEnum = this.ItemEnum;
            defensiveCopy.Image = this.Image;
            defensiveCopy.ItemDetails = this.ItemDetails;
            defensiveCopy.WinRate = this.WinRate;
            defensiveCopy.Popularity = this.Popularity;
            defensiveCopy.BuildsFrom = this.BuildsFrom;
            defensiveCopy.BuildsInto = this.BuildsInto;
            return defensiveCopy;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Hero))
                return false;

            return (obj as Hero).Name == this.Name && (obj as Hero).Reference == this.Reference;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 37;
                hash = hash * 83 + this.Name.GetHashCode();
                hash = hash * 83 + this.Reference.GetHashCode();
                return hash;
            }
        }
    }
}
