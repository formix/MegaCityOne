using Jint;
using Jint.Native;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace MegaCityOne
{
    /// <summary>
    /// JudgeAnderson runs an internal JavaScript interpreter to execute laws
    /// that have been defined in a Javacript file on the server.
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
        private HashSet<string> initialGlobals;

        #endregion

        #region Properties

        /// <summary>
        /// Tells if JudgeAnderson internal script engine is ready to process
        /// Laws. Returns true after a Law file have been loaded. Returns 
        /// false otherwise.
        /// </summary>
        public virtual bool IsInitialized 
        { 
            get
            {
                return this.engine != null;
            } 
        }


        /// <summary>
        /// Returns all rule names in a collection.
        /// </summary>
        public override ICollection<string> Rules
        {
            get
            {
                return this.GetScriptFunctionInstances();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the javascript law engine.
        /// </summary>
        public JudgeAnderson()
        {
            this.engine = null;
            this.initialGlobals = new HashSet<string>();
        }

        #endregion

        #region Methods


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

            this.initialGlobals = new HashSet<string>(this.engine.Global.Properties.Select(p => p.Key));
            this.engine.Execute(script);
        }

        /// <summary>
        /// Gives an advice based on a law defined in a JavaScript file.
        /// See MegaCityOne.Judge.Advise for more details.
        /// </summary>
        /// <param name="law">The law to be advised.</param>
        /// <param name="arguments">Any system state that could help the 
        /// Judge to give a relevant advice regarding the law in question.</param>
        /// <returns>True if the advised law is respected for the current Principal, 
        /// given optional arguments. False otherwise.</returns>
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

            if (!this.Principal.Identity.IsAuthenticated)
            {
                return false;
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
        /// Adds an object to the JavaScript engine.
        /// </summary>
        /// <param name="name">How the object shall be called in the script.</param>
        /// <param name="target">The object reference to be added.</param>
        public virtual void AddObject(string name, object target)
        {
            if (!this.IsInitialized)
            {
                throw new InvalidOperationException("Cannot add an object to the Javascript Engine befor loading a script!");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Argument 'name' cannot be null, empty or contain any spaces");
            }

            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            this.engine.SetValue(name, target);
        }

        /// <summary>
        /// Raise the Message event.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnMessage(MessageEventArgs e)
        {
            if (this.Message != null)
            {
                this.Message(this, e);
            }
        }


        private ICollection<string> GetScriptFunctionInstances()
        {
            if (this.engine == null)
            {
                return new List<string>();
            }
            HashSet<string> rules = new HashSet<string>();
            foreach (var item in this.engine.Global.Properties)
            {
                if (!this.initialGlobals.Contains(item.Key))
                {
                    if (item.Value.Value.ToString().Contains("function"))
                    {
                        rules.Add(item.Key);
                    }
                }
            }
            return rules;
        }

        #endregion
    }
}
