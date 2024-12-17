using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM;

public class User
{
    public string UserID { get; set; }
    public string UserName {  get; set; }
    public string Password { get; set; }
    public double Balance { get; set; }
    public string AccountType { get; set; }
    public User(string username, string password)
    {
        this.UserID = Guid.NewGuid().ToString();
        this.UserName = username.Trim();
        this.Password = password.Trim();
        this.Balance = 0;
        this.AccountType = "saving";
    }
}


