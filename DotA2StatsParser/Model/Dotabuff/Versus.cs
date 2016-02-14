using DotA2StatsParser.Model.Dotabuff.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff
{
    public class Versus
    {
        /// <summary>
        /// Gets the hero.
        /// </summary>
        /// <value>
        /// The hero.
        /// </value>
        public IHero Hero { get; internal set; }
        /// <summary>
        /// Gets the advantage.
        /// </summary>
        /// <value>
        /// The advantage.
        /// </value>
        public string Advantage { get; internal set; }
        /// <summary>
        /// Gets the win rate.
        /// </summary>
        /// <value>
        /// The win rate.
        /// </value>
        public string WinRate { get; internal set; }
        /// <summary>
        /// Gets the matches.
        /// </summary>
        /// <value>
        /// The matches.
        /// </value>
        public int Matches { get; internal set; }

        public override string ToString()
        {
            return string.Format("{0} - Win Rate: {1}", Hero.ToString(), WinRate);
        }
    }
}
