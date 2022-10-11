using System;

namespace IcoCovid.Extensions
{
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Gets the inner exception message iterating through all the inner exceptions. If the inner exception is null, it will return the last found error message.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetInnerExceptionMessage(this Exception ex)
        {
            Exception innerException = ex.InnerException;
            Exception lastInnerException = ex.InnerException;
            if (innerException == null) return ex.Message;
            while (innerException != null)
            {
                if (innerException.InnerException == null) lastInnerException = innerException;
                innerException = innerException.InnerException;
            }
            return lastInnerException.Message;
        }
    }
}