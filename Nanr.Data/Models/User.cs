using System;
using System.Collections.Generic;

namespace Nanr.Data.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public int Balance { get; set; }
        public string? RepurchaseAmount { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Click> Clicks { get; set; }
        public ICollection<Withdraw> Withdraws { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
