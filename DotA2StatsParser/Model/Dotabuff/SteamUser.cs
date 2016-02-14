using DotA2StatsParser.Model.Dotabuff.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff
{
    internal class SteamUser : ISteamUser
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; internal set; }
        /// <summary>
        /// Gets the profile URL.
        /// </summary>
        /// <value>
        /// The profile URL.
        /// </value>
        public string ProfileUrl { get; internal set; }

        public override string ToString()
        {
            return string.Format("{0}", Id);
        }
    }
}
