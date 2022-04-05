namespace Utils.Data
{
    using System;

    public class ExternalCommunicationData
    {
        public Uri BaseUri { get; set; }

        public string RequestMethod { get; set; }

        public dynamic RequestPayload { get; set; }

        public dynamic ResponseData { get; set; }

        public string AdditionalInfo { get; set; }
    }
}
