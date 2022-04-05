namespace Utils.ErrorHandling.Exceptions
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Data;
    using Responses;

    public abstract class SystemException : Exception
    {
        protected SystemException(Exception initialException)
        {
            InitialException = initialException;
            ExtractExceptionData();
        }

        public string ClassName { get; private set; }

        public string MethodName { get; private set; }

        public int? LineNumber { get; private set; }

        public Exception InitialException { get; }

        public SystemException Trace([CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            ClassName = Path.GetFileNameWithoutExtension(filePath);
            MethodName = memberName;
            LineNumber = lineNumber;

            return this;
        }

        public virtual ErrorResponse CreateResponse()
        {
            return new ErrorResponse
            {
                SystemError = new SystemError
                {
                    ClassName = ClassName,
                    MethodName = MethodName,
                    LineNumber = LineNumber,
                    InitialException = InitialException
                }
            };
        }

        private void ExtractExceptionData()
        {
            var stackTrace = new StackTrace(true);
            var exceptionType = GetType();

            var isEntryPoint = false;

            for (var index = 0; index < stackTrace.FrameCount; index++)
            {
                var frame = stackTrace.GetFrame(index);
                var method = frame.GetMethod();

                if (isEntryPoint)
                {
                    LineNumber = frame.GetFileLineNumber();

                    if (ExtractAsyncMethodData(method))
                        break;

                    ExtractMethodData(method);
                    break;
                }

                if (method.DeclaringType == exceptionType)
                    isEntryPoint = true;
            }
        }

        private void ExtractMethodData(MemberInfo method)
        {
            var className = method.DeclaringType?.Name ?? "";
            ClassName = ExtractClassName(className);
            MethodName = method.Name;
        }

        private bool ExtractAsyncMethodData(MemberInfo method)
        {
            var methodType = method.DeclaringType;

            var isMethodAsync = methodType?.GetInterface("IAsyncStateMachine") != null;
            if (!isMethodAsync)
                return false;

            var className = methodType.ReflectedType?.Name;
            ClassName = ExtractClassName(className);
            MethodName = methodType.FullName?.Split('<', '>')[1];

            return true;
        }

        private static string ExtractClassName(string rawClassName)
        {
            return rawClassName.Contains("`")
                ? rawClassName.Split("`")[0]
                : rawClassName;
        }
    }
}
