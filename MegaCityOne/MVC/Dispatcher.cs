using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MegaCityOne.MVC
{
    /// <summary>
    /// The dispatcher is responsible to check if a Judge is available for 
    /// the current thread. If no Judge is available, a Judge will be 
    /// summoned and assigned to the thread ID for later usage. You can see 
    /// the Dispatcher as a JudgePool. This class is a singleton and cannot 
    /// be instanciated. You must use the static member "Current" to use this 
    /// class.
    /// </summary>
    public sealed class Dispatcher
    {
        #region Events

        /// <summary>
        /// Event fired when there is no Judge available for the current 
        /// thread id.
        /// </summary>
        public event SummonDelegate Summon;

        #endregion

        #region Fields

        private static Dispatcher current = null;

        private IDictionary<int, Judge> judges;

        #endregion

        #region Properties

        /// <summary>
        /// The static dispatcher instance for the current application.
        /// </summary>
        public static Dispatcher Current
        {
            get
            {
                if (current == null)
                {
                    current = new Dispatcher();
                }
                return current;
            }
        }

        #endregion

        #region Constructors

        private Dispatcher()
        {
            this.judges = new Dictionary<int, Judge>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Calling the dispatch method can trigger the JudgeSummon event if 
        /// there is no Judge associated with the calling thread id. If this 
        /// is the case, it is assumed that an event handler will create a 
        /// Judge and asign it to the JudgeSummonEventArgs.Respondent property 
        /// for later use with the given thread. Otherwise, return the 
        /// existing Judge associated with the calling thread id.
        /// </summary>
        /// <returns>The designated Judge for the calling thread id.</returns>
        public Judge Dispatch()
        {
            Judge judge = null;
            lock (this.judges) {
                if (!this.judges.ContainsKey(Thread.CurrentThread.ManagedThreadId)) {
                    SummonEventArgs e = new SummonEventArgs();
                    this.OnSummon(e);
                    if (e.Respondent == null)
                    {
                        throw new InvalidOperationException("The Judge summoning returned null.");
                    }
                    this.judges[Thread.CurrentThread.ManagedThreadId] = e.Respondent;
                }
                judge = this.judges[Thread.CurrentThread.ManagedThreadId];
            }
            return judge;
        }

        /// <summary>
        /// Method used to fire a JudgeSummon event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        private void OnSummon(SummonEventArgs e)
        {
            if (this.Summon != null)
            {
                this.Summon(this, e);
            }
        }

        #endregion
    }
}
