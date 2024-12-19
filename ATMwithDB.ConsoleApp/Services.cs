using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMwithDB.ConsoleApp;

public class Services
{
    private readonly AppDbContext _db = new AppDbContext();

    public void Read()
    {
        List<User> users = _db.Users.ToList();
        Console.WriteLine("{0,-40} {1,-20} {2,-10} {3,-10} {4,-10} {5,-10} {6,-10:C}", "ID", "Name", "Password", "Balance", "ActiveFlag", "CreatedAt", "UpdatedAt");
        Console.WriteLine(new string('-', 110));
        foreach (User user in users)
        {
            Console.WriteLine("{0,-40} {1,-20} {2,-10} {3,-10} {4,-10} {5,-10} {6,-10}",user.UserID, user.UserName, user.Password, user.Balance, user.ActiveFlag, user.CreatedAt, user.UpdatedAt);
        }
    }
}
