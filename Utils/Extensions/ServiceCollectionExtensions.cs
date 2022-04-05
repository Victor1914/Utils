namespace Utils.Extensions
{
    using Configurations;
    using ErrorHandling.Data;
    using ErrorHandling.Interfaces;
    using ErrorHandling.Services;
    using Filters;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddErrorHandling(
            this IServiceCollection services, 
            IConfiguration configuration, 
            JsonSerializerSettings serializerSettings = null)
        {
            return services
                .Configure<ErrorHandlingConfiguration>(configuration.GetSection("ErrorHandling"))
                .Configure<MvcOptions>(options =>
                {
                    options.Filters.Add<GlobalExceptionFilter>();
                    options.Filters.Add<RequestMetadataFilter>();
                })
                .AddScoped<IRequestMetadataService, RequestMetadataService>()
                .AddSingleton(serializerSettings ?? new DefaultSerializerSettings());
        }
    }
}