using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOggettiBanca
{
    internal class Asset
    {
        StockPortfolio _stock;
        CryptotPortfolio _cryptot;
        FiatPortfolio _fiat;
        BanckAccount _bankAccount;
    

        public decimal TotalValue { get; set; }
        internal StockPortfolio Stock { get => _stock; set => _stock = value; }
        internal CryptotPortfolio Cryptot { get => _cryptot; set => _cryptot = value; }
        internal FiatPortfolio Fiat { get => _fiat; set => _fiat = value; }


        private Asset(BanckAccount banckAccount, StockPortfolio stock, CryptotPortfolio cryptot, FiatPortfolio fiat)
        {
            _bankAccount = banckAccount;
            Stock = stock;
            Cryptot = cryptot;
            Fiat = fiat;
            banckAccount.AddAsset(this);
        }
        public void AddAssetStock(StockPortfolio asset)
        {
            Stock = asset;
        }

        public void RemoveAssetStock(StockPortfolio asset)
        {
            Stock = null;
        }

        public void AddAssetCryptot(CryptotPortfolio asset)
        {
            Cryptot = asset;
        }

        public void RemoveAssetCryptot(CryptotPortfolio asset)
        {
            Cryptot = null;
        }
        public void AddAssetFiat(FiatPortfolio asset)
        {
            Fiat = asset;
        }

        public void RemoveAssetFiat(FiatPortfolio asset)
        {
            Fiat = null;
        }
    }
}
