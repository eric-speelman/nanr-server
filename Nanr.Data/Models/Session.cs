using System;
using System.Collections.Generic;
using System.Text;

namespace Nanr.Data.Models
{
    public class Session
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
