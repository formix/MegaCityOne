// BankAccountExample.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MegaCityOne.Examples
{
    class BankAccountExample
    {
        public class BankAccount
        {
            public double Balance { get; set; }
            public string Owner { get; set; }
        }


        private Judge judge;


        public BankAccountExample()
        {
            JudgeDredd judgeDredd = new JudgeDredd();

            judgeDredd.Laws.Add(
                "CanWithdrawFromAccount",
                (principal, arguments)
                    =>
                    principal.IsInRole("BankCustomer") &&
                    ((BankAccount)arguments[0]).Balance > 0 &&
                    ((BankAccount)arguments[0]).Owner == principal.Identity.Name);

            this.judge = judgeDredd;
        }


        static void Main(string[] args)
        {
            IPrincipal initialPrincipal = Thread.CurrentPrincipal;

            BankAccountExample example = new BankAccountExample();

            Console.WriteLine("-----------------------------------------");

            try
            {
                example.JamelaTheGoodCitizenWithdrawsMoney();
            }
            catch (Exception ex)
            {
                Console.WriteLine("+++++ {0} +++++", ex.GetType().Name);
                Console.WriteLine("\t" + ex.Message);
            }

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();

            try
            {
                example.KayTheRobberTriesToSteal();
            }
            catch (Exception ex)
            {
                Console.WriteLine("+++++ {0} +++++", ex.GetType().Name);
                Console.WriteLine("\t{0}\n", ex.Message);
            }

            Thread.CurrentPrincipal = initialPrincipal;

            Console.WriteLine("-----------------------------------------");

            Console.WriteLine("Press ENTER to exit...");
            Console.ReadLine();
        }


        public void JamelaTheGoodCitizenWithdrawsMoney()
        {
            // Change the principal of the current thread for Jamela, a 
            // good Mega-City One citizen.
            Thread.CurrentPrincipal = new GenericPrincipal(
                new GenericIdentity("jamela"),
                new string[] { "BankCustomer", "GoodCitizen", "MotherOfTwo" });

            // This is Jamela's bank account
            BankAccount jamelasAccount = new BankAccount()
            {
                Balance = 376.22,
                Owner = "jamela"
            };

            bool judgeAccepts = this.AdviseWithJudge(jamelasAccount);
            if (judgeAccepts)
            {
                Console.WriteLine("{0}:\n\t-Thanks!\n",
                    judge.Principal.Identity.Name);

                // Jamela withdraws money witout fearing JudgeDredd.
                this.Withdraw(jamelasAccount, 60);

                Console.WriteLine("{0}:\n\t-Have a good one Judge!\n",
                    judge.Principal.Identity.Name);
            }
            else
            {
                Console.WriteLine("{0}:\n\t-I will fill a complaint, bigot!\n",
                    judge.Principal.Identity.Name);
            }

            Console.WriteLine("-----------------------------------------");
        }

        public void KayTheRobberTriesToSteal()
        {
            // Change the principal of the current thread for Kay, a 
            // notorious Mega-City One perp. He is a bank customer as well
            // but he should not try to take someone else hard earned money...
            Thread.CurrentPrincipal = new GenericPrincipal(
                new GenericIdentity("kay"),
                new string[] { "BankCustomer", "KnownRobber", "RealBadGuy" });

            // This is Jamela's bank account
            BankAccount jamelasAccount = new BankAccount()
            {
                Balance = 316.22,
                Owner = "jamela"
            };

            bool judgeAccepts = this.AdviseWithJudge(jamelasAccount);
            if (judgeAccepts)
            {
                Console.WriteLine("{0}:\n\t-Thanks, looser!\n",
                    judge.Principal.Identity.Name);
            }
            else
            {
                Console.WriteLine("{0}:\n\t-I don't care about you!\n",
                    judge.Principal.Identity.Name);
            }

            // Kay will face his last judgement...
            this.Withdraw(jamelasAccount, 300);

            Console.WriteLine("{0}:\n\t-Have a good one sucka!\n",
                judge.Principal.Identity.Name);

            Console.WriteLine("-----------------------------------------\n");
        }


        private bool AdviseWithJudge(BankAccount account)
        {
            Console.WriteLine("{0}:\n\t-Can I get some money from {1}'s account?\n",
                this.judge.Principal.Identity.Name,
                account.Owner);

            string judgeName = this.judge.GetType().Name;
            if (this.judge.Advise("CanWithdrawFromAccount", account))
            {
                Console.WriteLine("{0}:\n\t-Go ahead citizen.\n", judgeName);
                return true;
            }
            else
            {
                Console.WriteLine("{0}:\n\t-No! be warned perp.\n", judgeName);
                return false;
            }
        }


        public void Withdraw(BankAccount account, double amount)
        {
            this.judge.Enforce("CanWithdrawFromAccount", account);

            if (account.Balance - amount < 0)
            {
                throw new InvalidOperationException(
                    "Withdrawal amount cannot exceed account balance.");
            }

            account.Balance -= amount;

            Console.WriteLine("\t *** {0} withraws ${1:0.00} from {2}'s " +
                              "account, balance: ${3:0.00} ***\n",
                this.judge.Principal.Identity.Name,
                amount,
                account.Owner,
                account.Balance);
        }
    }
}
