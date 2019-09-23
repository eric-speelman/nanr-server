using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Models
{
    public class UpdateProfileModel
    {
        [EmailAddress]
        public string? Email { get; set; }
        [MaxLength(100)]
        public string? Tagline { get; set; }
        [MaxLength(250)]
        public string? Bio { get; set; }
        [MaxLength(7)]
        [RegularExpression("^#[a-fA-f0-9]+")]
        public string? BackgroundColor { get; set; }
        public bool? DarkText { get; set; }
        public bool? AutoRefill { get; set; }
    }
}
