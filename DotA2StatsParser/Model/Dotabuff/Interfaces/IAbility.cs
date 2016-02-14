using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff.Interfaces
{
    public interface IAbility
    {
        /// <summary>
        /// Gets the name of the ability.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }
        /// <summary>
        /// Gets the usual key for the ability.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        char? Key { get; }
        /// <summary>
        /// Gets the cooldown in seconds.
        /// </summary>
        /// <value>
        /// The cooldown.
        /// </value>
        int Cooldown { get; }
        /// <summary>
        /// Gets the mana cost of the ability.
        /// </summary>
        /// <value>
        /// The mana cost.
        /// </value>
        int ManaCost { get; }
        /// <summary>
        /// Gets the image of the ability.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        byte[] Image { get; }
    }
}
