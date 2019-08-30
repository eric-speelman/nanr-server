using Nanr.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Managers.Models
{
    public class SignupResponseModel
    {
        public SignupResponseModel(bool success, IDictionary<string, string> errors, Session? session)
        {
            (Success, Errors, Session) = (success, errors, session);
        }
        public bool Success { get; }
        public IDictionary<string, string> Errors { get; }
        public Session? Session { get; }
    }
}
