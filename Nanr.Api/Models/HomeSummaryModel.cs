using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Models
{
    public class HomeSummaryModel
    {
        public int Balance { get; set; }
        public int Recieved { get; set; }
        public int Sent { get; set; }
    }
}
