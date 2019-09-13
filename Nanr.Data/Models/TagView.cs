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
        public DateTime Timestamp { get; set; }
        public string Page { get; set; }
        public string Referrer { get; set; }
        public string PageId { get; set; }
    }
}
