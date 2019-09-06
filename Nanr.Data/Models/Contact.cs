using System;
using System.Collections.Generic;
using System.Text;

namespace Nanr.Data.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
    }
}
