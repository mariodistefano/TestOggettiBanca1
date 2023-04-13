using System;
using TestOggettiBanca.Utils;
using TestOggettiBanca;
using System.Runtime.CompilerServices;
using static TEST.OOP.BankAccount.CommertialBank;
using TestOggettiBanca.Abstract;
using static TEST.OOP.BankAccount.CommertialBank.Account;
using static TestOggettiBanca.Abstract.FinancialIntermediary;
using System.Diagnostics.Tracing;
using System.Globalization;
using static TestOggettiBanca.CryptoExchange;
using System.Timers;
using System.IO;

namespace TEST.OOP.BankAccount
{
        class CommertialBank : Bank
        {
        private CentralBank _centralBank;
        // Account _account;
        public Account[] _accounts { get; set; }
        int counter;
        CultureInfo _cultureInfo;
        //  public Account account { get => _account; }
        public CentralBank CentralBank { get => _centralBank; }
        public CultureInfo CultureInfo { get => _cultureInfo; set => _cultureInfo = value; }

        public CommertialBank(string Name, string Country, CentralBank Bank,string city, string culureinfo ) : base(Name, Country, city)
        {
            _centralBank = Bank;
            _country = Country;
            _name = Name;
            _code = new Random().Next(10000, 1000000);
            _accounts = new Account[0];
            CultureInfo  = new CultureInfo(culureinfo);
        }


        public void DepositCrypto(decimal Amount, string CodiceCF)
        {
            Account account = Array.Find(_accounts, account => account.Client1.Cf == CodiceCF);
            account.DepositCrypto(Amount);
        }


        public void addFiatToAccount(string NameFiat, string account)
        {
            Account find = Array.Find(_accounts, CodiceCF => CodiceCF.Client1.Cf == account);
            Fiat fiat = new Fiat(NameFiat);
            find.addFiat(fiat);
        }


        private void addStockToAccount(Asset asset, string account)
        {
            Account find = Array.Find(_accounts, CodiceCF => CodiceCF.Client1.Cf == account);
            find.AddStock(asset);
        }
      
        public override Asset BuyStock(STOCK STOCK, int Amount, StockMarket stockMarket, string account)
        {
            DateTime now = DateTime.Now;
            TimeSpan start = new TimeSpan(9, 0, 0);
            TimeSpan end = new TimeSpan(18, 0, 0);
            if (now.TimeOfDay < start || now.TimeOfDay > end)
            {
                Console.WriteLine("La compravendita di azioni è disponibile solo durante l'orario d'ufficio (9:00 - 18:00).");
                return null;
            }

            Account find =  Array.Find(_accounts, CodiceCF => CodiceCF.Client1.Cf == account);
            Asset stock = base.BuyStock(STOCK, Amount, stockMarket, account);
            this.addStockToAccount(stock, account);

            return stock;
        }



        public void CreateAccount(string name, string cf, string dateOfBirth, string culturezone)
        {
           
          //  string Dateformat = "MM/dd/yyyy";
            string dateFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
            DateTime output;
            CultureInfo culture = new CultureInfo(culturezone);




            if (DateTime.TryParseExact(dateOfBirth, dateFormat, culture, DateTimeStyles.None, out output))
            {  
                // Verifica se l'utente ha almeno 18 anni
                if (DateTime.Today.AddYears(-18) < output)
                {
                    throw new ArgumentException("L'utente deve avere almeno 18 anni per aprire un conto bancario.");
                }

            }


            // Crea l'account bancario
            Account bankAccount = new Account(this, name, cf);

            // Aggiunge l'account all'array _accounts
            Account[] temporaryArray = new Account[_accounts.Length + 1];
            Array.Copy(_accounts, temporaryArray, _accounts.Length);
            _accounts = temporaryArray;
            _accounts[_accounts.Length - 1] = bankAccount;
        }

