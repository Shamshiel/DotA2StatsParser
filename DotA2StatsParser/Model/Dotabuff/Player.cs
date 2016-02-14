using DotA2StatsParser.Model.Dotabuff.Interfaces;
using System.Collections.Generic;

namespace DotA2StatsParser.Model.Dotabuff
{
    internal class Player : IPlayer
    {
        /// <summary>
        /// Gets the profile picture.
        /// </summary>
        /// <value>
        /// The profile picture.
        /// </value>
        public byte[] ProfilePicture { get; internal set; }
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; internal set; }
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; internal set; }
        /// <summary>
        /// Gets the steam user.
        /// </summary>
        /// <value>
        /// The steam user.
        /// </value>
        public ISteamUser SteamUser { get; internal set; }
        /// <summary>
        /// Gets the lifetime stats.
        /// </summary>
        /// <value>
        /// The lifetime stats.
        /// </value>
        public IEnumerable<KeyValuePair<string, List<ILifetimeStat>>> LifetimeStats { get; internal set; }
        /// <summary>
        /// Gets the most played heros.
        /// </summary>
        /// <value>
        /// The most played heros.
        /// </value>
        public IEnumerable<KeyValuePair<IHero, IStat>> MostPlayedHeros { get; internal set; }
        /// <summary>
        /// Gets the latest matches.
        /// </summary>
        /// <value>
        /// The latest matches.
        /// </value>
        public IEnumerable<IMatch> LatestMatches { get; internal set; }
        /// <summary>
        /// Gets the aliases.
        /// </summary>
        /// <value>
        /// The aliases.
        /// </value>
        public IEnumerable<IAlias> Aliases { get; internal set; }
        /// <summary>
        /// Gets the friends.
        /// </summary>
        /// <value>
        /// The friends.
        /// </value>
        public IEnumerable<IFriend> Friends { get; internal set; }

        public override string ToString()
        {
            return string.Format("{0} ({1}) - {2}", Name, Id, SteamUser);
        }
    }
}
