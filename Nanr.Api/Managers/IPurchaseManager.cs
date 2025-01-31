﻿using Nanr.Api.Models;
using Nanr.Data.Models;
using System.Threading.Tasks;

namespace Nanr.Api.Managers
{
    public interface IPurchaseManager
    {
        Task<PurchaseResponseModel> Purchase(PurchaseModel purchaseModel, User user);
        Task<string?> Withdraw(int amount, string email, User user);
    }
}
