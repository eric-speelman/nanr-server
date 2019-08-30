using System;

namespace Nanr.Data.Models
{
    public class Tag
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public bool IsDefault { get; set; }
    }
}
