namespace Utils.ErrorHandling.Exceptions
{
    using System;
    using Responses;

    public abstract class RestfulApiException : ApplicationException
    {
        protected RestfulApiException(Exception initialException = null) : base(initialException) { }

        public virtual int HttpStatusCode { get; protected set; }

        public override ErrorResponse CreateResponse()
        {
            var response =  base.CreateResponse();

            response.HttpStatusCode = HttpStatusCode;

            return response;
        }
    }
}
