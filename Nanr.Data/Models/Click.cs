﻿using System;

namespace Nanr.Data.Models
{
    public class Click
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
        public Guid ViewId { get; set; }
        public string Page { get; set; }

        public string Referrer { get; set; }
        public string PageId { get; set; }
    }
}
