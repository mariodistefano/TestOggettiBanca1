using TestOggettiBanca;
namespace TEST.OOP.BankAccount
{
    class SwiftCentralBank : CentralBank, ISwiftSystem
    {
        public SwiftCentralBank(string name, string Country, int MaxInterestRate, string city) : base(name, Country, MaxInterestRate, city)
        {

        }
    }

}

