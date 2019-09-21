using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Models
{
    public class SignupModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(12)]
        public string? Username { get; set; }
        [Required]
        [MinLength(7)]
        [MaxLength(32)]
        public string? Password { get; set; }

        public string? Referrer { get; set; }
    }
}
