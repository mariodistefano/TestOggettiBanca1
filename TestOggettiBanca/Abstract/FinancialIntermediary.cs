using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEST.OOP.BankAccount;
using static TEST.OOP.BankAccount.CommertialBank.Account;
using static TestOggettiBanca.CryptoExchange;
using static TestOggettiBanca.StockMarket;
namespace TestOggettiBanca.Abstract
{
    public abstract class FinancialIntermediary
    {
        string _name;
        string _country;
        int counter;
        int _code;
        string _city;
        Asset[] _assets;
        CryptoExchange cryptoExchange;
        protected FinancialIntermediary(string name, string country,string city)
        {
            _city = city;
            _name = name;
            _country = country;
        }

        public abstract class Asset 
        {
            protected FinancialIntermediary FinancialIntermediary { get; set; }

            StockMarket _stockMarket;
            Fiat[] _fiats;
            Crypto[] _cryptos;
            string _name;
            
            public abstract decimal AmountInEuro { get; }
            public string Name { get => _name; set => _name = value; }
            internal Fiat[] Fiats { get => _fiats; set => _fiats = value; }
            internal Crypto[] Cryptos { get => _cryptos; set => _cryptos = value; }

            public Asset()
            {
            }

        }



        
        public virtual Asset BuyStock(STOCK STOCK, int Amount, StockMarket stockMarket ,string account)
        {
            return stockMarket.BuyStock(STOCK, Amount, stockMarket, account);
        }

     


    }
}
