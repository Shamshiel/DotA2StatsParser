using DotA2StatsParser.Model.HealthCheck.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotA2StatsParser.Model.HealthCheck
{
    internal class HealthCheckResult : IHealthCheckResult
    {
        public bool IsYaspAvailable { get; internal set; }
        public bool IsDotabuffAvailable { get; internal set; }

        public bool AreAllSystemsLive { get { return IsYaspAvailable && IsDotabuffAvailable; } }

        internal HealthCheckResult()
        {

        }

        internal HealthCheckResult(bool isYaspAvailable, bool isDotabuffAvailabl)
        {
            IsYaspAvailable = isYaspAvailable;
            IsDotabuffAvailable = isDotabuffAvailabl;
        }
    }
}
