using System;
using System.Runtime.Serialization;

namespace Coypu
{
    /// <summary>
    /// Thrown whenever some expected HTML cannot be found
    /// </summary>
    public class MissingDialogException : FinderException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public MissingDialogException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public MissingDialogException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public MissingDialogException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}