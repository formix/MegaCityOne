using MegaCityOne;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyWithJusticeDepartment
{
    public class MyJusticeDepartment : JusticeDepartment
    {
        public IDictionary<string, Law> GetLaws()
        {
            var laws = new Dictionary<string, Law>();
            laws.Add("CanWithdrawFromAccount", (principal, arguments) =>
                principal.IsInRole("BankUser") &&
                    (((BankAccount)arguments[0]).Owner == principal.Identity.Name));

            laws.Add("CanDepositToAccount", (principal, arguments) =>
                principal.IsInRole("BankUser"));

            laws.Add("CanDisplayBalance", (principal, arguments) =>
                principal.IsInRole("BankCounselor") || 
                principal.IsInRole("BankUser") &&
                (((BankAccount)arguments[0]).Owner == principal.Identity.Name));

            return laws;
        }
    }
}
