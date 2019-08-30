using Nanr.Api.Managers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Models
{
    public class SignupResultModel
    {
        public SignupResultModel(SignupResponseModel result)
        {
            Success = result.Success;
            Errors = result.Errors;
            if(result.Session != null)
            {
                Session = new SessionModel(result.Session);
            }
        }

        public SignupResultModel(IDictionary<string, string> errors)
        {
            Success = false;
            Errors = errors;
        }
        public bool Success { get; }
        public SessionModel? Session { get; }
        public IDictionary<string, string> Errors { get; }
    }
}
