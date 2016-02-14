using DotA2StatsParser.Model.Dotabuff.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff
{
    internal class LanePresence : ILanePresence
    {
        /// <summary>
        /// Gets the lane.
        /// </summary>
        /// <value>
        /// The lane.
        /// </value>
        public Lanes Lane { get; internal set; }
        /// <summary>
        /// Gets the presence.
        /// </summary>
        /// <value>
        /// The presence.
        /// </value>
        public string Presence { get; internal set; }
        /// <summary>
        /// Gets the win rate.
        /// </summary>
        /// <value>
        /// The win rate.
        /// </value>
        public string WinRate { get; internal set; }
        /// <summary>
        /// Gets the kda.
        /// </summary>
        /// <value>
        /// The kda.
        /// </value>
        public string Kda { get; internal set; }
        /// <summary>
        /// </summary>
        /// <value>
        /// The gold per minute
        /// </value>
        public string GPM { get; internal set; }
        /// <summary>
        /// </summary>
        /// <value>
        /// The experience per minute.
        /// </value>
        public string XPM { get; internal set; }

        public override string ToString()
        {
            return Lane.ToString();
        }
    }
}
