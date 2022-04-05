namespace Utils.ErrorHandling.Services
{
    using AutoMapper.Internal;
    using Data;
    using Interfaces;
    using Microsoft.AspNetCore.Http;

    public class RequestMetadataService : IRequestMetadataService
    {
        private const string HeaderPrefix = "X-Platform-";

        public MetadataDetails MetadataDetails { get; private set; }

        public void ExtractMetadata(HttpRequest httpRequest)
        {
            var headers = httpRequest.Headers;

            var timeZone = 0;
            if (int.TryParse(headers.GetOrDefault($"{HeaderPrefix}TZ"), out var timeZoneHeader))
                timeZone = timeZoneHeader;

            MetadataDetails = new MetadataDetails
            {
                BusinessUnit = headers.GetOrDefault($"{HeaderPrefix}BU"),
                Origin = headers.GetOrDefault($"{HeaderPrefix}Origin"),
                Language = headers.GetOrDefault($"{HeaderPrefix}Lang"),
                Device = headers.GetOrDefault($"{HeaderPrefix}Device"),
                TimeZone = timeZone,
                Target = httpRequest.Path
            };
        }
    }
}
