using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nanr.Api.Filters;
using Nanr.Api.Managers;
using Nanr.Api.Models;

namespace Nanr.Api.Controllers
{
    [ApiController]
    public class AccountController : NanrController
    {
        public AccountController(ITransactionManager transactionManager, IAuthManager authManager)
        {
            this.transactionManager = transactionManager;
            this.authManager = authManager;
        }
        [HttpGet]
        [Route("api/me")]
        public UserModel Me()
        {
            return new UserModel(NanrUser!);
        }

        [HttpGet]
        [Route("api/account/home-summary")]
        public async Task<HomeSummaryModel> Summary()
        {
            var recent = await transactionManager.RecentTransactionCount(NanrUser!);
            return new HomeSummaryModel
            {
                Balance = NanrUser!.Balance,
                Recieved = recent.recieved,
                Sent = recent.sent
            };
        }

        [HttpPost]
        [Route("api/account/reset-password")]
        [AllowAnonymous]
        public async Task Reset([FromBody]ResetPasswordStartModel model)
        {
            await authManager.SetResetCode(model.Email!);
        }

        [HttpPost]
        [Route("api/account/reset-password-set")]
        [AllowAnonymous]
        public async Task<ActionResult> ResetSet([FromBody]ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await authManager.ResetPassword(model);
            return Ok();
        }

        [HttpGet]
        [Route("api/account/profile")]
        public ProfileModel Profile()
        {
            return new ProfileModel
            {
                Email = NanrUser!.Email,
                Username = NanrUser!.Username
            };
        }

        [HttpPost]
        [Route("api/account/profile")]
        public async Task<IEnumerable<string>> UpdateProfile(UpdateProfileModel profile)
        {
            if (ModelState.IsValid)
            {
                var error = await authManager.UpdateProfile(profile, NanrUser!);
                if(!string.IsNullOrWhiteSpace(error))
                {
                    return new List<string>()
                    {
                        error
                    };
                }
                return new string[0];
            }
            else
            {
                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => x.Value.Errors.First().ErrorMessage);
                return errors;
            }
        }

        [HttpPost]
        [Route("api/account/change-password")]
        public async Task<IEnumerable<string>> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var hasError = await authManager.ChangePassword(model, NanrUser!);
                if (!hasError)
                {
                    return new List<string>()
                    {
                        "Incorrect password"
                    };
                }
                return new string[0];
            }
            else
            {
                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => x.Value.Errors.First().ErrorMessage);
                return errors;
            }
        }

        [HttpPost]
        [Route("api/account/logout")]
        public async Task Logout()
        {
            await authManager.Logout(SessionId);
        }

        private readonly ITransactionManager transactionManager;
        private readonly IAuthManager authManager;
    }
}