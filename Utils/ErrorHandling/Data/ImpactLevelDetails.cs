namespace Utils.ErrorHandling.Data
{
    using Microsoft.Extensions.Logging;
    using Utils.Data;

    public class ImpactLevelDetails
    {
        public virtual LogLevel LogLevel { get; set; }

        public virtual LogSeverity Severity { get; set; }
    }
}
