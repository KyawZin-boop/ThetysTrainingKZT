try
{
    Console.Write("Width : ");
    var width = Convert.ToDouble(Console.ReadLine());
    Console.Write("Height : ");
    var height = Convert.ToDouble(Console.ReadLine());

    var result = width * height;
    Console.WriteLine($"Rectangle Area = {result}");
}
catch
{
    Console.WriteLine("Please Enter A Number!");
}
