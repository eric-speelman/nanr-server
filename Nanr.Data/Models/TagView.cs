using System;
using System.Collections.Generic;
using System.Text;

namespace Nanr.Data.Models
{
    public class TagView
    {
        public Guid Id { get; set; }
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
        public Guid? UserId { get; set; }
        public User User { get; set; }
        public string Page { get; set; }
    }
}