        public void addAccount(Account account) 
        {
                Account[] newAccount = new Account[_accounts.Length + 1];
                Array.Copy(_accounts, newAccount, _accounts.Length);
                newAccount[_accounts.Length] = account;
                _accounts = newAccount;
        }
        public void RemoveAccount(string ClientCF) 
        {
            Account account = Array.Find(_accounts, account => account.Client1.Cf == ClientCF);
            int index = Array.IndexOf(_accounts, account);
                                                                                                                           
            if (index >= 0)
            {
                Array.Copy(_accounts, index + 1, _accounts, index, _accounts.Length - index - 1);
                _accounts[_accounts.Length - 1] = null;
            }
        }

        public override bool Transfer(string nameFiat, string CF, Bank to, FIATDespositRequest data)
        {

            // CommertialBank transferFrom = (CommertialBank) from;
            CommertialBank transferTo = (CommertialBank)to;

            Account account = Array.Find(_accounts, account => account.Client1.Cf == CF);
            var index = Array.IndexOf(_accounts, account);

            Account accountTo = Array.Find(transferTo._accounts, account => account.AccountNumber == data._accountTo);
            var indexTo = Array.IndexOf(transferTo._accounts, accountTo);

            if (account != null && this._centralBank.CheckTransfer(nameFiat ,CF , this, transferTo, data))
            {
                /*  
                   Prima di procedere con il versamento, verificare che l'ammontare da accreditare è stato effettivamente scalato dal conto del versatore
                   Quindi  avere una copia dello stato del conto prima di scalere i soldi per  poter confrontare che il prelievo è andato a buon fine.
                */

                // stato conto prima
                transferTo.WithdrawFIAT(nameFiat, CF, data._amount);
                // confronto le due cifre dopo il prelievo. 
                Utility.GetAccountInfo(ConsoleColor.Red, this, index, false, data);
                
                transferTo._accounts[index].DepositFIAT(nameFiat, data._amount);
                Utility.GetAccountInfo(ConsoleColor.Green, transferTo, indexTo, true, data);


                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine($"The  amount {data._amount} from the account {data._accountfrom} from the Bank {this.Name} to " +
                    $"account {data._accountTo} of from the Bank {to.Name} has been made! ");
                Console.ResetColor();

                return true;
            }
            return false;
        }
        public void DepositFiat(string fiatName ,string CodiceCF , decimal Amount)
        {
            // Check Client // è biondo! //                                                                                       // qui
            Account account1 = Array.Find(_accounts, account => account.Client1.Cf == CodiceCF);
            account1.DepositFIAT(fiatName, Amount);
        }
        public void DepositCrypto(string CodiceCF , decimal Amount)
        {
            Account account1 = Array.Find(_accounts, account => account.Client1.Cf == CodiceCF);

            account1.DepositCrypto(Amount);
        }
   
        //                                                                                                        controllo prelievoGiornaliero()
        public bool checkWhitdraw(Account account , decimal Amount)
        {  
          
            if (account.TotalWithdrawalToday > account.MaxWithdrawalPerDay || Amount > account.MaxWithdrawalPerDay)
            {
                Console.WriteLine("Error: Exceeded the maximum daily withdrawal limit");
    
                return false;
            }
          
            DateTime today = DateTime.Today;
            
            if (account.LastWithdrawalDate.Day != today.Day )
            {
                account.LastWithdrawalDate = today;
                account.TotalWithdrawalToday = 0;
            }

            decimal monthlyWithdrawalLimit = 30000;
            decimal totalWithdrawalThisMonth = account.TotalWithdrawalThisMonth + Amount;
            if (totalWithdrawalThisMonth > monthlyWithdrawalLimit)
            {
                Console.WriteLine("Error: Exceeded the monthly withdrawal limit");
                if (account.WithdrawalBlockedUntil <= today)
                {
                    // Block withdrawals for 3 days
                    account.WithdrawalBlockedUntil = today.AddDays(3);
                }
                return false;
            }

            account.TotalWithdrawalToday += Amount;
            account.TotalWithdrawalThisMonth = totalWithdrawalThisMonth;
            return true;
        }
        public static void WriteOnFile(string log, string path, string FileName)
        {
            //logs
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // File.WriteAllLines(Path.Combine(path, FileName), log); // -> Erease all content 
            File.AppendAllText(Path.Combine(path, FileName), log); // -> Add Content text 
        }

