﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailManagement.ConsoleApp;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
    public double ProfitPerItem { get; set; }
}


