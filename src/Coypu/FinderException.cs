using System;
using System.Runtime.Serialization;

namespace Coypu
{
    /// <summary>
    /// 
    /// </summary>
    public class FinderException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public FinderException(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public FinderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public FinderException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}