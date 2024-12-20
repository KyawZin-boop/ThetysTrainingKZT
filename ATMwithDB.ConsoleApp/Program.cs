using ATMwithDB.ConsoleApp;

Services service = new Services();
while (true)
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("Choose one Option You want to Execute");
    Console.WriteLine("1. Login");
    Console.WriteLine("2. Exit\n");
    var input = Console.ReadLine();

    switch (input)
    {
        case "1":
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Please Enter Your UserName = ");
                var username = Console.ReadLine()!.Trim();
                if (string.IsNullOrEmpty(username))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No Input");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    continue;
                }

                Console.Write("Please Enter Your Password = ");
                var password = Console.ReadLine()!.Trim();
                if (string.IsNullOrEmpty(password))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No Input");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    continue;
                }

                var result = service.ValidateUser(username, password);

                if (result.UserID is null) continue;

                service.ATMFunction(result);
                break;
            }
            break;

        case "2":
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\nProgram Ended.");
            Console.ForegroundColor = ConsoleColor.White;
            Environment.Exit(0);
            break;
    }
}
