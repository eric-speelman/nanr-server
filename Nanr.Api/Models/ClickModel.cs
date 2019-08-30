using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Models
{
    public class ClickModel
    {
        public Guid? TagId { get; set; }
        public string? Username { get; set; }
        public string? Page { get; set; }
    }
}
