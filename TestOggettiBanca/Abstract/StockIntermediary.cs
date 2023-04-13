using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOggettiBanca.Abstract
{
    public abstract class StockIntermediary : FinancialIntermediary
    {


        public StockIntermediary(string name, string country, string city) : base(name, country, city)
        {
        }


    }
}
