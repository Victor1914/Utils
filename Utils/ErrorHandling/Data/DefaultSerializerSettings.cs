namespace Utils.ErrorHandling.Data
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class DefaultSerializerSettings : JsonSerializerSettings
    {
        public DefaultSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore;
            Formatting = Formatting.Indented;
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            ContractResolver = new DefaultContractResolver();
        }
    }
}
