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

namespace MegaCityOne
{
    /// <summary>
    /// JudgeAnderson runs an internal JavaScript interpreter (namely Jint)
    /// to execute laws that have been defined in a Javacript file on the 
    /// server.
    /// </summary>
    public class JudgeAnderson : AbstractJudge
    {

        #region Internal Types

        private delegate void JsMessageDelegate(object messageData);

        #endregion

        #region Events

        /// <summary>
        /// Event launched when the "message(text)" function is called from 
        /// the JavaScript.
        /// </summary>
        public event MessageDelegate Message;

        #endregion

        #region Fields

        private Jint.Engine engine;

        #endregion

        #region Properties

        public virtual bool IsInitialized 
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
        public JudgeAnderson()
        {
            this.engine = null;
        }

        #endregion

        #region Methods


        /// <summary>
        /// Adds an object to the JavaScript engine.
        /// </summary>
        /// <param name="name">How the object shall be called in the script.</param>
        /// <param name="target">The object reference to be added.</param>
        public virtual void AddObject(string name, object target)
        {
            if (!this.IsInitialized)
            {
                throw new InvalidOperationException("Cannot add an object the Javascript Engine befaur loading a script!");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Argument 'name' cannot be null, empty or contains only spaces");
            }

            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            this.engine.SetValue(name, target);
        }

        /// <summary>
        /// Loads a law script from the specified file.
        /// </summary>
        /// <param name="file">The file containing the javascript law 
        /// to be loaded in the current principal.</param>
        public virtual void Load(FileInfo file)
        {
            if (file == null)
            {
                throw new ArgumentNullException("file");
            }

            if (!file.Exists)
            {
                throw new FileNotFoundException("Cannot initialize security engine. The laws file does not exists: " + file.FullName);
            }

            using (TextReader reader = file.OpenText())
            {
                this.Load(reader);
            }
        }


        /// <summary>
        /// Loads a JavaScript law file definition.
        /// </summary>
        /// <param name="reader">The reader containing the JavaScript code.
        /// </param>
        public virtual void Load(TextReader reader)
        {
            string script = null;
            script = reader.ReadToEnd();

            if (script == null || script.Trim() == "")
            {
                throw new InvalidDataException("The provided stream is empty.");
            }

            this.Load(script);
        }


        /// <summary>
        /// Loads the law script received.
        /// </summary>
        /// <param name="script">The javascript laws applied to the current 
        /// principal.</param>
        public virtual void Load(string script)
        {
            if (string.IsNullOrEmpty(script.Trim()))
            {
                throw new ArgumentException("the 'script' parameter cannot be null or empty.");
            }

            this.engine = new Jint.Engine();
            this.engine.SetValue("message", new JsMessageDelegate(o => OnMessage(new MessageEventArgs(o.ToString()))));
            this.engine.Execute(script);
        }

        /// <summary>
        /// Gives an advice based on a law defined in a JavaScript file.
        /// See <see cref="MegaCityOne.Judge.Advise"/> for more details.
        /// </summary>
        /// <param name="law"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public override bool Advise(string law, params object[] arguments)
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

        /// <summary>
        /// Raise the Message event.
        /// </summary>
        /// <param name="">The event data</param>
        protected virtual void OnMessage(MessageEventArgs e)
        {
            if (this.Message != null)
            {
                this.Message(this, e);
            }
        }

        #endregion
    }
}
