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
    /// server. This Judge is still a serverside component. Do not think that
    /// it runs within a browser just because thw word "Javascript" was used 
    /// in this summary. Note that the Javascript file used to define Laws 
    /// shall be outside of the reach of your web server (IIS I guess) in a 
    /// tightly secured folder.
    /// </summary>
    public class JudgeAnderson : AbstractJudge
    {

        #region Internal Types

        public delegate void JsMessageDelegate(object messageData);

        #endregion

        #region Events

        public event MessageDelegate EngineMessage;

        #endregion

        #region Fields

        private Jint.Engine engine;

        #endregion

        #region Properties

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
        public JudgeAnderson()
        {
            this.engine = null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads a law script from the specified file.
        /// </summary>
        /// <param name="file">The file containing the javascript law 
        /// applied to the current principal.</param>
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

            string script = null;
            using (var sr = file.OpenText())
            {
                script = sr.ReadToEnd();
            }

            if (script == null || script.Trim() == "")
            {
                throw new InvalidDataException("Cannot initialize the security engine with an empty laws file: " + file.FullName);
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
            this.engine.SetValue("message", new JsMessageDelegate(o => OnEngineMessage(new MessageEventArgs(o.ToString()))));
            this.engine.Execute(script);
        }

        /// <summary>
        /// Gives an advice based on a law defined in a JavaScript file.
        /// See <see cref="MegaCityOne.IJudge.Advise"/> for more details.
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
