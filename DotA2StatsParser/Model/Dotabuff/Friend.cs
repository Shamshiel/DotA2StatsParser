using DotA2StatsParser.Model.Dotabuff.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff
{
    internal class Friend : IFriend
    {
        /// <summary>
        /// Gets the Id of the friend
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; internal set; }
        /// <summary>
        /// Gets the profile picture of the friend
        /// </summary>
        /// <value>
        /// The player image.
        /// </value>
        public byte[] PlayerImage { get; internal set; }
        /// <summary>
        /// Gets the latest name of the friend
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; internal set; }
        /// <summary>
        /// Gets the number of matches that the player played with the friend
        /// </summary>
        /// <value>
        /// The matches.
        /// </value>
        public int Matches { get; internal set; }
        /// <summary>
        /// Gets the win rate that the player has with the friend
        /// </summary>
        /// <value>
        /// The win rate.
        /// </value>
        public IWinRate WinRate { get; internal set; }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, Id);
        }
    }
}
