using System;
using TestOggettiBanca.Utils;
using TestOggettiBanca;
using System.Runtime.CompilerServices;
using static TEST.OOP.BankAccount.CommertialBank;
using TestOggettiBanca.Abstract;

namespace TEST.OOP.BankAccount
{
        class CommertialBank : Bank
        {
        private CentralBank _centralBank;
        // Account _account;
        public Account[] _accounts { get; set; }
        int counter;
        //  public Account account { get => _account; }
        public CentralBank CentralBank { get => _centralBank; }
        public CommertialBank(string Name, string Country, CentralBank Bank ) : base(Name, Country)
        {
            _centralBank = Bank;
            _country = Country;
            _name = Name;
            _code = new Random().Next(10000, 1000000);
            _accounts = new Account[0];
           
        }
        public void AddBankAccount(string name, string surname, string id)
        {
            Client bankClient = Array.Find(BankClientProp, client => client.ID.Equals(id) && client.Name.Equals(name) && client.Surname.Equals(surname));

            BankAccount bankAccount = new BankAccount(this, bankClient);

            bankClient.AddBankAccount(bankAccount);

            BankAccount[] temporaryArray = new BankAccount[BankAccountProp.Length + 1];
            Array.Copy(BankAccountProp, temporaryArray, BankAccountProp.Length);
            BankAccountProp = temporaryArray;
            BankAccountProp[BankAccountProp.Length - 1] = bankAccount;
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
            Account account = Array.Find(_accounts, account => account._client.Cf == ClientCF);
            int index = Array.IndexOf(_accounts, account);
                                                                                                                              //   Da vedere
            if (index >= 0)
            {
                Array.Copy(_accounts, index + 1, _accounts, index, _accounts.Length - index - 1);
                _accounts[_accounts.Length - 1] = null;
            }
        }

        public override bool Transfer(string CF, Bank to, FIATDespositRequest data)
        {

            // CommertialBank transferFrom = (CommertialBank) from;
            CommertialBank transferTo = (CommertialBank)to;

            Account account = Array.Find(_accounts, account => account._client.Cf == CF);
            var index = Array.IndexOf(_accounts, account);

            Account accountTo = Array.Find(transferTo._accounts, account => account.AccountNumber == data._accountTo);
            var indexTo = Array.IndexOf(transferTo._accounts, accountTo);

            if (account != null && this._centralBank.CheckTransfer(CF , this, transferTo, data))
            {
                /*  
                   Prima di procedere con il versamento, verificare che l'ammontare da accreditare è stato effettivamente scalato dal conto del versatore
                   Quindi  avere una copia dello stato del conto prima di scalere i soldi per  poter confrontare che il prelievo è andato a buon fine.
                */

                // stato conto prima
                transferTo.WithdrawFIAT(CF, data._amount);
                // confronto le due cifre dopo il prelievo. 
                Utility.GetAccountInfo(ConsoleColor.Red, this, index, false, data);
                
                transferTo._accounts[index].DepositFIAT(data._amount);
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
            Account account1 = Array.Find(_accounts, account => account._client.Cf == CodiceCF);
            account1.DepositFIAT(Amount);
        }
        public void DepositCrypto(string CodiceCF , decimal Amount)
        {
            Account account1 = Array.Find(_accounts, account => account._client.Cf == CodiceCF);

            account1.DepositCrypto(Amount);
        }
        public void InvestInStock(string CodiceCF , decimal Amount)
        {
            Account account1 = Array.Find(_accounts, account => account._client.Cf == CodiceCF);

namespace TestOggettiBanca
{
    internal class CommercialBanck : Bank
    {
        string _name;
        private BanckAccount _banckAccount;
        private SwiftCentralBank _swiftCentralBank;
        CentralBanck _centralBanck;
        public CommercialBanck(CentralBanck centralBanck)
        {
            centralBanck.AddCommercialBanck(this);
        }



        // deposito
        public override void Deposit(int depositMoney)
        {
            TotalValue += depositMoney;
            ShowTotalValue();
        }
        // prelievo
        public override void withdraw(int withdrawMoney)
        {
            TotalValue -= withdrawMoney;
            ShowTotalValue();
        }

        // mostra totale
        public override void ShowTotalValue()
        {
            Console.WriteLine(TotalValue);
        }


        public void AddBanckAccount(BanckAccount banckAccount, string name)
        {
            _banckAccount = banckAccount;
        }

        public void RemoveBanckAccount()
        {
            _banckAccount = null;
        }
     



    }
}
