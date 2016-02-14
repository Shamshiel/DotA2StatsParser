using DotA2StatsParser.Model.Dotabuff.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff
{
    internal class Alias : IAlias
    {
        /// <summary>
        /// Gets the name of the alias.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; internal set; }
        /// <summary>
        /// Gets the date the alias was last used.
        /// </summary>
        /// <value>
        /// The last used date.
        /// </value>
        public DateTime LastUsed { get; internal set; }

        public override string ToString()
        {
            return string.Format("{0} - Last Used {1}", Name, LastUsed.ToShortDateString());
        }
    }
}
