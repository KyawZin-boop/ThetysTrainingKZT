using System.Reflection;

try
{
    List<String> operators = ["+", "-", "*", "/", "%"];

    Console.Write("Pls Enter Your First Number = ");
    var userinput = Console.ReadLine();
    double num1 = Convert.ToDouble(userinput);

    Console.Write("Pls Enter The Operator You Want to Execute = ");
    var userOperator = Console.ReadLine();
    if (!operators.Contains(userOperator!))
    {
        Console.WriteLine("Pls Enter a Valid Operator");
        return;
    }

    Console.Write("Pls Enter Your Second Number = ");
    userinput = Console.ReadLine();
    double num2 = Convert.ToDouble(userinput);

    double result = 0;
    switch (userOperator)
    {
        case "+":
            result = num1 + num2;
            break;

        case "-":
            result = num1 - num2;
            break;

        case "*":
            result = num1 * num2; 
            break;

        case "/":
            result = num1 / num2;
            break;

        case "%":
            result = num1 % num2;
            break;
    }

    Console.WriteLine($"Your Result = {result}");
}
catch
{
    Console.WriteLine("Pls Enter a Number!");
}

