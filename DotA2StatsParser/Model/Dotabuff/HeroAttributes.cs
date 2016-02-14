using DotA2StatsParser.Model.Dotabuff.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff
{
    internal class HeroAttributes : IHeroAttributes
    {
        /// <summary>
        /// Gets the primary attribute.
        /// </summary>
        /// <value>
        /// The primary attribute.
        /// </value>
        public Attributes PrimaryAttribute { get; internal set; }
        /// <summary>
        /// Gets the strength.
        /// </summary>
        /// <value>
        /// The strength.
        /// </value>
        public float Strength { get; internal set; }
        /// <summary>
        /// Gets the strength gain.
        /// </summary>
        /// <value>
        /// The strength gain.
        /// </value>
        public float StrengthGain { get; internal set; }
        /// <summary>
        /// Gets the agility.
        /// </summary>
        /// <value>
        /// The agility.
        /// </value>
        public float Agility { get; internal set; }
        /// <summary>
        /// Gets the agility gain.
        /// </summary>
        /// <value>
        /// The agility gain.
        /// </value>
        public float AgilityGain { get; internal set; }
        /// <summary>
        /// Gets the intelligence.
        /// </summary>
        /// <value>
        /// The intelligence.
        /// </value>
        public float Intelligence { get; internal set; }
        /// <summary>
        /// Gets the intelligence gain.
        /// </summary>
        /// <value>
        /// The intelligence gain.
        /// </value>
        public float IntelligenceGain { get; internal set; }
        /// <summary>
        /// Gets the movement speed.
        /// </summary>
        /// <value>
        /// The movement speed.
        /// </value>
        public int MovementSpeed { get; internal set; }
        /// <summary>
        /// Gets the sight range at day.
        /// </summary>
        /// <value>
        /// The sight range at day.
        /// </value>
        public int SightRangeDay { get; internal set; }
        /// <summary>
        /// Gets the sight range at night.
        /// </summary>
        /// <value>
        /// The sight range at night.
        /// </value>
        public int SightRangeNight { get; internal set; }
        /// <summary>
        /// Gets the armor.
        /// </summary>
        /// <value>
        /// The armor.
        /// </value>
        public float Armor { get; internal set; }
        /// <summary>
        /// Gets the base attack time.
        /// </summary>
        /// <value>
        /// The base attack time.
        /// </value>
        public float BaseAttackTime { get; internal set; }
        /// <summary>
        /// Gets the damage.
        /// </summary>
        /// <value>
        /// The damage.
        /// </value>
        public IDamage Damage { get; internal set; }
        /// <summary>
        /// Gets the attack point.
        /// </summary>
        /// <value>
        /// The attack point.
        /// </value>
        public float AttackPoint { get; internal set; }

        internal HeroAttributes()
        {

        }

        public override string ToString()
        {
            string heroAttributeString = string.Format("Strength: {0} (+{1})", Strength, StrengthGain);
            heroAttributeString += string.Format(" Agility: {0} (+{1})", Agility, AgilityGain);
            heroAttributeString += string.Format(" Intelligence: {0} (+{1})", Intelligence, IntelligenceGain);
            return heroAttributeString;
        }
    }
}
