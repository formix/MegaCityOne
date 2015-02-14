using Jint;
using Jint.Native;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace Formix.Security.Authorization
{
    /// <summary>
    /// The JsSecurityEngine runs an internal JavaScript interpreter (namely 
    /// Jint) to execute laws.
    /// </summary>
    public class JsDredd : IJudge
    {

        #region Internal Types

        public delegate void JsMessageDelegate(object messageData);

        #endregion

        #region Events

        public event MessageDelegate EngineMessage;

        #endregion

        #region Fields

        private Jint.Engine engine;
        private IPrincipal principal;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the principal currently associated with this law engine.
        /// </summary>
        public virtual IPrincipal Principal {
            get
            {
                if (this.principal == null)
                {
                    return Thread.CurrentPrincipal;
                }
                else
                {
                    return this.principal;
                }
            }

            set
            {
                this.principal = value;
            }
        }

        public bool IsInitialized 
        { 
            get
            {
                return this.engine != null;
            } 
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the javascript law engine.
        /// </summary>
        /// <param name="principal"></param>
        public JsDredd()
        {
            this.engine = null;
            this.principal = null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads a law script from the specified file.
        /// </summary>
        /// <param name="file">The file containing the javascript law 
        /// applied to the current principal.</param>
        public virtual void Initialize(FileInfo file)
        {
            if (file == null)
            {
                throw new ArgumentNullException("file");
            }

            if (!file.Exists)
            {
                throw new FileNotFoundException("Cannot initialize security engine. The laws file does not exists: " + file.FullName);
            }

            string script = null;
            using (var sr = file.OpenText())
            {
                script = sr.ReadToEnd();
            }

            if (script == null || script.Trim() == "")
            {
                throw new InvalidDataException("Cannot initialize the security engine with an empty laws file: " + file.FullName);
            }

            this.Initialize(script);
        }


        /// <summary>
        /// Loads the law script received.
        /// </summary>
        /// <param name="script">The javascript laws applied to the current 
        /// principal.</param>
        public virtual void Initialize(string script)
        {
            if (string.IsNullOrEmpty(script.Trim()))
            {
                throw new ArgumentException("the 'script' parameter cannot be null or empty.");
            }

            this.engine = new Jint.Engine();
            this.engine.SetValue("message", new JsMessageDelegate(o => OnEngineMessage(new MessageEventArgs(o.ToString()))));
            this.engine.Execute(script);
        }


        public virtual bool Advise(string law, params object[] arguments)
        {
            if (this.engine == null)
            {
                throw new InvalidOperationException("Laws definition not loaded. Plead load laws prior calling this method.");
            }
            if (string.IsNullOrEmpty(law.Trim()))
            {
                throw new ArgumentException("The 'law' parameter cannot be null or empty.");
            }

            var args = new object[arguments.Length + 1];
            args[0] = this.Principal;
            for (int i = 0; i < arguments.Length; i++)
            {
                args[i + 1] = arguments[i];
            }

            JsValue lawResult = this.engine.Invoke(law, args);
            return lawResult.AsBoolean();
        }


        public virtual void Enforce(string law, params object[] arguments)
        {
            if (!this.Advise(law, arguments))
            {
                string message = "Failed law advice for user: " +
                        this.Principal.Identity.Name +
                        " and law: " + law;
                if (arguments.Length > 0)
                {
                    message += " with the following arguments: " + arguments.ToString();
                }
                throw new LawgiverException(message);
            }
        }

        /// <summary>
        /// Raise the Message event.
        /// </summary>
        /// <param name="">The event data</param>
        protected virtual void OnEngineMessage(MessageEventArgs e)
        {
            if (this.EngineMessage != null)
            {
                this.EngineMessage(this, e);
            }
        }

        #endregion
    }
}
