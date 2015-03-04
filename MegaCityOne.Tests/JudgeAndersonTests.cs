using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MegaCityOne.Tests
{
    /// <summary>
    /// Test if JudgeAnderson features works properly.
    /// </summary>
    [TestClass]
    public class JudgeAndersonTests
    {

        /// <summary>
        /// Test to see if a simple law works well.
        /// </summary>
        [TestMethod]
        public void SimpleJavaScriptRuleSuccess()
        {
            JudgeAnderson judge = new JudgeAnderson();
            judge.Load(@"function CanSeeBankAccount(principal) {
                return principal.IsInRole('BankingConsultant'); 
            }");

            judge.Principal = new GenericPrincipal(
                WindowsIdentity.GetCurrent(),
                new string[] { "BankingConsultant" });

            judge.Enforce("CanSeeBankAccount");
        }


        /// <summary>
        /// Insure that an Advise failure throws an LawgiverException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(LawgiverException))]
        public void SimpleJavaScriptRuleFail()
        {
            JudgeAnderson judge = new JudgeAnderson();
            judge.Load(@"function CanSeeBankAccount(principal) {
                return principal.IsInRole('BankingConsultant'); 
            }");

            judge.Principal = new GenericPrincipal(
                WindowsIdentity.GetCurrent(), new string[0]);

            judge.Enforce("CanSeeBankAccount");
        }

        /// <summary>
        /// Loads a set of law from a Javascript Law definition file.
        /// This method shall not thow any LawgiverException.
        /// </summary>
        [TestMethod]
        public void LoadLawsFromJavaScriptFile()
        {
            JudgeAnderson judge = new JudgeAnderson();
            judge.Load(new FileInfo("JusticeDepartment.js"));

            judge.Principal = new GenericPrincipal(
                    new GenericIdentity("rico"), 
                    new string[] { "BankUser" });

            object account = new
            {
                Owner = "rico",
                Balance = decimal.Zero
            };

            judge.Enforce("CanWithdrawFromAccount", account);
            judge.Enforce("CanDepositToAccount", account);
            judge.Enforce("CanDisplayBalance", account);
        }
    }
}
