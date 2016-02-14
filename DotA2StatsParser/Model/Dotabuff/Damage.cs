using DotA2StatsParser.Model.Dotabuff.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff
{
    internal class Damage : IDamage
    {
        /// <summary>
        /// Gets the minimum damage output.
        /// </summary>
        /// <value>
        /// The minimum damage output.
        /// </value>
        public int Minimum { get; internal set; }
        /// <summary>
        /// Gets the maximum damage output.
        /// </summary>
        /// <value>
        /// The maximum damage output.
        /// </value>
        public int Maximum { get; internal set; }

        internal Damage(int minimum, int maximum)
        {
            this.Minimum = minimum;
            this.Maximum = maximum;
        }

        public override string ToString()
        {
            return Minimum + " - " + Maximum;
        }
    }
}
