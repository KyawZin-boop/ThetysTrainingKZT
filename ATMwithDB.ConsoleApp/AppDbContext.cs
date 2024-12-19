using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ATMwithDB.ConsoleApp;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(AppSettings.connectionBuilder.ConnectionString);
        }
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> TransactionHistory { get; set; }
}

[Table("Tbl_User")]
public class User
{
    [Key]
    public Guid? UserID { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public decimal? Balance { get; set; }
    public bool? ActiveFlag { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
}

[Table("Tbl_Transaction")]
public class Transaction
{
    [Key]
    public string? TransactionID { get; set; }
    public string? TransactionType { get; set; }
    public decimal? Amount { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
}
