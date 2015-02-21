using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MegaCityOne
{
    /// <summary>
    /// An event argument containing a message sent by the Javascript 
    /// function "message".
    /// </summary>
    public class MessageEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the message content.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Instanciate a MessageEventArgs.
        /// </summary>
        public MessageEventArgs() : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of JsMessageEventArgs.
        /// </summary>
        /// <param name="text">The message text received from the internal 
        /// Javascript engine.</param>
        public MessageEventArgs(string text)
        {
            this.Text = text;
        }

        /// <summary>
        /// Returns a string representation of the current object.
        /// </summary>
        /// <returns>a string representation of the current object.</returns>
        public override string ToString()
        {
            return string.Format("Engine message: {0}", this.Text);
        }
    }

    /// <summary>
    /// Delegate for JsMessage passing.
    /// </summary>
    /// <param name="source">The source object of the message.</param>
    /// <param name="e">The JsMessageEventArgs instance</param>
    public delegate void MessageDelegate(object source, MessageEventArgs e);
}
