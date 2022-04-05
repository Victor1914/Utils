namespace Utils.ErrorHandling.Responses
{
    using Data;
    using Newtonsoft.Json;

    public class ErrorResponse
    {
        public SystemError SystemError { get; set; }

        public int StatusCode { get; set; }

        public string Message { get; set; }

        [JsonIgnore]
        public ImpactLevelDetails ImpactLevelDetails { get; set; }

        [JsonIgnore]
        public int HttpStatusCode { get; set; }

        public MetadataDetails MetadataDetails { get; set; }

        public dynamic BusinessDetails { get; set; }

        [JsonIgnore]
        public bool ShowSystemDetails { get; set; } = true;

        [JsonIgnore]
        public bool ShowMetadataDetails { get; set; } = true;

        [JsonIgnore]
        public bool ShowBusinessDetails { get; set; } = true;

        public bool ShouldSerializeSystemError() => ShowSystemDetails;

        public bool ShouldSerializeMetadataDetails() => ShowMetadataDetails;

        public bool ShouldSerializeBusinessDetails() => ShowBusinessDetails;
    }
}
