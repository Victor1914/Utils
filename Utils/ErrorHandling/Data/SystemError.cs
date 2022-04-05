namespace Utils.ErrorHandling.Data
{
    using System;

    public class SystemError
    {
        public string ClassName { get; set; }

        public string MethodName { get; set; }

        public int? LineNumber { get; set; }

        public Exception InitialException { get; set; }
    }
}
