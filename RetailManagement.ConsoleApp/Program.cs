using System.Collections.Generic;
using RetailManagement.ConsoleApp;

class Program
{
    static List<Product> Products = new List<Product>
    {
        new Product{Id = 1, Name = "Apple", Stock = 30, Price = 800, ProfitPerItem = 200},
        new Product{Id = 2, Name = "Orange", Stock = 50, Price = 500, ProfitPerItem = 100},
        new Product{Id = 3, Name = "Banana", Stock = 35, Price = 400, ProfitPerItem = 100},
        new Product{Id = 4, Name = "Pineapple", Stock = 40, Price = 700, ProfitPerItem = 150},
        new Product{Id = 5, Name = "Papaya", Stock = 30, Price = 800, ProfitPerItem = 50},
    };

    static List<Sale> Cart = new List<Sale>();
    static List<SaleRecord> saleRecords = new List<SaleRecord>();
    static double TotalRevenue = 0;
    static decimal TotalProfit = 0;

    static void Main(string[] args)
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine();
            Console.WriteLine("Pls Type the Number to Execute...");
            Console.WriteLine("1. Stock Menu");
            Console.WriteLine("2. Cashier Menu");
            Console.WriteLine("3. Manager Menu");
            Console.WriteLine("4. End Program");
            var menu = Console.ReadLine();
            Console.WriteLine();

