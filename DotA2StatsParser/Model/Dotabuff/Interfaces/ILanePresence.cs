using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff.Interfaces
{
    public interface ILanePresence
    {
        /// <summary>
        /// Gets the lane.
        /// </summary>
        /// <value>
        /// The lane.
        /// </value>
        Lanes Lane { get; }
        /// <summary>
        /// Gets the presence.
        /// </summary>
        /// <value>
        /// The presence.
        /// </value>
        string Presence { get; }
        /// <summary>
        /// Gets the win rate.
        /// </summary>
        /// <value>
        /// The win rate.
        /// </value>
        string WinRate { get; }
        /// <summary>
        /// Gets the kda.
        /// </summary>
        /// <value>
        /// The kda.
        /// </value>
        string Kda { get; }
        /// <summary>
        /// Gets the gold per minute.
        /// </summary>
        /// <value>
        /// The gold per minute
        /// </value>
        string GPM { get; }
        /// <summary>
        /// Gets the experience per minute.
        /// </summary>
        /// <value>
        /// The experience per minute.
        /// </value>
        string XPM { get; }
    }
}
