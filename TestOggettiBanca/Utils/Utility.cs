using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TEST.OOP.BankAccount;

namespace TestOggettiBanca.Utils
{
    static class Utility
    {
        

        public static void GetAccountInfo(ConsoleColor consoleColor, CommertialBank bank, int index, bool isDeposit, FIATDespositRequest data)
        {
            Console.WriteLine($"Account Number: {bank._accounts[index].AccountNumber}");
            Console.WriteLine($"Account Client: {bank._accounts[index].Client1.Name}");
            Console.ForegroundColor = consoleColor;
            Console.WriteLine($"Amount {(isDeposit ? "Deposited" : "Withdrawn")}: {data._amount}");
            Console.ResetColor();
            // Console.WriteLine($"Account Balance: {bank.account.Balance}");

            Console.WriteLine("-------------------------------------");
        }

 

    }
}
