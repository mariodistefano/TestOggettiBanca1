using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOggettiBanca
{
    internal class BanckAccount
    {
        FiatPortfolio _fiatPortfolio;
        CryptotPortfolio _cryptotPortfolio;
        StockPortfolio _stockPortfolio;
        CommercialBanck _CommercialBanck;
        Asset _asset;
        string _nome;
        string _paese;
        int _totalValue;


        public int TotalValue { get => _totalValue; set => _totalValue = value; }
        public string Nome { get => _nome; set => _nome = value; }
        public string Paese { get => _paese; set => _paese = value; }
        internal CommercialBanck CommercialBanck { get => _CommercialBanck; set => _CommercialBanck = value; }
        internal Asset Asset { get => _asset; set => _asset = value; }







        // Costruttore di un account bancario
        public BanckAccount(CommercialBanck CommercialBanck)
        {
            this.CommercialBanck = CommercialBanck;
            
        }

        public void AddAsset(Asset asset) 
        {
            Asset = asset;
        }



    }
}
