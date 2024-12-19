using System.Diagnostics.Contracts;
using ATM.ConsoleApp;

public class Program
{

    static List<User> users = new List<User>
    {
        new User("Mg Mg","mgmg123"),
        new User("Kyaw Kyaw","kyaw123"),
        new User("Zaw Zaw","zaw123"),
        new User("Aung Aung","aung123"),
        new User("Myo Myo","myo123"),
    };

    static void Main(string[] args)
    {
        var PasswordLimit = 0;
        while (true)
        {
            Console.Write("Please Enter Your UserName = ");
            var username = Console.ReadLine()!.Trim();
            if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("No Input");
                continue;
            }

            Console.Write("Please Enter Your Password = ");
            var password = Console.ReadLine()!.Trim();
            if (string.IsNullOrEmpty(password))
            {
                Console.WriteLine("No Input");
                continue;
            }

            var validateUser = users.Find(x => x.UserName == username && x.Password == password);

            if (validateUser is null)
            {
                var validateName = users.Find(x => x.UserName == username);
                if (validateName is null)
                {
                    Console.WriteLine("There is No User with this name!");
                    continue;
                }
                else
                {
                    Console.WriteLine("Incorrect Password");
                    PasswordLimit++;
                    if (PasswordLimit == 3)
                    {
                        Console.WriteLine("Close this program to try agian!");
                        break;
                    }
                    continue;
                }
            }

            ATMFunction(validateUser);
        }
    }
    static void ATMFunction(User user)
    {
        Console.WriteLine("Login Success!");
        Console.WriteLine("-----------------------");

        while (true)
        {
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
                    var withdrawAmount = Convert.ToDouble(Console.ReadLine());
                    if (withdrawAmount > user.Balance)
                    {
                        Console.WriteLine("Not enough balance!");
                    }
                    user.Balance -= withdrawAmount;
                    Console.WriteLine("Withdraw Success!");
                    Console.ReadLine();
                    break;

                case "2":
                    Console.WriteLine("Please Enter the amount you want to deposit");
                    var depositAmount = Convert.ToDouble(Console.ReadLine());
                    user.Balance += depositAmount;
                    Console.WriteLine("Deposit Success!");
                    Console.ReadLine();
                    break;

                case "3":
                    Console.WriteLine($"Your Current Balance = {user.Balance}");
                    Console.ReadLine();
                    break;

                case "4":
                    Console.WriteLine("Log out Success!");
                    Console.ReadLine();
                    break;

                case "5":
                    Console.WriteLine("Program Ended");
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid Input");
                    break;
            }

            if (userInput == "4") break;
        }
    }
}
