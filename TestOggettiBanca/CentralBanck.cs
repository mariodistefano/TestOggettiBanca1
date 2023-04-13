using TestOggettiBanca.Static;
using TestOggettiBanca;
using TEST.OOP.BankAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOggettiBanca
{
    class CentralBank : Bank
    {
        public int _interestRate;
        CommertialBank[] _commercialBanks { get; set; }
        int counter;
        public bool CheckTransfer(string nameFiat, string CF, Bank from, Bank to, FIATDespositRequest data)
        {
            if (from.Country == to.Country)
            {
                return from.Transfer(nameFiat, CF, to, data);
            }
            else
            {
                if (WordBank.Transfer((CommertialBank)from, (CommertialBank)to, data))
                {
                    return true;
                }
                return false;
            }
        }
        // ADD
        public void addCommercialBank(CommertialBank commertialBank) 
        {
            if (counter > _commercialBanks.Length)
            {
                  _commercialBanks[counter] = commertialBank;
                  counter++;
            }
            else
            {
                CommertialBank[] commertialBanks = new CommertialBank[_commercialBanks.Length + 1];
                Array.Copy(_commercialBanks, commertialBanks, _commercialBanks.Length);
                commertialBanks[_commercialBanks.Length] = commertialBank;
                _commercialBanks = commertialBanks;
                counter++;
            }
        }
        // REMOVE
        public void removeCommertialBank(CommertialBank[] commertialBank , int index) 
        {
            CommertialBank[] newArrayCommertial = new CommertialBank[commertialBank.Length - 1];
            int newIndex = 0;
            for (int i = 0; i < commertialBank.Length; i++) 
            {
                if (i != index)
                {
                    newArrayCommertial[newIndex++] = commertialBank[i];
                }
                _commercialBanks = newArrayCommertial;
            }
        }

        const decimal _maxInterestTax = 5;
        public CentralBank(string name, string Country, int MaxInterestRate,string city) : base(name, Country, city)
        {

        }
    }

}

