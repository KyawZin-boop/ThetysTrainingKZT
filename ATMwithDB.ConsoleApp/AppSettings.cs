using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ATMwithDB.ConsoleApp;

public class AppSettings
{
    public static SqlConnectionStringBuilder connectionBuilder { get; } = new SqlConnectionStringBuilder()
    {
        DataSource = "192.168.0.184",
        InitialCatalog = "KZT_ATM",
        UserID = "thetys",
        Password = "P@ssw0rd",
        TrustServerCertificate = true,
    };
}
