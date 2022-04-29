using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMED.Model
{
    public class BalanceResponse
    {
        public int? code { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
    public class Data
    {
        public string balance { get; set; }
        public string currency { get; set; }
    }
}
