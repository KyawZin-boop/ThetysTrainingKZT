using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RetailManagementWithDB;

public class Services
{
    private readonly AppDbContext _db = new AppDbContext();

    public List<Cart> cart = new List<Cart>();

    public decimal TotalRevenue { get; set; }
    public decimal TotalProfit { get; set; }

    public void ShowProduct()
    {
        List<Product> products = _db.Products.ToList();
        if (products.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n{0,-10} {1,-10} {2,-10} {3,-10} {4,-20:C}", "Code", "Name", "Stock", "Price", "ProfitPerItem");
            Console.WriteLine(new string('-', 60));
            foreach (Product product in products)
            {
                Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-10} {4,-20}", product.ProductCode, product.Name, product.Stock, product.Price, product.ProfitPerItem);
            }
            Console.ForegroundColor = ConsoleColor.Blue;
        }
    }

    public void EditProduct()
    {
        ShowProduct();
        Console.WriteLine("Please Enter the Product Code you want to Edit");
        var editCode = Console.ReadLine();
        var item = _db.Products.FirstOrDefault(x => x.ProductCode == editCode);

        if (item is not null)
        {
            Console.Write("Enter new Name (leave blank to keep current): ");
            string editName = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(editName)) item!.Name = editName;

            Console.Write("Enter new Stock (leave blank to keep current): ");
            string editStock = Console.ReadLine()!;
            item!.Stock = Convert.ToInt32(editStock);

            Console.Write("Enter new Price (leave blank to keep current): ");
            string price = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(price))
            {
                if (!decimal.TryParse(price, out decimal editPrice))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Input.");
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                item!.Price = Convert.ToDecimal(editPrice);
            }

            Console.Write("Enter new ProfitePerItem (leave blank to keep current): ");
            string profit = Console.ReadLine()!;
            if (!string.IsNullOrEmpty(profit))
            {
                if (!decimal.TryParse(profit, out decimal editProfit))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Input.");
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                item!.ProfitPerItem = Convert.ToDecimal(editProfit);
            }

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Edit Success.");
            Console.ForegroundColor = ConsoleColor.Blue;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("There's no Product with this Code.\n");
            Console.ForegroundColor = ConsoleColor.Blue;
        }
    }

    public void AddProduct()
    {
        try
        {
            while (true)
            {
                Console.WriteLine("Add New Product");
                Console.Write("Enter the Product Code = ");
                var newCode = Console.ReadLine()!;
                var codeCheck = _db.Products.AsNoTracking().FirstOrDefault(x => x.ProductCode == newCode);
                if (codeCheck is null)
                {
                    if (string.IsNullOrEmpty(newCode))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Need Product Code.");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Code Exists!");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                }

                Console.Write("Enter the Product Name = ");
                var newName = Console.ReadLine()!;
                var nameCheck = _db.Products.AsNoTracking().FirstOrDefault(x => x.ProductCode == newCode);
                if (nameCheck is null)
                {
                    if (string.IsNullOrEmpty(newName))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Need Product Name.");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Product Name Exists!");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                }

                Console.Write("Enter the Product Stock = ");
                if (!int.TryParse(Console.ReadLine(), out int newStock))
                {
                    Console.WriteLine("Invalid Input!");
                    break;
                }

                Console.Write("Enter the Product Price = ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal newPrice))
                {
                    Console.WriteLine("Invalid Input!");
                    break;
                }

                Console.Write("Enter the Product Profit = ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal newProfit))
                {
                    Console.WriteLine("Invalid Input!");
                    break;
                }

                _db.Products.Add(new Product
                {
                    ProductCode = newCode,
                    Name = newName,
                    Stock = newStock,
                    Price = newPrice,
                    ProfitPerItem = newProfit
                });
                _db.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Successfully Added!\n");
                Console.ForegroundColor = ConsoleColor.Blue;
                break;
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = ConsoleColor.Blue;
        }
    }

    public List<Cart> AddtoCart()
    {
        while (true)
        {
            ShowProduct();
            Console.WriteLine("Please Enter the Product Code");
            var code = Console.ReadLine();
            if (string.IsNullOrEmpty(code)) continue;

            var item = _db.Products.FirstOrDefault(p => p.ProductCode == code);
            if (item is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Product not found.");
                Console.ForegroundColor = ConsoleColor.Blue;
                continue;
            }

            Console.WriteLine("Enter quantity: ");
            if (int.TryParse(Console.ReadLine(), out int quantity))
            {
                var cartItem = cart.FirstOrDefault(x => x.ProductCode == item.ProductCode);
                if (cartItem is not null)
                {
                    var totalQty = cartItem.Quantity + quantity;
                    if (totalQty <= item.Stock)
                    {
                        cartItem.Quantity += quantity;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Product added to cart!");
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Not Enough Item in Stock!");
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                }
                else
                {
                    cart.Add(new Cart
                    {
                        ProductCode = item.ProductCode,
                        ProductName = item.Name,
                        Price = item.Price,
                        Quantity = quantity,
                        TotalAmount = item.Price * quantity,
                        Profit = item.ProfitPerItem * quantity,
                    });
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Input.");
                Console.ForegroundColor = ConsoleColor.Blue;
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Enter (y) to add more. Enter (n) to exist.");
            var userInput = Console.ReadLine();
            if (userInput == "n") return cart;
        }
    }

    public void SummarizeOrder(List<Cart> cart)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n{0,-10} {1,-20} {2,-10} {3,-10} {4,-10}", "ID", "Name", "Price", "Quantity", "TotalAmount");
        Console.WriteLine(new string('-', 60));

        foreach (var item in cart)
        {
            Console.WriteLine("{0,-10} {1,-20} {2,-10} {3,-10} {4,-10:C}", item.ProductCode, item.ProductName, item.Price, item.Quantity, item.TotalAmount);
        }

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\nEnter (y) to Finalize the Transaction. Enter (n) to exist.");
        var input = Console.ReadLine();
        if (input == "y")
        {
            decimal totalPrice = 0;
            foreach (var item in cart)
            {
                totalPrice += (item.Price * item.Quantity);
                _db.Report.Add(new SaleReport
                {
                    ProductCode = item.ProductCode,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    SellingPrice = item.Price,
                    Profit = item.Profit
                });
                var product = _db.Products.FirstOrDefault(x => x.ProductCode == item.ProductCode);
                product!.Stock -= item.Quantity;
                TotalRevenue += item.TotalAmount;
                _db.Entry(product).State = EntityState.Modified;
                _db.SaveChanges();
            }
            cart.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nYour Total Amount : ${totalPrice}");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Press Enter to Exist.");
            Console.ReadLine();
        }
    }

    public void ShowSaleRecord()
    {
        List<SaleReport> lst = _db.Report.ToList();
        Console.ForegroundColor = ConsoleColor.Green;
        if (lst is null)
        {
            Console.WriteLine("There's no Record Yet!");
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        Console.WriteLine("\n{0,-20} {1,-20} {2,-10} {3,-10} {4,-10}", "ProductCode", "Name", "Price", "Quantity", "TotalAmount");
        Console.WriteLine(new string('-', 70));
        foreach (SaleReport report in lst)
        {
            Console.WriteLine("{0,-20} {1,-20} {2,-10} {3,-10} {4,-10:C}", report.ProductCode, report.ProductName, report.Quantity, report.SellingPrice, report.Profit);
        }

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("\nPress Enter to exist.");
        Console.ReadLine();
    }

    public void ShowTotalSummary()
    {
        List<SaleReport> lst = _db.Report.ToList();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n{0,-20} {1,-20}", "TotalRevenue", "TotalProfit");
        Console.WriteLine(new string('-', 40));

        foreach(SaleReport report in lst)
        {
            TotalRevenue += report.SellingPrice * report.Quantity;
            TotalProfit += report.Profit;
        }

        Console.WriteLine("{0,-20} {1,-20:C}", TotalRevenue, TotalProfit);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Press Enter to Exit");
        Console.ReadLine();
    }
}
