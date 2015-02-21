using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace MegaCityOne
{
    /// <summary>
    /// Exception launched by a Judge while Enforcing a Law.
    /// </summary>
    public class LawgiverException : SecurityException, ISerializable
    {
        /// <summary>
        /// Instanciate a basic LawgivedException.
        /// </summary>
        public LawgiverException() : base() { }

        /// <summary>
        /// Instanciate a LawgiverException with the given message.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public LawgiverException(string message) : base(message) { }

        /// <summary>
        /// Instanciate a Lawgiver exception with the given message and inner Exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public LawgiverException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Instanciate a LawgiverException in a deserialization context.
        /// </summary>
        /// <param name="info">The serialization information.</param>
        /// <param name="context">The streaming context</param>
        protected LawgiverException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
