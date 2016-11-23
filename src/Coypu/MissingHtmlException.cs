using System;
using System.Runtime.Serialization;

namespace Coypu
{
    /// <summary>
    /// Thrown whenever some expected HTML cannot be found
    /// </summary>
    public class MissingHtmlException : FinderException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public MissingHtmlException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MissingHtmlException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public MissingHtmlException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}