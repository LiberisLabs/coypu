using System;
using System.Runtime.Serialization;

namespace Coypu
{
    /// <summary>
    /// Thrown whenever an expected browser window cannot be found
    /// </summary>
    public class MissingWindowException : FinderException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public MissingWindowException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MissingWindowException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public MissingWindowException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}