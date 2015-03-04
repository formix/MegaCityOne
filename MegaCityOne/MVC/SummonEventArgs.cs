using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCityOne.Mvc
{
    /// <summary>
    /// Event arguments for a Dispatcher.Summon event.
    /// </summary>
    public class SummonEventArgs : EventArgs
    {
        /// <summary>
        /// The Judge who answers the summoning from the Dispatcher.
        /// </summary>
        public Judge Respondent { get; set; }

        /// <summary>
        /// Creates an instance of SummonEventArgs.
        /// </summary>
        public SummonEventArgs()
        {
            this.Respondent = null;
        }
    }
}
