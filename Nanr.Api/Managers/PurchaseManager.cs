using Microsoft.EntityFrameworkCore;
using Nanr.Api.Models;
using Nanr.Data;
using Nanr.Data.Models;
using Square.Connect.Api;
using Square.Connect.Client;
using Square.Connect.Model;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Managers
{
    public class PurchaseManager : IPurchaseManager
    {
        public PurchaseManager(NanrDbContext context, string squareApiBase, string squareApiKey, IEmailManager emailManager)
        {
            this.context = context;
            this.configuration = new Configuration(new ApiClient(squareApiBase));
            this.configuration.AccessToken = squareApiKey;
            this.emailManager = emailManager;
        }

        public async Task<PurchaseResponseModel> Purchase(PurchaseModel purchaseModel, User user)
        {
            var purchaseResponse = new PurchaseResponseModel
            {
                Success = false,
                Nanrs = 0
            };
            try
            {
                if(purchaseModel.SaveBilling)
                {
                    CustomersApi customerApi = new CustomersApi(configuration);
                    if (string.IsNullOrWhiteSpace(user.BillingId))
                    {
                        CreateCustomerRequest createCustomer = new CreateCustomerRequest(Guid.NewGuid().ToString(), Nickname: user.Username, EmailAddress: user.Email, ReferenceId: user.Id.ToString());
                        var customerResponse = await customerApi.CreateCustomerAsync(createCustomer);
                        if (customerResponse.Errors != null && customerResponse.Errors.Any())
                        {
                            return purchaseResponse;
                        }
                        user.BillingId = customerResponse.Customer.Id;
                    }
                    CreateCustomerCardRequest cardRequest = new CreateCustomerCardRequest(purchaseModel.Token);
                    var cardResponse = await customerApi.CreateCustomerCardAsync(user.BillingId, cardRequest);
                    if(cardResponse.Errors != null && cardResponse.Errors.Any())
                    {
                        return purchaseResponse;
                    }
                    user.CardId = cardResponse.Card.Id;
                }
                (int price, int amount) = GetPricing(purchaseModel.Amount!);
                string idempotencyKey = Guid.NewGuid().ToString();
                Money money = new Money(price, "USD");
                CreatePaymentRequest body;
                if (purchaseModel.UseSaved || purchaseModel.SaveBilling)
                {
                    body = new CreatePaymentRequest(SourceId: user.CardId, CustomerId: user.BillingId, IdempotencyKey: idempotencyKey, AmountMoney: money, Note: $"{user.Id} {purchaseModel.Amount}");
                }
                else
                {
                    body = new CreatePaymentRequest(SourceId: purchaseModel.Token, IdempotencyKey: idempotencyKey, AmountMoney: money, Note: $"{user.Id} {purchaseModel.Amount}");
                }
                PaymentsApi paymentsApi = new PaymentsApi(configuration);
                var response = await paymentsApi.CreatePaymentAsync(body);
                if (response.Payment.Status == "COMPLETED")
                {
                    user.Balance += amount;
                    user.Repurchase = purchaseModel.Refill;
                    if (user.Repurchase)
                    {
                        user.RepurchaseAmount = purchaseModel.Amount;
                    } else
                    {
                        user.RepurchaseAmount = null;
                    }
                    var purchase = new Purchase
                    {
                        Id = Guid.NewGuid(),
                        NanrAmount = amount,
                        UsdAmount = price,
                        Timestamp = DateTime.UtcNow
                    };
                    context.Purchases.Add(purchase);
                    await emailManager.SendPurchase(user, purchase);
                } else
                {
                    return purchaseResponse;
                }
                await context.SaveChangesAsync();
                purchaseResponse.Success = true;
                purchaseResponse.Nanrs = amount;
                return purchaseResponse;
            } catch(Exception ex)
            {
                Console.WriteLine(ex);
                return purchaseResponse;
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
            withdraw.UsdAmount = (amount - 1) * (decimal)0.18;
            if(user.ReferrerId != null)
            {
                var referrer = await context.Users.Where(x => x.Id == user.ReferrerId).SingleAsync();
                var totalNanrs = amount - 1 + referrer.RefferrerRemainder;
                withdraw.ReferralId = referrer.Id;
                withdraw.RefferalAmount = totalNanrs / 10;
                referrer.Balance += totalNanrs / 10;
                referrer.RefferrerRemainder = totalNanrs % 10;
            }
            context.Withdraws.Add(withdraw);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            await emailManager.SendWithdraw(user);
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
                price = 2400;
                amount = 100;
            }
            return (price, amount);
        }

        private readonly NanrDbContext context;
        readonly Configuration configuration;
        private IEmailManager emailManager;
    }
}
