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
    public class TransactionController : NanrController
    {
        public TransactionController(ITransactionManager transactionManager)
        {
            this.transactionManager = transactionManager;
        }

        [HttpGet]
        [Route("api/transactions")]
        public async Task<TransactionResponseModel> Get()
        {
            var clicksSent = await transactionManager.GetClickTransactionsSent(NanrUser!);
            var clicksRecieved = await transactionManager.GetClickTransactionsRecieved(NanrUser!);
            var withdraws = await transactionManager.GetWithdrawTransaction(NanrUser!);
            return new TransactionResponseModel(clicksSent, clicksRecieved, withdraws);
        }

        private readonly ITransactionManager transactionManager;
    }
}