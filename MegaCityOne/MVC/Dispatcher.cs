using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MegaCityOne.MVC
{
    public class Dispatcher
    {
        #region Internal types

        public delegate Judge Convocation();

        #endregion

        #region Fields

        private static Dispatcher current = null;

        private IDictionary<int, Judge> judges;
        private Convocation convocationMethod;

        #endregion

        #region Properties

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


        public Convocation ConvocationMethod 
        {
            get { return this.convocationMethod; }
            set { this.convocationMethod = value; }
        }

        #endregion

        #region Constructors

        private Dispatcher()
        {
            this.convocationMethod = () => null;
            this.judges = new Dictionary<int, Judge>();
        }

        #endregion

        #region Methods

        public Judge Dispatch()
        {
            Judge judge = null;
            lock (this.judges) {
                if (!this.judges.ContainsKey(Thread.CurrentThread.ManagedThreadId)) {
                    var newJudge = this.convocationMethod();
                    if (judge == null)
                    {
                        throw new InvalidOperationException("The Judge ConvocationMethod returned null or is not defined.");
                    }
                    this.judges[Thread.CurrentThread.ManagedThreadId] = newJudge;
                }
                judge = this.judges[Thread.CurrentThread.ManagedThreadId];
            }
            return judge;
        }

        #endregion
    }
}