        public void WithdrawFIAT(string nameFiat, string CodiceCF ,decimal Amount)
        {
           Account account1 = Array.Find(_accounts, account => account.Client1.Cf == CodiceCF);

           account1.TotalWithdrawalToday += Amount; 
            
           if(checkWhitdraw(account1, Amount)) 
           {
            account1.LastWithdrawalDate = DateTime.Now;
            account1.WithdrawFIAT(nameFiat.ToLower(), Amount);

            string log = String.Format($"{Amount}, {account1.Client1.Name}, {account1.CommertialBank}");
            WriteOnFile(log, @"c:\log\", "log.txt");
           }
            
        }
        public void WithdrawCrypto(string CodiceCF, decimal Amount)
        {
            Account account1 = Array.Find(_accounts, account => account.Client1.Cf == CodiceCF);
            account1.WithdrawCrypto(Amount);
          
        }
        public void SellStoks(string CodiceCF, decimal Amount)
        {
            Account account1 = Array.Find(_accounts, account => account.Client1.Cf == CodiceCF);

            account1.SellStoks(Amount);
        }
        public class Account
        {
            CommertialBank _commertialBank;
            Client _client;
            public long AccountNumber { get; }
            Asset[] _assets;
            Fiat[] _fiats;
            Crypto _crypto;
            decimal _interests;
            decimal _balance;
            decimal maxWithdrawalPerDay = 10000M; // 10K al giorno
            decimal totalWithdrawalToday = 0;
            decimal withdrawalAmount = 0;
            public decimal TotalWithdrawalThisMonth { get; set; }
            public DateTime WithdrawalBlockedUntil { get; set; }


            public DateTime LastWithdrawalDate { get; set; }
            public DateTime LastWithdrawalDateMonth { get; set; }
            public decimal Balance { get { return CalcAmount() + calcInterests(); } }
            public Asset[] Assets { get => _assets; set => _assets = value; }
            internal CommertialBank CommertialBank { get => _commertialBank; set => _commertialBank = value; }
            internal Client Client1 { get => _client; set => _client = value; }
            public decimal TotalWithdrawalToday { get => totalWithdrawalToday; set => totalWithdrawalToday = value; }
            public decimal MaxWithdrawalPerDay { get => maxWithdrawalPerDay; set => maxWithdrawalPerDay = value; }
            public decimal WithdrawalAmount { get => withdrawalAmount; set => withdrawalAmount = value; }






            public Account(CommertialBank commercialBanck, string ClientName, string ClientCF)
            {
                CommertialBank = commercialBanck;
                Client1  = new Client(ClientName, ClientCF);
                AccountNumber = new Random().Next(10000, 1000000);
                Assets = new Asset[0];
                _crypto = new Crypto(this, 0);
            }

            internal void AddStock(Asset asset)
            {
                if (_assets == null)
                {
                    _assets = new Asset[] { asset };
                }
                else 
                {
                    Asset[] nuovoArray = new Asset[_assets.Length + 1];
                    Array.Copy(_assets, nuovoArray, _assets.Length);
                    nuovoArray[_assets.Length] = asset;
                    _assets = nuovoArray;
                }
            }

            //                                                                                   qui
            public void addFiat(Fiat fiat)
            {
                if (_fiats == null)
                {
                    _fiats = new Fiat[] { fiat };
                    return;
                }

                Fiat[] newFiat = new Fiat[_fiats.Length + 1];
                Array.Copy(_fiats, newFiat, _fiats.Length);
                newFiat[_fiats.Length] = fiat;
                _fiats = newFiat;

            }
            decimal calcInterests()
            {
             return _interests = (CalcAmount() / 100) * _commertialBank.CentralBank._interestRate;
            }
            decimal CalcAmount()
            {
                // return 0M;
                foreach (var fiat in _fiats)
                {
                    return fiat.AmountInEuro + _crypto.AmountInEuro;
                }
                return 0M;
        
            }

            //                                                                                                     da veederreeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee-------------
            public void DepositFIAT(string fiatname , decimal Amount)
            {
                
               // 
                if (Array.Find(_fiats, fiat => fiat.Namefiat == fiatname) == null)
                {
                    Fiat fiat1 = Array.Find(_fiats, fiat => fiat.Namefiat == fiatname);
                    addFiat(fiat1);
                    fiat1.EuroAmount += Amount;
                }
                Fiat fiat2 = Array.Find(_fiats, fiat => fiat.Namefiat == fiatname);
                fiat2.EuroAmount += Amount;
                
            }

           
            public void DepositCrypto(decimal Amount)
            {
                _crypto.CryptoAmount += Amount;
            }
    
            public void WithdrawFIAT(string fiat, decimal Amount)
            {
            
                if (_fiats.Length == 0) 
                {
                    Console.WriteLine($"you have no currency");
                    return;
                }
                Fiat fiat1 = Array.Find(_fiats, fiat1 => fiat1.Namefiat == fiat);
                fiat1.EuroAmount -= Amount;
            }
            public void WithdrawCrypto(decimal Amount)
            {
                this._crypto.CryptoAmount -= Amount;
            }
            public void SellStoks(decimal Amount)
            {
                CommertialBank.SellStoks(Client1.Cf, Amount);
            }
     
            public class Client
            {
                string _name;
                string _cf;
                Account[] _accounts;
                int counter;
                long _clientId;


                public Client(string ClientName, string ClientCF)
                {
                    Cf = ClientCF;
             
                    Name = ClientName;
                    Accounts = new Account[0];
                    _clientId = new Random().Next(10000, 100000);
                }
                public void addAccount(Account account)
                {
                    if (counter < _accounts.Length)
                    {
                        _accounts[counter] = account;
                        counter++;
                    }
                    else
                    {
                        Account[] accounts = new Account[_accounts.Length + 1];
                        Array.Copy(_accounts, accounts, _accounts.Length);
                        accounts[_accounts.Length] = account;
                        _accounts = accounts;
                        counter++;
                    }
                }

                public void removeAccount(Account[] arrayAccount , int index) 
                {
                    Account[] newArray = new Account[_accounts.Length -1];
                    int newIndex = 0;

                    for (int i = 0; i < _accounts.Length; i++) 
                    {
                        if (i != index) 
                        {
                            newArray[newIndex++] = arrayAccount[i];
                            _accounts = newArray;
                        }
                       
                    }

                }

                public string Name { get => _name; set => _name = value; }
                public long ClientId { get => _clientId; }
                public string Cf { get => _cf; set => _cf = value; }
                internal Account[] Accounts { get => _accounts; set => _accounts = value; }
            }
          
            public class Fiat : Asset
            {
                public decimal EuroAmount;
                public decimal GbpAmount;
                Fiat _fiat;
                Fiat[] _fiats { get; set; }
                int _counter;
                decimal _euroPrice = 1;
                decimal _gbpPrice = 0.89M;
                string _namefiat;
                public override decimal AmountInEuro { get => EuroAmount + (GbpAmount * _gbpPrice); } // Converti in EURO. Per esempio, se ho altre FIAT come Dollari, Yen , Sterline 
                public string Namefiat { get => _namefiat; set => _namefiat = value; }

                // public decimal FiatAmount { get => _fiatAmount; set => _fiatAmount = value; }
                public Fiat(string nameFiat) 
                {
                    Namefiat = nameFiat;
                }
                public void addFiat(Fiat Name)
                {
                    if (_counter < _fiats.Length)
                    {
                        _fiats[_counter] = Name;
                        _counter++;
                    }
                    else 
                    {
                        Fiat[] fiats = new Fiat[_fiats.Length + 1];
                        Array.Copy(_fiats, fiats, _fiats.Length);
                        _fiats[_fiats.Length] = Name;
                        _fiats = fiats;
                        _counter++;
                    }
                }
            }
           
        }
    }
}

