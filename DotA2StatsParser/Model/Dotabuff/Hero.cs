using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotA2StatsParser.Model.Dotabuff.Interfaces;

namespace DotA2StatsParser.Model.Dotabuff
{
    internal class Hero : IHero
    {
        /// <summary>
        /// Gets the name of the hero.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; internal set; }
        /// <summary>
        /// Gets the reference (url representation at dotabuff) of the hero.
        /// </summary>
        /// <value>
        /// The reference.
        /// </value>
        public string Reference { get; internal set; }
        /// <summary>
        /// Gets the hero enum.
        /// </summary>
        /// <value>
        /// The hero enum.
        /// </value>
        public Heroes HeroEnum { get; internal set; }
        /// <summary>
        /// Gets the hero image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        public byte[] Image { get; internal set; }
        /// <summary>
        /// Gets the win rate.
        /// </summary>
        /// <value>
        /// The win rate.
        /// </value>
        public IWinRate WinRate { get; internal set; }
        /// <summary>
        /// Gets the popularity.
        /// </summary>
        /// <value>
        /// The popularity.
        /// </value>
        public IPopularity Popularity { get; internal set; }
        /// <summary>
        /// Gets a list of roles.
        /// </summary>
        /// <value>
        /// The roles as a list representation.
        /// </value>
        public IEnumerable<Roles> Roles { get; internal set; }
        /// <summary>
        /// Gets the lane presence.
        /// </summary>
        /// <value>
        /// The lane presence.
        /// </value>
        public IEnumerable<ILanePresence> LanePresence { get; internal set; }
        /// <summary>
        /// Gets the best versus match ups.
        /// </summary>
        /// <value>
        /// The best versus match ups.
        /// </value>
        public IEnumerable<Versus> BestVersus { get; internal set; }
        /// <summary>
        /// Gets the worst versus match ups.
        /// </summary>
        /// <value>
        /// The worst versus match ups.
        /// </value>
        public IEnumerable<Versus> WorstVersus { get; internal set; }
        /// <summary>
        /// Gets the attributes of the hero.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        public IHeroAttributes Attributes { get; internal set; }
        /// <summary>
        /// Gets the most used items.
        /// </summary>
        /// <value>
        /// The most used items.
        /// </value>
        public IEnumerable<IItem> MostUsedItems { get; internal set; }
        /// <summary>
        /// Gets the abilities.
        /// </summary>
        /// <value>
        /// The abilities.
        /// </value>
        public IEnumerable<IAbility> Abilities { get; internal set; }
        /// <summary>
        /// Gets the most popular ability builds.
        /// </summary>
        /// <value>
        /// The most popular ability build.
        /// </value>
        public IEnumerable<AbilityBuild> MostPopularAbilityBuild { get; internal set; }

        internal Hero()
        {

        }

        internal Hero(string name, string reference, Heroes heroEnum)
        {
            this.Name = name;
            this.Reference = reference;
            this.HeroEnum = heroEnum;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Hero))
                return false;

            return (obj as Hero).Name == this.Name && (obj as Hero).Reference == this.Reference;
        }

        public override int GetHashCode()
        {
            unchecked 
            {
                int hash = 23;
                hash = hash * 149 + this.Name.GetHashCode();
                hash = hash * 149 + this.Reference.GetHashCode();
                return hash;
            }
        }
    }
}
