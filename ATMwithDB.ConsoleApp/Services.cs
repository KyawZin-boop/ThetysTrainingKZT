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
        Console.WriteLine("Login Success!");
        Console.WriteLine("-----------------------");

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            var getUser = _db.Users.Where(x => x.UserID == user.UserID).FirstOrDefault();
            Console.WriteLine("Please enter the number you want to execute");
            Console.WriteLine("1. Withdraw");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Check Balance");
            Console.WriteLine("4. Log Out");
            Console.WriteLine("5. End Program");
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
                    Console.WriteLine($"Your Current Balance = {getUser!.Balance}");
                    Console.ReadLine();
                    break;

                case "4":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Log out Success!");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.ReadLine();
                    break;

                case "5":
                    Console.WriteLine("Program Ended");
                    Environment.Exit(0);
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Input");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
            }

            if (userInput == "4") break;
        }
    }
}
