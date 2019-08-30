using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Models
{
    public class PurchaseWithBillingModel
    {
        public string? Amount { get; set; }
        public bool Repurchase { get; set; }

    }
}
