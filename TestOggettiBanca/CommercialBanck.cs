using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOggettiBanca
{
    internal class CommercialBanck : Bank
    {
        string _name;
        private BanckAccount _banckAccount;
        private SwiftCentralBank _swiftCentralBank;
        CentralBanck _centralBanck;
        public CommercialBanck(CentralBanck centralBanck)
        {
            centralBanck.AddCommercialBanck(this);
        }



        // deposito
        public override void Deposit(int depositMoney)
        {
            TotalValue += depositMoney;
            ShowTotalValue();
        }
        // prelievo
        public override void withdraw(int withdrawMoney)
        {
            TotalValue -= withdrawMoney;
            ShowTotalValue();
        }

        // mostra totale
        public override void ShowTotalValue()
        {
            Console.WriteLine(TotalValue);
        }


        public void AddBanckAccount(BanckAccount banckAccount, string name)
        {
            _banckAccount = banckAccount;
        }

        public void RemoveBanckAccount()
        {
            _banckAccount = null;
        }
     



    }
}
