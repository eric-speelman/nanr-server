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
            var recent = await transactionManager.RecentTransactionCount(NanrUser!, 7);
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

        private readonly ITransactionManager transactionManager;
        private readonly IAuthManager authManager;
    }
}