using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Models
{
    public class ClickResponseModel
    {
        public ClickResponseModel(IEnumerable<string> errors)
        {
            Errors = errors;
        }
        public bool Success => !Errors.Any();
        public IEnumerable<string> Errors { get; }
        public int? PageNanrCount { get; set; }
        public int? TotalNanrCount { get; set; }
    }
}
