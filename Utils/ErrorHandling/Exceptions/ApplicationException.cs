namespace Utils.ErrorHandling.Exceptions
{
    using System;
    using Responses;

    public abstract class ApplicationException : ImpactLevelException
    {
        protected ApplicationException(Exception initialException = null) : base(initialException) { }

        public virtual int StatusCode { get; protected set; }

        public override string Message { get; }

        public override ErrorResponse CreateResponse()
        {
            var response = base.CreateResponse();

            response.StatusCode = StatusCode;
            response.Message = Message;

            return response;
        }
    }
}
