using System;
using System.Collections.Generic;

namespace API.Extensions.HealthChecks
{
    public class HealthCheckResult
    {
        public string Status { get; set; }
        public IEnumerable<IndividualHealthCheckResult> Checks { get; set; }
        public TimeSpan Duration { get; set; }
    }
}