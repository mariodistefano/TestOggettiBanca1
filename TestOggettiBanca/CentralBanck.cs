using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOggettiBanca
{
    internal class CentralBanck
    {
        CommercialBanck _commercialBanck;
        SwiftCentralBank _swiftCentralBank;
        public CentralBanck()
        {
        }


        public void AddCommercialBanck(CommercialBanck commercialBanck) 
        {
            _commercialBanck = commercialBanck;
        }

        public void RemoveCommercialBanck(CommercialBanck commercialBanck) 
        {
            _commercialBanck = null;
        }

        public void transferMoney(CommercialBanck Cb1)
        {



        }


    }
}
