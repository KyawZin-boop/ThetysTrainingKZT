using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.ConsoleApp;

public class User
{
    public string UserID { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public double Balance { get; set; }
    public string AccountType { get; set; }

    public User(string username, string password)
    {
        UserID = Guid.NewGuid().ToString();
        UserName = username.Trim();
        Password = password.Trim();
        Balance = 0;
        AccountType = "saving";
    }
}


