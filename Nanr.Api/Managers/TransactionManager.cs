using Microsoft.EntityFrameworkCore;
using Nanr.Api.Managers.Models;
using Nanr.Data;
using Nanr.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Managers
{
    public class TransactionManager : ITransactionManager
    {
        public TransactionManager(NanrDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ClickTransaction>> GetClickTransactionsRecieved(User user)
        {
            var clicks = await context.Clicks.Where(x => x.Tag.UserId == user.Id)
                .OrderByDescending(x => x.Timestamp)
                .Take(50).ToListAsync();
            return clicks.Select(x => new ClickTransaction(x.Timestamp, x.Page));
        }

        public async Task<IEnumerable<ClickTransaction>> GetClickTransactionsSent(User user)
        {
            var clicks = await context.Clicks.Where(x => x.UserId == user.Id)
                .OrderByDescending(x => x.Timestamp)
                .Take(50).ToListAsync();
            return clicks.Select(x => new ClickTransaction(x.Timestamp, x.Page));
        }

        public async Task<IEnumerable<WithdrawTransaction>> GetWithdrawTransaction(User user)
        {
            var withdraws = await context.Withdraws.Where(x => x.UserId == user.Id)
                .OrderByDescending(x => x.CreatedOn)
                .Take(50).ToListAsync();
            return withdraws.Select(x => new WithdrawTransaction
            {
                Id = x.Id,
                CreatedOn = x.CreatedOn,
                NanrAmount = x.NanrAmount,
                Status = x.Status,
                UsdAmount = x.UsdAmount
            });
        }

        public async Task<(int sent, int recieved)> RecentTransactionCount(User user, int length)
        {
            var start = DateTime.UtcNow;
            var end = start.AddDays(-length);
            var rangeQuery = context.Clicks.Where(x => x.Timestamp >= end && x.Timestamp <= start);
            var sent = await rangeQuery.Where(x => x.UserId == user.Id).CountAsync();
            var recieved = await rangeQuery.Where(x => x.Tag.UserId == user.Id).CountAsync();
            return (sent, recieved);
        }

        private readonly NanrDbContext context;
    }
}
