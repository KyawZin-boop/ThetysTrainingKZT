using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailManagement.ConsoleApp;

public class SaleRecord
{
    public string? SaleId { get; set; } = Guid.NewGuid().ToString();
    public int ProductId { get; set; }
    public string? ProductName { get; set; }
    public int Quantity { get; set; }
    public double? SellingPrice { get; set; }
    public decimal? Profit { get; set; }
}
