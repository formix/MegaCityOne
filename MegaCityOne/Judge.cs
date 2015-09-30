using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace MegaCityOne
{
    /// <summary>
    /// Defining a security engine basic interface.
    /// </summary>
    public interface Judge
    {
        /// <summary>
        /// The principal assigned to the current Judge.
        /// </summary>
        IPrincipal Principal { get; set; }

        /// <summary>
        /// Returns all rule names useable by the current Judge.
        /// </summary>
        ICollection<string> Rules { get; }

        /// <summary>
        /// The Judge gives an adivce regarding a law taking in account some 
        /// optional arguments for the current Principal.
        /// </summary>
        /// <param name="law">The law to be advised.</param>
        /// <param name="arguments">Any system state that could help the 
        /// Judge to give a relevant advice regarding the law in question.</param>
        /// <returns>True if the advised law is respected for the current Principal, 
        /// given optional arguments. False otherwise.</returns>
        bool Advise(string law, params object[] arguments);

        /// <summary>
        /// The Judge is in a situation where he have to give a Life or Death 
        /// sentence regarding a law, given optional arguments provided, for 
        /// the current Principal.
        /// </summary>
        /// <param name="law">The law to be enforced.</param>
        /// <param name="arguments">Any system state that could help the 
        /// Judge to give a relevant sentence regarding the law in question.</param>
        /// <exception cref="MegaCityOne.LawgiverException">
        /// Inherited from System.Security.SecurityException. 
        /// This exceptions is thrown at the face of current Principal if he 
        /// is breaking the given law.</exception>
        void Enforce(string law, params object[] arguments);
    }
}
