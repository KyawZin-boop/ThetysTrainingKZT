using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ATMwithDB.ConsoleApp;

public class Services
{
    private readonly AppDbContext _db = new AppDbContext();

    public User ValidateUser(string username, string password)
    {
        var user = _db.Users.AsNoTracking().FirstOrDefault(x => x.UserName == username && x.Password == password);
        if (user is null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Wrong Credentials");
            Console.ForegroundColor = ConsoleColor.Blue;
            return new User
            {
                UserID = null,
                Balance = 0m,
                ActiveFlag = null,
            }; ;
        }
        return new User
        {
            UserID = user.UserID,
            Balance = user.Balance,
            ActiveFlag = user.ActiveFlag,
        };
    }

    public void ATMFunction(User user)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nLogin Success!");
        Console.WriteLine("-----------------------");

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            var getUser = _db.Users.Where(x => x.UserID == user.UserID).FirstOrDefault();
            Console.WriteLine("Please enter the number you want to execute");
            Console.WriteLine("1. Withdraw");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Check Balance");
            Console.WriteLine("4. Check Report");
            Console.WriteLine("5. Log Out");
            Console.WriteLine("6. End Program");
            Console.WriteLine();
            var userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    Console.WriteLine("Please Enter the amount you want to withdraw");
                    var withdrawAmount = Convert.ToDecimal(Console.ReadLine());
                    if (withdrawAmount > user.Balance)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Not enough balance!");
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }

                    getUser!.Balance -= withdrawAmount;
                    _db.Entry(getUser).State = EntityState.Modified;
                    _db.TransactionHistory.Add(new Transaction
                    {
                        UserId = getUser.UserID,
                        TransactionType = "Withdraw",
                        Amount = withdrawAmount,
                        Date = DateTime.Now,
                    });
                    _db.SaveChanges();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Withdraw Success.");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.ReadLine();
                    break;

                case "2":
                    Console.WriteLine("Please Enter the amount you want to deposit");
                    var depositAmount = Convert.ToDecimal(Console.ReadLine());
                    getUser!.Balance += depositAmount;
                    _db.Entry(getUser).State = EntityState.Modified;
                    _db.TransactionHistory.Add(new Transaction
                    {
                        UserId = getUser.UserID,
                        TransactionType = "Deposit",
                        Amount = depositAmount,
                        Date = DateTime.Now,
                    });
                    _db.SaveChanges();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Deposit Success.");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.ReadLine();
                    break;

                case "3":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\nYour Current Balance = {getUser!.Balance}");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.ReadLine();
                    break;

                case "4":
                    var userReport = _db.TransactionHistory.AsNoTracking().Where(x => x.UserId == getUser.UserID);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0,-40} {1,-10} {2,-20} {3,-15} {4,-30:C}", "TID", "Name", "Type", "Amount", "Date");
                    Console.WriteLine(new string('-', 120));

                    foreach (var report in userReport)
                    {
                        Console.WriteLine("{0,-40} {1,-10} {2,-20} {3,-15} {4,-30}", report.TransactionID, getUser.UserName, report.TransactionType, report.Amount, report.Date);
                    }
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;

                case "5":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Log out Success!");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.ReadLine();
                    break;

                case "6":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Program Ended.");
                    Environment.Exit(0);
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Input");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
            }

            if (userInput == "5") break;
        }
    }
}
