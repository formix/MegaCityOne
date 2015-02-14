using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Formix.Security.Authorization
{
    public class LawgiverException : SecurityException, ISerializable
    {
        public LawgiverException() : base() { }
        public LawgiverException(string message) : base(message) { }
        public LawgiverException(string message, Exception inner) : base(message, inner) { }
        protected LawgiverException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
