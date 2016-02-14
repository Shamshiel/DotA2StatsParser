using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff.Interfaces
{
    public interface IFriend
    {
        /// <summary>
        /// Gets the Id of the friend
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string Id { get; }
        /// <summary>
        /// Gets the profile picture of the friend
        /// </summary>
        /// <value>
        /// The player image.
        /// </value>
        byte[] PlayerImage { get; }
        /// <summary>
        /// Gets the latest name of the friend
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }
        /// <summary>
        /// Gets the number of matches that the player played with the friend
        /// </summary>
        /// <value>
        /// The matches.
        /// </value>
        int Matches { get; }
        /// <summary>
        /// Gets the win rate that the player has with the friend
        /// </summary>
        /// <value>
        /// The win rate.
        /// </value>
        IWinRate WinRate { get; }
    }
}
