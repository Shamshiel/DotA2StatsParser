using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff.Interfaces
{
    public interface ISteamUser
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string Id { get; }
        /// <summary>
        /// Gets the profile URL.
        /// </summary>
        /// <value>
        /// The profile URL.
        /// </value>
        string ProfileUrl { get; }
    }
}
