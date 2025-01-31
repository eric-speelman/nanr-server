﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Models
{
    public class PurchaseModel
    {
        public string? Amount { get; set; }
        public string? Token { get; set; }
        public bool SaveBilling { get; set; }
        public bool UseSaved { get; set; }
        public bool Refill { get; set; }
    }
}
