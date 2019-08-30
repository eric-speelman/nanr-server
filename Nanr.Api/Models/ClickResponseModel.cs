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
            Success = !errors.Any();
        }
        public bool Success { get; }
        public IEnumerable<string> Errors { get; }
    }
}
