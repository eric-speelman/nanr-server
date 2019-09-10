using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nanr.Api.Managers;
using Nanr.Api.Models;

namespace Nanr.Api.Controllers
{
    [ApiController]
    public class AccountController : NanrController
    {
        public AccountController(ITransactionManager transactionManager)
        {
            this.transactionManager = transactionManager;
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

        private ITransactionManager transactionManager;
    }
}