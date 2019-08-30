using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Models
{
    public class WithdrawResponseModel
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
}
