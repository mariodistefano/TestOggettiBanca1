using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOggettiBanca
{
    internal abstract class Bank
    {
        private int totalValue;

        protected int TotalValue { get => totalValue; set => totalValue = value; }

        // deposito
        public abstract void Deposit(int depositMoney);

        // prelievo
        public abstract void withdraw(int withdrawMoney);
       

        // mostra totale
        public abstract void ShowTotalValue();
     



    }
}
