using System;
using System.Collections.Generic;
using System.Text;

namespace Nanr.Data.Models
{
    public class Withdraw
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public string Email { get; set; }
        public int NanrAmount { get; set; }
        public decimal UsdAmount { get; set; }
        public int TransactionFee { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
