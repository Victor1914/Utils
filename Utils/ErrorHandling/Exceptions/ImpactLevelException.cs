namespace Utils.ErrorHandling.Exceptions
{
    using System;
    using Data;
    using Microsoft.Extensions.Logging;
    using Responses;
    using Utils.Data;

    public abstract class ImpactLevelException : SystemException
    {
        protected ImpactLevelException(Exception initialException = null) : base(initialException) { }

        public virtual LogLevel LogLevel { get; protected set; } = LogLevel.Error;

        public virtual LogSeverity Severity { get; protected set; } = LogSeverity.High;

        public override ErrorResponse CreateResponse()
        {
            var response = base.CreateResponse();

            response.ImpactLevelDetails = new ImpactLevelDetails
            {
                LogLevel = LogLevel,
                Severity = Severity
            };

            return response;
        }
    }
}
