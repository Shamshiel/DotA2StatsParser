using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff.Interfaces
{
    /// <summary>
    /// Extends the match class with the items used in this match
    /// </summary>
    /// <seealso cref="DotA2StatsParser.Model.Dotabuff.Interfaces.IMatch" />
    public interface IMatchExtended : IMatch
    {
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        IEnumerable<IItem> Items { get; } 
    }
}
