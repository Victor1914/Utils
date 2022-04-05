namespace Utils.Formatters
{
    using System.Net.Mime;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Newtonsoft.Json;

    public class JsonFormatter : IOutputFormatter
    {
        private readonly JsonSerializerSettings _settings;

        public JsonFormatter(JsonSerializerSettings settings)
        {
            _settings = settings;
        }

        public bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return true;
        }

        public async Task WriteAsync(OutputFormatterWriteContext context)
        {
            var httpContext = context.HttpContext;

            httpContext.Response.ContentType = MediaTypeNames.Application.Json;
            var json = JsonConvert.SerializeObject(context.Object, _settings);

            await httpContext.Response.WriteAsync(json);
        }
    }
}