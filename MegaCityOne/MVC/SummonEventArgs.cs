using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCityOne.MVC
{
    /// <summary>
    /// Event arguments for JudgeSummon event.
    /// </summary>
    public class SummonEventArgs : EventArgs
    {
        /// <summary>
        /// The Judge who answers the summoning from the Dispatcher.
        /// </summary>
        public Judge Respondent { get; set; }

        /// <summary>
        /// Creates an instance of JudgeSummonEventArgs.
        /// </summary>
        public SummonEventArgs()
        {
            this.Respondent = null;
        }
    }
}
