using DotA2StatsParser.Model.Dotabuff.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff
{
    internal class Match : IMatchExtended
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; internal set; }
        /// <summary>
        /// Gets the hero.
        /// </summary>
        /// <value>
        /// The hero.
        /// </value>
        public IHero Hero { get; internal set; }
        /// <summary>
        /// Gets how long the match was ago.
        /// </summary>
        /// <value>
        /// The time how long the match was ago.
        /// </value>
        public DateTime TimeAgo { get; internal set; }
        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public Results Result { get; internal set; }
        /// <summary>
        /// Gets the mode.
        /// </summary>
        /// <value>
        /// The mode.
        /// </value>
        public Modes Mode { get; internal set; }
        /// <summary>
        /// Gets the skillbracket.
        /// </summary>
        /// <value>
        /// The skillbracket.
        /// </value>
        public Skillbrackets Skillbracket { get; internal set; }
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public Types Type { get; internal set; }
        /// <summary>
        /// Gets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public TimeSpan Duration { get; internal set; }
        /// <summary>
        /// Gets the kills, deaths and assits.
        /// </summary>
        /// <value>
        /// The kills, deaths and assits.
        /// </value>
        public IKda Kda { get; internal set; }
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public IEnumerable<IItem> Items { get; internal set; }

        public override string ToString()
        {
            return string.Format("{0} - {1} - {2}", Id, Hero, Kda);
        }
    }
}
