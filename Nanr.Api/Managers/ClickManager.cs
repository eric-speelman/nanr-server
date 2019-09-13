using Microsoft.EntityFrameworkCore;
using Nanr.Api.Models;
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

        public async Task<ClickResponseModel> Click(ClickModel clickModel, Guid userId)
        {
            var errors = new List<string>();
            var response = new ClickResponseModel(errors);
            using var transaction = context.Database.BeginTransaction(IsolationLevel.RepeatableRead);
            var now = DateTime.UtcNow;
            Tag? tag = null;
            if (Guid.TryParse(clickModel.TagId, out Guid tagGuid))
            {

                tag = await context.Tags.Include(x => x.User).SingleOrDefaultAsync(x => x.Id == tagGuid);
                if (tag == null)
                {
                    errors.Add(InvalidTagIdError);
                }
            } else
            {
                tag = await context.Tags.Include(x => x.User).SingleOrDefaultAsync(x => x.User.Username == clickModel.TagId);
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
                return response;
            }
            user.Balance -= 1;
            tag!.User.Balance += 1;
            context.Clicks.Add(new Click
            {
                Id = Guid.NewGuid(),
                TagId = tag!.Id,
                UserId = userId,
                Timestamp = now,
                ViewId = clickModel.viewId,
                PageId = clickModel.PageId,
                Page = clickModel.Page,
                Referrer = clickModel.Referrer
            });
            await context.SaveChangesAsync();
            await transaction.CommitAsync();

            var tagOwner = tag.UserId;
            response.TotalNanrCount = await context.Clicks.Where(x => x.UserId == userId && x.Tag.UserId == tagOwner).CountAsync();
            if (!string.IsNullOrWhiteSpace(clickModel.PageId))
            {
                response.PageNanrCount = await context.Clicks.Where(x => x.UserId == userId && x.PageId == clickModel.PageId && x.Tag.UserId == tagOwner).CountAsync();
            }
            return response;
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
