namespace Utils.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Data;
    using ErrorHandling.Data;
    using ErrorHandling.Responses;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public static class LoggerExtensions
    {
        public static void DumpExternalCommunication(
            this ILogger logger,
            Uri baseUri,
            string requestMethod,
            object requestPayload,
            object responseData,
            string additionalInfo = "-")
        {
            var externalCommunicationData = new ExternalCommunicationData
            {
                BaseUri = baseUri,
                RequestMethod = requestMethod,
                RequestPayload = requestPayload,
                ResponseData = responseData,
                AdditionalInfo = additionalInfo
            };

            logger.LogInformation(externalCommunicationData.Stringify(new DefaultSerializerSettings { Formatting = Formatting.None }));
        }

        public static void DumpError(this ILogger logger, ErrorResponse errorResponse)
        {
            var logMethods = new Dictionary<LogLevel, Action<string, LogSeverity, string>>
            {
                {LogLevel.Trace, (message, severity, businessUnit) => logger.LogTrace(message, severity, businessUnit)},
                {LogLevel.Debug, (message, severity, businessUnit) => logger.LogDebug(message, severity, businessUnit)},
                {LogLevel.Information, (message, severity, businessUnit) => logger.LogInformation(message, severity, businessUnit)},
                {LogLevel.Warning, (message, severity, businessUnit) => logger.LogWarning(message, severity, businessUnit)},
                {LogLevel.Error, (message, severity, businessUnit) => logger.LogError(message, severity, businessUnit)},
                {LogLevel.Critical, (message, severity, businessUnit) => logger.LogCritical(message, severity, businessUnit)}
            };

            var logMethod = logMethods.GetValueOrDefault(errorResponse.ImpactLevelDetails.LogLevel);

            logMethod(errorResponse.Stringify(new DefaultSerializerSettings { Formatting = Formatting.None }), errorResponse.ImpactLevelDetails.Severity, errorResponse.MetadataDetails?.BusinessUnit ?? "None");
        }

        public static void LogOperationResult(this ILogger logger, string operationName, object result)
        {
            logger.LogInformation(new StringBuilder()
                .AppendLine(operationName)
                .AppendLine(result.Stringify(new DefaultSerializerSettings { Formatting = Formatting.None }))
                .ToString()
            );
        }
    }
}
