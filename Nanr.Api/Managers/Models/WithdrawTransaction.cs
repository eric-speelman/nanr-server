using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Managers.Models
{
    public class WithdrawTransaction
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get;set; }
        public int Status { get; set; }
        public decimal UsdAmount { get; set; }
        public int NanrAmount { get; set; }
    }
}
