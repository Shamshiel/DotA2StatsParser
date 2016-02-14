using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff.Interfaces
{
    public interface IHeroAttributes
    {

        /// <summary>
        /// Gets the primary attribute.
        /// </summary>
        /// <value>
        /// The primary attribute.
        /// </value>
        Attributes PrimaryAttribute { get; }
        /// <summary>
        /// Gets the strength.
        /// </summary>
        /// <value>
        /// The strength.
        /// </value>
        float Strength { get; }
        /// <summary>
        /// Gets the strength gain.
        /// </summary>
        /// <value>
        /// The strength gain.
        /// </value>
        float StrengthGain { get; }
        /// <summary>
        /// Gets the agility.
        /// </summary>
        /// <value>
        /// The agility.
        /// </value>
        float Agility { get; }
        /// <summary>
        /// Gets the agility gain.
        /// </summary>
        /// <value>
        /// The agility gain.
        /// </value>
        float AgilityGain { get; }
        /// <summary>
        /// Gets the intelligence.
        /// </summary>
        /// <value>
        /// The intelligence.
        /// </value>
        float Intelligence { get; }
        /// <summary>
        /// Gets the intelligence gain.
        /// </summary>
        /// <value>
        /// The intelligence gain.
        /// </value>
        float IntelligenceGain { get; }
        /// <summary>
        /// Gets the movement speed.
        /// </summary>
        /// <value>
        /// The movement speed.
        /// </value>
        int MovementSpeed { get; }
        /// <summary>
        /// Gets the sight range at day.
        /// </summary>
        /// <value>
        /// The sight range at day.
        /// </value>
        int SightRangeDay { get; }
        /// <summary>
        /// Gets the sight range at night.
        /// </summary>
        /// <value>
        /// The sight range at night.
        /// </value>
        int SightRangeNight { get; }
        /// <summary>
        /// Gets the armor.
        /// </summary>
        /// <value>
        /// The armor.
        /// </value>
        float Armor { get; }
        /// <summary>
        /// Gets the base attack time.
        /// </summary>
        /// <value>
        /// The base attack time.
        /// </value>
        float BaseAttackTime { get; }
        /// <summary>
        /// Gets the damage.
        /// </summary>
        /// <value>
        /// The damage.
        /// </value>
        IDamage Damage { get; }
        /// <summary>
        /// Gets the attack point.
        /// </summary>
        /// <value>
        /// The attack point.
        /// </value>
        float AttackPoint { get; }
    }
}
