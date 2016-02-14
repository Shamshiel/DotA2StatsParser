using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff.Interfaces
{
    public interface IPlayer
    {
        /// <summary>
        /// Gets the profile picture.
        /// </summary>
        /// <value>
        /// The profile picture.
        /// </value>
        byte[] ProfilePicture { get; }
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string Id { get; }
        /// <summary>
        /// Gets the steam user.
        /// </summary>
        /// <value>
        /// The steam user.
        /// </value>
        ISteamUser SteamUser { get; }
        /// <summary>
        /// Gets the lifetime stats.
        /// </summary>
        /// <value>
        /// The lifetime stats.
        /// </value>
        IEnumerable<KeyValuePair<string, List<ILifetimeStat>>> LifetimeStats { get; }
        /// <summary>
        /// Gets the most played heros.
        /// </summary>
        /// <value>
        /// The most played heros.
        /// </value>
        IEnumerable<KeyValuePair<IHero, IStat>> MostPlayedHeros { get; }
        /// <summary>
        /// Gets the latest matches.
        /// </summary>
        /// <value>
        /// The latest matches.
        /// </value>
        IEnumerable<IMatch> LatestMatches { get; }
        /// <summary>
        /// Gets the aliases.
        /// </summary>
        /// <value>
        /// The aliases.
        /// </value>
        IEnumerable<IAlias> Aliases { get; }
        /// <summary>
        /// Gets the friends.
        /// </summary>
        /// <value>
        /// The friends.
        /// </value>
        IEnumerable<IFriend> Friends { get; }
    }
}
