using DotA2StatsParser.Model.Dotabuff.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff
{
    internal class Stat : IStat
    {
        /// <summary>
        /// Gets the number of matches.
        /// </summary>
        /// <value>
        /// The number of matches.
        /// </value>
        public int Matches { get; internal set; }
        /// <summary>
        /// Gets the win rate.
        /// </summary>
        /// <value>
        /// The win rate.
        /// </value>
        public string WinRate { get; internal set; }
        /// <summary>
        /// Gets the kills, deaths and asissts ratio.
        /// </summary>
        /// <value>
        /// The the kills, deaths and asissts ratio.
        /// </value>
        public float Kda { get; internal set; }

        internal Stat(string matches, string winRate)
        {
            this.WinRate = winRate;

            int parsedMatches = 0;
            int.TryParse(matches, out parsedMatches);
            this.Matches = parsedMatches;
        }

        internal Stat(string matches, string winRate, string kda)
        {
            this.WinRate = winRate;

            int parsedMatches = 0;
            int.TryParse(matches, out parsedMatches);
            this.Matches = parsedMatches;

            float parsedKda = 0;
            float.TryParse(kda, out parsedKda);
            this.Kda = parsedKda;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Matches, WinRate, Kda);
        }
    }
}
