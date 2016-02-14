using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff.Interfaces
{
    public interface IMatch
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string Id { get; }
        /// <summary>
        /// Gets the hero.
        /// </summary>
        /// <value>
        /// The hero.
        /// </value>
        IHero Hero { get; }
        /// <summary>
        /// Gets how long the match was ago.
        /// </summary>
        /// <value>
        /// The time how long the match was ago.
        /// </value>
        DateTime TimeAgo { get; }
        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        Results Result { get; }
        /// <summary>
        /// Gets the mode.
        /// </summary>
        /// <value>
        /// The mode.
        /// </value>
        Modes Mode { get; }
        /// <summary>
        /// Gets the skillbracket.
        /// </summary>
        /// <value>
        /// The skillbracket.
        /// </value>
        Skillbrackets Skillbracket { get; }
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        Types Type { get; }
        /// <summary>
        /// Gets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        TimeSpan Duration { get; }
        /// <summary>
        /// Gets the kills, deaths and assits.
        /// </summary>
        /// <value>
        /// The kills, deaths and assits.
        /// </value>
        IKda Kda { get; }
    }
}
