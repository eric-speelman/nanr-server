using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Managers.Models
{
    public class ClickTransaction
    {
        public ClickTransaction(DateTime dateTime, string page)
        {
            Timestamp = dateTime;
            Page = page;
        }
        public DateTime Timestamp { get; set; }
        public string Page { get; set; }
    }
}
