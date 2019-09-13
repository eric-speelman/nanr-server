using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Models
{
    public class ClickModel
    {
        public string? TagId { get; set; }
        public string? PageId { get; set; }
        public string? Page { get; set; }
        public string? Referrer { get; set; }
        public Guid viewId { get; set; }
    }
}
