using System;

namespace TestOggettiBanca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SwiftCentralBank Swift = new SwiftCentralBank();
            CentralBanck russianBank = new CentralBanck();
            CommercialBanck russianCommercialBanck = new CommercialBanck(russianBank);

            BanckAccount MarioBanckAccount = new BanckAccount(russianCommercialBanck);

            //russianCommercialBanck.Deposit(5522225);
            //russianCommercialBanck.withdraw(2132342);
            //russianCommercialBanck.ShowTotalValue();
        }
    }
}
