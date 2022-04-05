namespace Utils.Filters
{
    using Configurations;
    using Data;
    using ErrorHandling.Data;
    using ErrorHandling.Exceptions;
    using ErrorHandling.Interfaces;
    using ErrorHandling.Responses;
    using Extensions;
    using Formatters;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;

    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IOptionsMonitor<ErrorHandlingConfiguration> _config;
        private readonly ILogger<GlobalExceptionFilter> _logger;

        private readonly IRequestMetadataService _requestMetadataService;
        private readonly JsonSerializerSettings _serializerSettings;

        public GlobalExceptionFilter(
            IOptionsMonitor<ErrorHandlingConfiguration> config,
            ILogger<GlobalExceptionFilter> logger,
            IRequestMetadataService requestMetadataService,
            JsonSerializerSettings serializerSettings)
        {
            _config = config;
            _logger = logger;
            _requestMetadataService = requestMetadataService;
            _serializerSettings = serializerSettings;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var response = new ErrorResponse
            {
                SystemError = new SystemError
                {
                    InitialException = exception
                },
                ImpactLevelDetails = new ImpactLevelDetails
                {
                    LogLevel = LogLevel.Error,
                    Severity = LogSeverity.High
                },
                StatusCode = -1,
                Message = "Internal Server Error",
                HttpStatusCode = StatusCodes.Status500InternalServerError
            };

            if (exception is RestfulApiException restfulApiException)
                response = restfulApiException.CreateResponse();

            response.MetadataDetails = _requestMetadataService.MetadataDetails;

            _logger.DumpError(response);

            response.ShowSystemDetails = _config.CurrentValue.ShowSystemDetails;
            response.ShowBusinessDetails = _config.CurrentValue.ShowBusinessDetails;
            response.ShowMetadataDetails = _config.CurrentValue.ShowMetadataDetails;

            context.Result = new ObjectResult(response)
            {
                StatusCode = response.HttpStatusCode,
                Formatters = new FormatterCollection<IOutputFormatter> { new JsonFormatter(_serializerSettings) }
            };
            context.ExceptionHandled = true;
        }
    }
}
