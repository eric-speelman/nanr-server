using Microsoft.EntityFrameworkCore;
using Nanr.Api.Models;
using Nanr.Data;
using Nanr.Data.Models;
using Square.Connect.Api;
using Square.Connect.Client;
using Square.Connect.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Managers
{
    public class PurchaseManager : IPurchaseManager
    {
        public PurchaseManager(NanrDbContext context, string squareApiBase, string squareApiKey)
        {
            this.context = context;
            this.configuration = new Configuration(new ApiClient(squareApiBase));
            this.configuration.AccessToken = squareApiKey;
        }

        public async Task<bool> Purchase(PurchaseModel purchaseModel, User user)
        {
            try
            {
                (int price, int amount) = GetPricing(purchaseModel.Amount!);
                string idempotencyKey = Guid.NewGuid().ToString();
                Money money = new Money(price, "USD");
                CreatePaymentRequest body = new CreatePaymentRequest(SourceId: purchaseModel.Token, IdempotencyKey: idempotencyKey, AmountMoney: money, Note: $"{user.Id} {purchaseModel.Amount}");
                PaymentsApi paymentsApi = new PaymentsApi(configuration);
                var response = await paymentsApi.CreatePaymentAsync(body);
                if (response.Payment.Status == "COMPLETED")
                {
                    user.Balance += amount;
                } else
                {
                    return false;
                }
                await context.SaveChangesAsync();
                return true;
            } catch(Exception)
            {
                return false;
            }
        }

        public async Task<string?> Withdraw(int amount, string email, User user)
        {
            if(amount < 2)
            {
                return "Must withdraw at least 2 Nanrs";
            }
            var transaction = await context.Database.BeginTransactionAsync(IsolationLevel.RepeatableRead);
            var currentUser = await context.Users.Where(x => x.Id == user.Id).SingleAsync();
            if(currentUser.Balance < amount)
            {
                await transaction.RollbackAsync();
                return "Insufficient funds";
            }
            currentUser.Balance -= amount;
            var withdraw = new Withdraw
            {
                Id = Guid.NewGuid(),
                NanrAmount = amount,
                CreatedOn = DateTime.UtcNow,
                TransactionFee = 1,
                UserId = user.Id,
                Status = 1,
                Email = email
            };
            withdraw.UsdAmount = (amount - 1) * (decimal)0.2;
            context.Withdraws.Add(withdraw);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return null;
        }

        private (int price, int amount) GetPricing(string amountType)
        {
            int price = 0;
            int amount = 0;
            var type = amountType.ToLower();
            if (type == "bushel")
            {
                price = 500;
                amount = 20;
            }
            else if (type == "bucket")
            {
                price = 1200;
                amount = 50;
            }
            else if (type == "mountain")
            {
                price = 2200;
                amount = 100;
            }
            return (price, amount);
        }

        private readonly NanrDbContext context;
        readonly Configuration configuration;
    }
}
