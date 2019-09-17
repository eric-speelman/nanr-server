using Nanr.Api.Managers.Models;
using Nanr.Api.Models;
using Nanr.Data.Models;
using System;
using System.Threading.Tasks;

namespace Nanr.Api.Managers
{
    public interface IAuthManager
    {
        Task<Session?> CreateSession(string email, string password);
        Task<SignupResponseModel> Signup(SignupModel signupModel);
        Task SetResetCode(string email);
        Task ResetPassword(ResetPasswordModel model);
    }
}
