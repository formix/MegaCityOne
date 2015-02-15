using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaCityOne
{
    /// <summary>
    /// An event argument containing a message sent by the Javascript function "message".
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the message content.
        /// </summary>
        public string Message { get; private set; }

        public MessageEventArgs() : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of JsEngineMessageEventArgs.
        /// </summary>
        /// <param name="message">The message received from the internal Javascript engine.</param>
        public MessageEventArgs(string message)
        {
            this.Message = message;
        }

        public override string ToString()
        {
            return string.Format("Engine message: {0}", this.Message);
        }
    }

    /// <summary>
    /// Delegate for JsEngineMessage passing.
    /// </summary>
    /// <param name="source">The source object of the message.</param>
    /// <param name="e">The JsEngineMessageEventArgs instance</param>
    public delegate void MessageDelegate(object source, MessageEventArgs e);
}
