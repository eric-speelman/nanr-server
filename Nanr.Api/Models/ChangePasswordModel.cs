using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Models
{
    public class ChangePasswordModel
    {
        public string? Password { get; set; }
        [Required]
        [MinLength(7)]
        [MaxLength(32)]
        public string? NewPassword { get; set; }
    }
}
