using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MegaCityOne.Tests
{
    /// <summary>
    /// Test if JudgeDredd features works properly.
    /// </summary>
    [TestClass]
    public class JudgeDreddTests
    {

        /// <summary>
        /// Test to see if a simple law works well.
        /// </summary>
        [TestMethod]
        public void SimpleCompiledRuleSuccess()
        {
            JudgeDredd judge = new JudgeDredd();
            judge.Laws.Add("CanSeeBankAccount", 
                (principal, arguments) => principal.IsInRole("BankingConsultant"));

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
        public void SimpleCompiledRuleFail()
        {
            JudgeDredd judge = new JudgeDredd();
            judge.Laws.Add("CanSeeBankAccount",
                (principal, arguments) => principal.IsInRole("BankingConsultant"));

            judge.Principal = new GenericPrincipal(
                WindowsIdentity.GetCurrent(), new string[0]);

            judge.Enforce("CanSeeBankAccount");
        }

        /// <summary>
        /// Loads a set of law from a foreign library.
        /// </summary>
        [TestMethod]
        public void LoadLawsFromAnotherAssembly()
        {
            JudgeDredd judge = new JudgeDredd();
            judge.Load("../../../MegaCityOne.Tests.JusticeDepartment/bin/debug/MegaCityOne.Tests.JusticeDepartment.dll");
            Assert.AreEqual(3, judge.Laws.Count);
        }
    }
}
