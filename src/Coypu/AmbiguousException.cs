using System;
using System.Runtime.Serialization;

namespace Coypu
{
    /// <summary>
    /// 
    /// </summary>
    public class AmbiguousException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public AmbiguousException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public AmbiguousException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public AmbiguousException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}