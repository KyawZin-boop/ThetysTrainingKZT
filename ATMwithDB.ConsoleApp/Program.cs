using ATMwithDB.ConsoleApp;

Services service = new Services();
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
}
