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
    }
}
