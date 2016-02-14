using DotA2StatsParser.Model.Dotabuff.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff
{
    internal class Kda : IKda
    {
        /// <summary>
        /// Gets the kills.
        /// </summary>
        /// <value>
        /// The kills.
        /// </value>
        public int Kills { get; internal set; }
        /// <summary>
        /// Gets the deaths.
        /// </summary>
        /// <value>
        /// The deaths.
        /// </value>
        public int Deaths { get; internal set; }
        /// <summary>
        /// Gets the assists.
        /// </summary>
        /// <value>
        /// The assists.
        /// </value>
        public int Assists { get; internal set; }

        internal Kda(int kills, int deaths, int assists)
        {
            this.Kills = kills;
            this.Deaths = deaths;
            this.Assists = assists;
        }

        internal Kda(string kills, string deaths, string assists)
            : this(int.Parse(kills), int.Parse(deaths), int.Parse(assists))
        {

        }

        public override string ToString()
        {
            return string.Format("{0} / {1} / {2}", Kills, Deaths, Assists);
        }
    }
}
