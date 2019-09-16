using System;
using System.Collections.Generic;
using System.Text;

namespace Nanr.Data.Models
{
    public class Purchase
    {
        public Guid Id { get; set; }
        public decimal UsdAmount { get; set; }
        public int NanrAmount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
