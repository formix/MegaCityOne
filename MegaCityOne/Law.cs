using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MegaCityOne
{
    /// <summary>
    /// Delegate used ot define a Law useable by JudgeDredd.
    /// </summary>
    /// <param name="principal">The targetted principal for the given Law
    /// </param>
    /// <param name="arguments">An array of arguments given to the Law. Can 
    /// be an empty array and content depends on the Given Law 
    /// implementation.</param>
    /// <returns>True if the Law is respected, false otherwise.</returns>
    public delegate bool Law(IPrincipal principal, object[] arguments);
}
