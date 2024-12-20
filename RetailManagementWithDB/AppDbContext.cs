using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace RetailManagementWithDB;

public class AppDbContext : DbContext
{
    private readonly SqlConnectionStringBuilder _connBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = "192.168.0.184",
        InitialCatalog = "KZT_RetailManagement",
        UserID = "thetys",
        Password = "P@ssw0rd",
        TrustServerCertificate = true,
    };

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connBuilder.ConnectionString);
        }
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<SaleReport> Report { get; set; }
}

[Table("Tbl_Product")]
public class Product
{
    [Key]
    public Guid? Id { get; set; }
    public string ProductCode { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public decimal ProfitPerItem { get; set; }
}

[Table("Tbl_SaleReport")]
public class SaleReport
{
    [Key]
    public Guid? SaleId { get; set; }
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal SellingPrice { get; set; }
    public decimal Profit { get; set; }
}

public class Cart
{
    public string ProductCode { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Profit { get; set; }
}
