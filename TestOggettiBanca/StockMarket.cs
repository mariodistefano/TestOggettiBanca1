using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestOggettiBanca.Abstract;

namespace TestOggettiBanca
{

    public enum STOCK
    {
        TESLA,
        META,
        APL,
        BMW,
        HONDA
    }


    public class StockMarket : StockIntermediary
    {

        DateTime OpenTime = new DateTime();

        public StockMarket(string name, string country, string city) : base( name,  country,  city)
        {
        }

        public override Asset BuyStock(STOCK STOCK, int Amount, StockMarket stockMarket , string account)
        {
            return new Stock(STOCK, 3);
        }

        internal class Stock : Asset
        {

            StockMarket _stockMarket;
            STOCK _stockTicker;
            public StockMarket StockMarket { get; }
            public decimal Price { get; set; } = 200M;

            public override decimal AmountInEuro => throw new NotImplementedException();

            public Stock(STOCK cRYPTO, int Amount) //  Visibile dall'esterno ma non istanziabile
            {
            }




        }
    }

}


