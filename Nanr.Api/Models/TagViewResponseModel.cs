using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Models
{
    public class TagViewResponseModel
    {
        public bool HasUser { get; set; }
        public int? PageNanrCount { get; set; }
        public int? TotalNanrCount { get; set; }
    }
}
