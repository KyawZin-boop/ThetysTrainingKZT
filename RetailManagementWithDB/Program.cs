using RetailManagementWithDB;

Services service = new Services();
List<Cart> cart = new List<Cart>();

while (true)
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("Pls Type the Number to Execute...");
    Console.WriteLine("1. Stock Menu");
    Console.WriteLine("2. Cashier Menu");
    Console.WriteLine("3. Manager Menu");
    Console.WriteLine("4. End Program");
    var menu = Console.ReadLine();

    switch (menu)
    {
        case "1":
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\nPODUCT MENU");
                Console.WriteLine("Pls Type the Number to Execute...");
                Console.WriteLine("1. Product Inventory");
                Console.WriteLine("2. Edit Product");
                Console.WriteLine("3. Add New Product");
                Console.WriteLine("4. Exit to Menu");
                var saleMenu = Console.ReadLine();
                Console.WriteLine();

                switch (saleMenu)
                {
                    case "1":
                        service.ShowProduct();
                        break;

                    case "2":
                        service.EditProduct();
                        break;

                    case "3":
                        service.AddProduct();
                        break;

                    case "4":
                        break;
                }
                if (saleMenu == "4") break;
            }
            break;

        case "2":
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Cashier Menu");
                Console.WriteLine("1. Add to Cart");
                Console.WriteLine("2. Summarizing Order");
                Console.WriteLine("3. Exit to Menu");
                var cashierMenu = Console.ReadLine();
                Console.WriteLine();

                switch (cashierMenu)
                {
                    case "1":
                        cart = service.AddtoCart();
                        break;

                    case "2":
                        service.SummarizeOrder(cart);
                        break;

                    case "3":
                        break;
                }
                if (cashierMenu == "3") break;
            }
            break;

        case "3":
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Cashier Menu");
                Console.WriteLine("1. View Report");
                Console.WriteLine("2. Total Summary");
                Console.WriteLine("3. Exit to Menu");
                var managerMenu = Console.ReadLine();
                Console.WriteLine();

                switch (managerMenu)
                {
                    case "1":
                        service.ShowSaleRecord();
                        break;

                    case "2":
                        service.ShowTotalSummary();
                        break;

                    case "3":
                        break;
                }
                if (managerMenu == "3") break;
            }
            break;

        case "4":
            Environment.Exit(0);
            break;

        default:
            Console.WriteLine("Invalid Input");
            break;
    }
}