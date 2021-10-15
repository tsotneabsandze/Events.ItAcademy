namespace API.Extensions.HealthChecks
{
    public class IndividualHealthCheckResult
    {
        public string Status { get; set; }
        public string Component { get; set; }
        public string Description { get; set; }
    }
}