using Microsoft.AspNetCore.Mvc;
using Nanr.Api.Managers;
using Nanr.Api.Managers.Models;
using Nanr.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Controllers
{
    public class PurchaseController : NanrController
    {
        public PurchaseController(IPurchaseManager purchaseManager)
        {
            this.purchaseManager = purchaseManager;
        }
        [Route("api/purchase")]
        [HttpPost]
        public async Task<PurchaseResponseModel> Purchase([FromBody]PurchaseModel purchaseModel)
        {
            var response = new PurchaseResponseModel();
            response.Success = await purchaseManager.Purchase(purchaseModel, NanrUser!);
            return response;
        }
        
        [Route("api/withdraw")]
        [HttpPost]
        public async Task<WithdrawResponseModel> Withdraw([FromBody]WithdrawRequestModel withdrawRequest)
        {
            var err = await purchaseManager.Withdraw(withdrawRequest.Amount, NanrUser!);
            if(err == null)
            {
                return new WithdrawResponseModel
                {
                    Success = true,
                    Error = null,
                };
            } else
            {
                return new WithdrawResponseModel
                {
                    Success = false,
                    Error = err
                };
            }
        }

        private readonly IPurchaseManager purchaseManager;

    }
}
