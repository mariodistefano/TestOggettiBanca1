using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOggettiBanca
{
    internal class SwiftCentralBank : ISwiftSystem
    {
        CommercialBanck _commercialBanck;

        public SwiftCentralBank()
        {
        }
        public void AddCommercialBanck(CommercialBanck commercialBanck) 
        {
            _commercialBanck = commercialBanck;
        }
        public void RemoveCommercialBank(CommercialBanck commercialBanck) 
        {
            _commercialBanck = null;
        }

    }
}