            switch (menu)
            {
                case "1":
                    while (true)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("PODUCT MENU");
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
                                ShowProduct();
                                break;

                            case "2":
                                EditProduct();
                                break;

                            case "3":
                                AddProduct();
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
                        Dictionary<Product, int> cart = new Dictionary<Product, int>();
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
                                AddtoCart();
                                break;

                            case "2":
                                SummarizeOrder();
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
                                ShowSaleRecord();
                                break;

                            case "2":
                                ShowTotalSummary();
                                break;

                            case "3":
                                break;
                        }
                        if(managerMenu == "3") break;
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
    }

    static void ShowProduct()
    {
        Console.WriteLine("{0,-10} {1,-20} {2,-10} {3,-10} {4,-10}", "ID", "Name", "Stock", "Price", "Profit");
        Console.WriteLine(new string('-', 60));

        Console.ForegroundColor = ConsoleColor.Green;
        foreach (var product in Products)
        {
            Console.WriteLine("{0,-10} {1,-20} {2,-10} {3,-10} {4,-10:C}", product.Id!, product.Name, product.Stock, product.Price, product.ProfitPerItem);
        }
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine();
    }

    static void EditProduct()
    {
        ShowProduct();
        Console.WriteLine("Please Enter the Product ID you want to Edit");
        var editId = Convert.ToInt32(Console.ReadLine());
        var item = Products.Find(x => x.Id == editId);

        if (item is null)
        {
            Console.WriteLine("There's no Product with this ID");
        }

        Console.Write("Enter new Name (leave blank to keep current): ");
        string editName = Console.ReadLine()!;
        if (!string.IsNullOrWhiteSpace(editName)) item!.Name = editName;

        Console.Write("Enter new Stock (leave blank to keep current): ");
        string editStock = Console.ReadLine()!;
        if (!string.IsNullOrWhiteSpace(editStock)) item!.Stock = Convert.ToInt32(editStock);

        Console.Write("Enter new Price (leave blank to keep current): ");
        string editPrice = Console.ReadLine()!;
        if (!string.IsNullOrWhiteSpace(editPrice)) item!.Price = Convert.ToDouble(editPrice);

        Console.Write("Enter new ProfitePerItem (leave blank to keep current): ");
        string editProfit = Console.ReadLine()!;
        if (!string.IsNullOrWhiteSpace(editProfit)) item!.ProfitPerItem = Convert.ToDouble(editProfit);
    }

    static void AddProduct()
    {
        while (true)
        {
            Console.WriteLine("Add New Product");
            Console.Write("Enter the Product Name = ");
            var newName = Console.ReadLine()!;
            if (string.IsNullOrEmpty(newName)) break;

            Console.Write("Enter the Product Stock = ");
            var newStock = Console.ReadLine();
            if (string.IsNullOrEmpty(newStock)) break;

            Console.Write("Enter the Product Price = ");
            var newPrice = Console.ReadLine();
            if (string.IsNullOrEmpty(newPrice)) break;

            Console.Write("Enter the Product Profit = ");
            var newProfit = Console.ReadLine();
            if (string.IsNullOrEmpty(newProfit)) break;

            int newId = Products.Count > 0 ? Products.Last().Id + 1 : 0;
            Products.Add(new Product
            {
                Id = newId,
                Name = newName,
                Stock = Convert.ToInt32(newStock),
                Price = Convert.ToDouble(newPrice),
                ProfitPerItem = Convert.ToDouble(newProfit)
            });

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Successfully Added!");
            Console.WriteLine();
            break;
        }
    }

    static void AddtoCart()
    {
        while (true)
        {
            ShowProduct();
            Console.WriteLine("Please Enter the product ID");
            if (!int.TryParse(Console.ReadLine(), out int itemId))
            {
                Console.WriteLine("Invalid Input!");
            }

            var item = Products.FirstOrDefault(p => p.Id == itemId);
            if (item != null)
            {
                Console.Write("Enter quantity: ");
                if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0 && quantity <= item.Stock)
                {
                    Cart.Add(new Sale
                    {
                        ProductId = item.Id,
                        ProductName = item.Name,
                        Price = item.Price,
                        Quantity = quantity,
                        TotalAmount = item.Price * quantity,
                        Profit = (decimal)(item.ProfitPerItem * quantity),
                    });

                    Console.WriteLine("Product added to cart!");
                }
                else
                {
                    Console.WriteLine("Not Enough Item in Stock.");
                }
            }
            else
            {
                Console.WriteLine("Product not found.");
            }

            Console.WriteLine("Enter (y) to add more. Enter (n) to exist.");
            var userInput = Console.ReadLine();
            if (userInput == "n") break;
        }
    }

    static void SummarizeOrder()
    {
        Console.WriteLine();
        Console.WriteLine("{0,-10} {1,-20} {2,-10} {3,-10} {4,-10}", "ID", "Name", "Price", "Quantity", "TotalAmount");
        Console.WriteLine(new string('-', 60));
        Console.ForegroundColor = ConsoleColor.Cyan;

        foreach (var item in Cart)
        {
            Console.WriteLine("{0,-10} {1,-20} {2,-10} {3,-10} {4,-10:C}", item.ProductId, item.ProductName, item.Price, item.Quantity, item.TotalAmount);
        }

        Console.WriteLine();
        Console.WriteLine("Enter (y) to Finalize the Transaction");
        var input = Console.ReadLine();
        if (input == "y")
        {
            double totalPrice = 0;
            foreach (var item in Cart)
            {
                totalPrice += (item.Price * item.Quantity);
                saleRecords.Add(new SaleRecord
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    SellingPrice = item.Price,
                    Profit = item.Profit
                });
                var product = Products.FirstOrDefault(x => x.Id == item.ProductId);
                product!.Stock -= item.Quantity;
                TotalRevenue += item.TotalAmount;
                TotalProfit += item.Profit;
            }
            Cart.Clear();
            Console.WriteLine($"Your Total Amount : ${totalPrice}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Press Enter to Exist.");
            Console.ReadLine();
        }
    }

    static void ShowSaleRecord()
    {
        Console.WriteLine();
        Console.WriteLine("{0,-20} {1,-20} {2,-10} {3,-20} {4,-10}", "ProductID", "ProductName", "Quantity", "SellingPrice", "Profit");
        Console.WriteLine(new string('-', 80));

        Console.ForegroundColor = ConsoleColor.Green;
        if (saleRecords.Count > 0)
        {
            foreach (var record in saleRecords)
            {
                Console.WriteLine("{0,-20} {1,-20} {2,-10} {3,-20} {4,-10:C}",
                    record.ProductId, record.ProductName, record.Quantity, record.SellingPrice, record.Profit); ;
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("There's no Record Yet!");
        }
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Press Enter to Exit.");
        Console.ReadLine();
    }

    static void ShowTotalSummary()
    {
        Console.WriteLine();
        Console.WriteLine("{0,-20} {1,-20}", "TotalRevenue", "TotalProfit");
        Console.WriteLine(new string('-', 40));

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("{0,-20} {1,-20:C}", TotalRevenue, TotalProfit);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Press Enter to Exit");
        Console.ReadLine();
    }
}