using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MegaCityOne.Tests.Judges;
using System.Threading;
using System.Security.Principal;

namespace MegaCityOne.Tests
{
    /// <summary>
    /// Tests basic features provided through the AbstractJudge.
    /// </summary>
    [TestClass]
    public class AbstractJudgeTests
    {
        /// <summary>
        /// Check if the current Judge is able to identify the Principal from the current thread.
        /// </summary>
        [TestMethod]
        public void TestPrincipalWithCurrentUser()
        {
            var judge = new JudgeDummy();
            var expectedUserName = getCurrentUserName();
            Assert.AreEqual(expectedUserName, judge.Principal.Identity.Name);
        }

        /// <summary>
        /// Get the current user name using environment variables.
        /// </summary>
        /// <returns></returns>
        private string getCurrentUserName()
        {
            return string.Format("{0}\\{1}",
                Environment.GetEnvironmentVariable("USERDOMAIN"),
                Environment.GetEnvironmentVariable("USERNAME"));
        }

        /// <summary>
        /// Verifies that the Judge is able to get a principal different from the default WindowsPrincipal
        /// that have been previously associated with the current thread.
        /// </summary>
        [TestMethod]
        public void TestThreadOverridenPrincipal()
        {
            IPrincipal initialPrincipal = Thread.CurrentPrincipal;
            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("vienna"), new string[0]);
            var judge = new JudgeDummy();
            var judgePrincipal = judge.Principal;
            Thread.CurrentPrincipal = initialPrincipal;
            Assert.AreEqual("vienna", judgePrincipal.Identity.Name);
        }

        /// <summary>
        /// Verifies that a Judge can switch his attention from the current thread principal to another
        /// Principal and then back to the current thread principal.
        /// </summary>
        [TestMethod]
        public void TestJudgeOverridenPrincipal()
        {
            var judge = new JudgeDummy();
            
            judge.Principal = new GenericPrincipal(new GenericIdentity("rico"), new string[0]);
            Assert.AreEqual("rico", judge.Principal.Identity.Name);

            judge.Principal = null;
            var expectedUserName = getCurrentUserName();
            Assert.AreEqual(expectedUserName, judge.Principal.Identity.Name);
        }

        /// <summary>
        /// Checks that no exception is thrown by the Judge.Enforce method when the 
        /// Judge.Advise method returns true.
        /// </summary>
        [TestMethod]
        public void TestJudgeEnforcePass()
        {
            // The default Advise implementation for JudgeDummy is to always return true.
            var judgeDummy = new JudgeDummy();
            judgeDummy.Enforce("any-law");
        }


        /// <summary>
        /// Insure that a LawgiverException is thrown by calling 
        /// Judge.Enforce when the Judge.Advise method returns false.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(LawgiverException))]
        public void TestJudgeEnforceFail()
        {
            // JudgeDummy evil twin that always Advise to false (just like 
            //the real Judge Death)
            var judgeDeath = new JudgeDummy((l, a) => false);
            judgeDeath.Enforce("any-law");
        }
    }
}
