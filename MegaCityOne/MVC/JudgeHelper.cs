using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaCityOne.Mvc
{
    /// <summary>
    /// This static Judge is intended to be used as a Razor helper. It gets an available Judge from the Dispatcher and It can 
    /// only advise.
    /// </summary>
    public static class JudgeHelper
    {
        /// <summary>
        /// Static method to be used inside a Razor rendered web page.
        /// </summary>
        /// <param name="law">The law to be advized</param>
        /// <param name="arguments">Optional arguments to hel the Judge give 
        /// an advice.</param>
        /// <returns></returns>
        public static bool Advise(string law, params object[] arguments)
        {
            Judge judge = Dispatcher.Current.Dispatch();
            bool advisal = judge.Advise(law, arguments);
            Dispatcher.Current.Returns(judge);
            return advisal;
        }
    }
}
