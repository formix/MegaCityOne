using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MegaCityOne.Mvc
{
    /// <summary>
    /// The dispatcher is responsible to check if a Judge is available for 
    /// the call. If no Judge is available, a Judge will be summoned. 
    /// Dispatched Judges must be returned to the pool by using the 
    /// Dispatcher.Return method. Otherwise, the Dispatch method will summon 
    /// a new Judge on each call. The Dispatcher as a JudgePool. This class is 
    /// a singleton and cannot be instanciated. You must use the static 
    /// member Dispatcher.Current to use an instance of this class.
    /// </summary>
    public sealed class Dispatcher
    {
        #region Events

        /// <summary>
        /// Event fired when there is no Judge available for the current 
        /// thread id. The event handler is expected to create a Judge,
        /// provide it with laws and attach it the the event args.
        /// </summary>
        public event SummonDelegate Summon;

        #endregion

        #region Fields

        private static Dispatcher current = null;

        private Stack<Judge> judgePool;
        private HashSet<int> dispatchedJudges;

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
            this.judgePool = new Stack<Judge>();
            this.dispatchedJudges = new HashSet<int>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Thread safe. Calling the dispatch method can trigger the Summon 
        /// event if there is no Judge available in the pool. If this 
        /// is the case, it is assumed that a Summon event handler will create 
        /// a Judge and asign it to the SummonEventArgs.Respondent property. 
        /// Otherwise, return an existing Judge from the pool.
        /// </summary>
        /// <returns>A Judge available to answer the call.</returns>
        public Judge Dispatch()
        {
            Judge judge = null;
            lock (this.judgePool)
            {
                if (this.judgePool.Count == 0)
                {
                    SummonEventArgs e = new SummonEventArgs();
                    this.OnSummon(e);
                    if (e.Respondent == null)
                    {
                        throw new InvalidOperationException("The Judge summoning returned null.");
                    }
                    this.judgePool.Push(e.Respondent);
                }
                judge = this.judgePool.Pop();
                this.dispatchedJudges.Add(judge.GetHashCode());
            }
            return judge;
        }

        /// <summary>
        /// Thread safe. Returns a dispatched judge to the pool. This method 
        /// do not accept a judge that have not been dispatched by the 
        /// current instance of the dispatcher.
        /// </summary>
        /// <param name="judge">The judge that answered a previous call to 
        /// Dispatch.</param>
        public void Returns(Judge judge)
        {
            lock (this.judgePool)
            {
                if (judge == null)
                {
                    throw new ArgumentNullException("judge");
                }

                if (!this.dispatchedJudges.Contains(judge.GetHashCode()))
                {
                    throw new ArgumentException(
                        "The judge received have not been dispatched by an " +
                        "earlier call to Dispatch()");
                }

                this.dispatchedJudges.Remove(judge.GetHashCode());
                this.judgePool.Push(judge);
            }
        }

        /// <summary>
        /// Method used to fire a Summon event.
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
