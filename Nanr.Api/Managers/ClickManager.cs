using Microsoft.EntityFrameworkCore;
using Nanr.Data;
using Nanr.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Managers
{
    public class ClickManager : IClickManager
    {
        public ClickManager(NanrDbContext context, IPurchaseManager purchaseManager)
        {
            this.context = context;
            this.purchaseManager = purchaseManager;
        }

        public async Task<IEnumerable<string>> Click(Guid userId, string tagId, string? page)
        {
            var errors = new List<string>();
            using var transaction = context.Database.BeginTransaction(IsolationLevel.RepeatableRead);
            var now = DateTime.UtcNow;
            Tag? tag = null;
            if (Guid.TryParse(tagId, out Guid tagGuid))
            {

                tag = await context.Tags.Include(x => x.User).SingleOrDefaultAsync(x => x.Id == tagGuid);
                if (tag == null)
                {
                    errors.Add(InvalidTagIdError);
                }
            } else
            {
                tag = await context.Tags.Include(x => x.User).SingleOrDefaultAsync(x => x.User.Username == tagId);
                if(tag == null)
                {
                    errors.Add(InvlaidUsernameError);
                }
            }
            if(tag == null)
            {
                errors.Add(NoUsernameOrTagId);
            }
            var user = await context.Users.SingleAsync(x => x.Id == userId);
            if(!errors.Any() && user.Balance <= 0)
            {
                errors.Add(InsufficientFundsError);
            }
            if(errors.Any())
            {
                await transaction.RollbackAsync();
                return errors;
            }
            user.Balance -= 1;
            tag!.User.Balance += 1;
            context.Clicks.Add(new Click
            {
                Id = Guid.NewGuid(),
                TagId = tag!.Id,
                UserId = userId,
                Timestamp = now,
                Page = page
            });
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return errors;
        }

        private readonly NanrDbContext context;
        private readonly IPurchaseManager purchaseManager;
        public static readonly string InvalidSessionIdError = "Invalid session id";
        public static readonly string InvalidTagIdError = "Invalid tag id";
        public static readonly string InsufficientFundsError = "Insufficient Funds";
        public static readonly string InvlaidUsernameError = "Invalid username";
        public static readonly string NoUsernameOrTagId = "No username or tag id provided";
    }
}
