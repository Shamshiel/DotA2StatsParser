using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff.Interfaces
{
    public interface IPopularity
    {
        /// <summary>
        /// Gets the poularity value.
        /// </summary>
        /// <value>
        /// The poularity value.
        /// </value>
        int Value { get; }
    }
}
