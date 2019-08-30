using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Models
{
    public class TagViewModel
    {
        public Guid TagId { get; set; }
        public string? Page { get; set; }
        public Guid? SessionId { get; set; }
    }
}
