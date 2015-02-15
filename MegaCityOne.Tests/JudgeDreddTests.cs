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
        /// Test to see if a simple law woks well.
        /// </summary>
        [TestMethod]
        public void TestSimpleCompiledRuleSuccess()
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
        /// Insure that a Advise failure throws an LawgiverException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(LawgiverException))]
        public void TestSimpleCompiledRuleFail()
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
            judge.Load("../../../AssemblyWithJusticeDepartment/bin/debug/AssemblyWithJusticeDepartment.dll");
            Assert.AreEqual(3, judge.Laws.Count);
        }
    }
}
