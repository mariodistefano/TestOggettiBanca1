using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TEST.OOP.BankAccount.CommertialBank;
using static TestOggettiBanca.Abstract.FinancialIntermediary;
using TestOggettiBanca.Abstract;

namespace TestOggettiBanca
{
    internal class CryptoExchange : FinancialIntermediary
   
    {


        public CryptoExchange(string name, string country, string city) : base(name, country, city)
        {
        }

        //public virtual Crypto DepositCrypto()
        //{

        //}

        public class Crypto : Asset
        {
            decimal _cryptoAmount;
            decimal _cryptoPrice = 28000;
            Crypto[] _cryptos { get; set; }
            int _counter;
            public override decimal AmountInEuro { get => _cryptoAmount * _cryptoPrice; }
            public decimal CryptoAmount { get => _cryptoAmount; set => _cryptoAmount = value; }

            public Crypto(Account Account, int totCrypto)
            {
                _cryptos = new Crypto[totCrypto];
            }

            public void addCrypto(Crypto Name)
            {
                if (_counter < _cryptos.Length)
                {
                    _cryptos[_counter] = Name;
                    _counter++;
                }
                else
                {
                    Crypto[] cryptos = new Crypto[_cryptos.Length + 1];
                    Array.Copy(_cryptos, cryptos, _cryptos.Length);
                    cryptos[_cryptos.Length] = Name;
                    _cryptos = cryptos;
                    _counter++;
                }

            }

        }

    }
}
