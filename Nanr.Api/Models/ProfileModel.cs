using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Models
{
    public class ProfileModel
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Tagline { get; set; }
        public string? Bio { get; set; }
        public string? BackgroundColor { get; set; }
        public bool DarkText { get; set; }
    }
}
