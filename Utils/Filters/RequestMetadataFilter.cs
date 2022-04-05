namespace Utils.Filters
{
    using ErrorHandling.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class RequestMetadataFilter : TypeFilterAttribute
    {
        public RequestMetadataFilter() : base(typeof(CustomRequestHeadersFilter)) { }

        private class CustomRequestHeadersFilter : ActionFilterAttribute
        {
            private readonly IRequestMetadataService _requestMetadataRetriever;

            public CustomRequestHeadersFilter(IRequestMetadataService requestMetadataRetriever)
            {
                _requestMetadataRetriever = requestMetadataRetriever;
            }

            public override void OnActionExecuting(ActionExecutingContext context)
            {
                _requestMetadataRetriever.ExtractMetadata(context.HttpContext.Request);
            }
        }
    }
}
