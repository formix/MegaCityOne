using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyWithJusticeDepartment
{
    public class BankAccount
    {
        public string Owner { get; set; }
        public decimal Balance { get; private set; }

        public BankAccount()
        {
            this.Balance = 0;
        }

        public void Deposit(decimal amount)
        {
            this.Balance += amount;
        }

        public void Withdraw(decimal amount) {
            this.Balance -= amount;
        }
    }
}
