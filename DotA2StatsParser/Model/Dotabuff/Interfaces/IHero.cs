using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff.Interfaces
{
    public interface IHero
    {
        /// <summary>
        /// Gets the name of the hero.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }
        /// <summary>
        /// Gets the reference (url representation at dotabuff) of the hero.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        string Reference { get; }
        /// <summary>
        /// Gets the hero enum.
        /// </summary>
        /// <value>
        /// The hero enum.
        /// </value>
        Heroes HeroEnum { get; }
        /// <summary>
        /// Gets the hero image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        byte[] Image { get; }
        /// <summary>
        /// Gets the win rate.
        /// </summary>
        /// <value>
        /// The win rate.
        /// </value>
        IWinRate WinRate { get; }
        /// <summary>
        /// Gets the popularity.
        /// </summary>
        /// <value>
        /// The popularity.
        /// </value>
        IPopularity Popularity { get; }
        /// <summary>
        /// Gets a list of roles.
        /// </summary>
        /// <value>
        /// The roles as a list representation.
        /// </value>
        IEnumerable<Roles> Roles { get; }
        /// <summary>
        /// Gets the lane presence.
        /// </summary>
        /// <value>
        /// The lane presence.
        /// </value>
        IEnumerable<ILanePresence> LanePresence { get; }
        /// <summary>
        /// Gets the best versus match ups.
        /// </summary>
        /// <value>
        /// The best versus match ups.
        /// </value>
        IEnumerable<Versus> BestVersus { get; }
        /// <summary>
        /// Gets the worst versus match ups.
        /// </summary>
        /// <value>
        /// The worst versus match ups.
        /// </value>
        IEnumerable<Versus> WorstVersus { get; }
        /// <summary>
        /// Gets the attributes of the hero.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        IHeroAttributes Attributes { get; }
        /// <summary>
        /// Gets the most used items.
        /// </summary>
        /// <value>
        /// The most used items.
        /// </value>
        IEnumerable<IItem> MostUsedItems { get; }
        /// <summary>
        /// Gets the abilities.
        /// </summary>
        /// <value>
        /// The abilities.
        /// </value>
        IEnumerable<IAbility> Abilities { get; }
        /// <summary>
        /// Gets the most popular ability builds.
        /// </summary>
        /// <value>
        /// The most popular ability build.
        /// </value>
        IEnumerable<AbilityBuild> MostPopularAbilityBuild { get; }
    }
}
