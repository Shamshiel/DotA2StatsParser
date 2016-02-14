using DotA2StatsParser.Model.Dotabuff.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff
{
    internal class Ability : IAbility
    {
        /// <summary>
        /// Gets the name of the ability.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; internal set; }
        /// <summary>
        /// Gets the usual key for the ability.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public char? Key { get; internal set; }
        /// <summary>
        /// Gets the cooldown in seconds.
        /// </summary>
        /// <value>
        /// The cooldown.
        /// </value>
        public int Cooldown { get; internal set; }
        /// <summary>
        /// Gets the mana cost of the ability.
        /// </summary>
        /// <value>
        /// The mana cost.
        /// </value>
        public int ManaCost { get; internal set; }
        /// <summary>
        /// Gets the image of the ability.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        public byte[] Image { get; internal set; }

        internal Ability()
        {

        }

        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }
}
