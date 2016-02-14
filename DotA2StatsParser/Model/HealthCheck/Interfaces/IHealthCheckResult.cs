using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.HealthCheck.Interfaces
{
    public interface IHealthCheckResult
    {
        /// <summary>
        /// Gets a value indicating whether Yasp is available.
        /// </summary>
        /// <value>
        /// <c>true</c> if Yasp is available; otherwise, <c>false</c>.
        /// </value>
        bool IsYaspAvailable { get; }
        /// <summary>
        /// Gets a value indicating whether Dotabuff is available.
        /// </summary>
        /// <value>
        /// <c>true</c> if Dotabuff is available; otherwise, <c>false</c>.
        /// </value>
        bool IsDotabuffAvailable { get; }
        /// <summary>
        /// Gets a value indicating whether [all systems are live].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [all systems are live]; otherwise, <c>false</c>.
        /// </value>
        bool AreAllSystemsLive { get; }
    }
}
