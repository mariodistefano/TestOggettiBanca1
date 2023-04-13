using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using TEST.OOP.BankAccount;
using System.Timers;

namespace TestOggettiBanca.BankAccount
{
    internal class Program
    {

        static void Main(string[] args)
        {

            CentralBank RussianCentralBank = new CentralBank("Russia Central Bank", "Russia", 5, "catania");
            CentralBank BancaDitalia = new SwiftCentralBank("Banca D'Italia", "Italia", 3, "catania");

            
            CommertialBank SberBank = new CommertialBank("SberBank", "Russia", RussianCentralBank,"catania", "IT");
            CommertialBank Unicredit = new CommertialBank("Unicredit", "Italy", BancaDitalia, "catania", "IT");
     

            // Crea un conto Corrente e deposita dentro un dei soldi 
            DateTime dateTime = new DateTime(1995 - 04 - 12);

            SberBank.CreateAccount("Vladimir Putin", "SDFGFBDGFGD657", "05 / 01 / 2015", "IT");
            SberBank.CreateAccount("Vladimir Putin", "SDFGFBDGFGD657", "05 / 01 / 2015", "IT");

            StockMarket BorsaItaliana = new StockMarket("Borsa Italiana", "italy","catania");
            SberBank.BuyStock(STOCK.TESLA, 3, BorsaItaliana, "SDFGFBDGFGD657");
            
            
            SberBank.addFiatToAccount("euro", "SDFGFBDGFGD657");
            SberBank.DepositFiat("euro", "SDFGFBDGFGD657", 100000M);
            SberBank.WithdrawFIAT("euro", "SDFGFBDGFGD657", 9900M);
            SberBank.WithdrawFIAT("euro", "SDFGFBDGFGD657", 9000M);
            SberBank.WithdrawFIAT("euro", "SDFGFBDGFGD657", 7000M);
            SberBank.DepositCrypto("SDFGFBDGFGD657", 4);
      

            // Crea un conto Corrente con saldo zero
            Unicredit.CreateAccount("Mario Di Stefano", "DSTMNC97T29C351W", "05 / 01 / 2015", "IT");
            Unicredit.CreateAccount("Bruno Ferreira", "FRBBRIIM394NFNNF", "05 / 01 / 2015","IT");
            Unicredit.CreateAccount("pippo pizza", "PIPOPOO454SDDDT", "05 / 01 / 2015", "IT");
            Unicredit.addFiatToAccount("euro", "DSTMNC97T29C351W");

            Unicredit.DepositFiat("euro", "DSTMNC97T29C351W", 10000000000M);
            // rimuove account
            //  Unicredit.RemoveAccount("FRBBRIIM394NFNNF");

            // Stampa Saldo iniziale dei due conti  
            Console.WriteLine("-------------------------------------- SALDO INIZIALE -------------------");

            Console.WriteLine($" L'account di Vladimir Putin ha un credito di :  {SberBank._accounts[0].Balance}");
            Console.WriteLine($" L'account di Bruno Ferreira ha un credito di :  {Unicredit._accounts[0].Balance}");
            Console.WriteLine("-------------------------------------------------------------------------------");


            bool result = SberBank.Transfer("EURO","TMNC97T29C351W", Unicredit, new FIATDespositRequest() { _amount = 1000M, _accountfrom = 5548485187, _accountTo = 1112355477 });

            if (!result)
            {
                Console.WriteLine("Amount not Transfered!");
                return;// Fine Programma! 
            }


            // Stampa Saldo Fianale dei due conti  
            Console.WriteLine("-------------------------------------- SALDO FINALE -------------------");

            Console.WriteLine($" L'account di Vladimir Putin ha un credito di :  {SberBank._accounts[0].Balance}");
            Console.WriteLine($" L'account di Bruno Ferreira ha un credito di :  {Unicredit._accounts[0].Balance}");
            Console.WriteLine("-------------------------------------------------------------------------------");


            Console.ResetColor();

            // Show After before Transfer  

            Console.ReadLine();

        }
    }

}

