using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff.Interfaces
{
    public interface IStat
    {
        /// <summary>
        /// Gets the number of matches.
        /// </summary>
        /// <value>
        /// The number of matches.
        /// </value>
        int Matches { get; }
        /// <summary>
        /// Gets the win rate.
        /// </summary>
        /// <value>
        /// The win rate.
        /// </value>
        string WinRate { get; }
        /// <summary>
        /// Gets the kills, deaths and asissts ratio.
        /// </summary>
        /// <value>
        /// The the kills, deaths and asissts ratio.
        /// </value>
        float Kda { get; }
    }
}
