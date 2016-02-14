using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.Dotabuff.Interfaces
{
    public interface IWinRate
    {
        /// <summary>
        /// Gets the value of the win rate.
        /// </summary>
        /// <value>
        /// The value of the win rate.
        /// </value>
        float Value { get; }
    }
}
