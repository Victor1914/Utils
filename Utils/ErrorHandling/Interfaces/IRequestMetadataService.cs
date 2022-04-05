namespace Utils.ErrorHandling.Interfaces
{
    using Data;
    using Microsoft.AspNetCore.Http;

    public interface IRequestMetadataService
    {
        void ExtractMetadata(HttpRequest httpRequest);

        MetadataDetails MetadataDetails { get; }
    }
}
