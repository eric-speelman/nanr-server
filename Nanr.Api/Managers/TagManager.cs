using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nanr.Api.Models;
using Nanr.Data;
using Nanr.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Managers
{
    public class TagManager : ITagManager
    {
        public TagManager(NanrDbContext context)
        {
            this.context = context;
        }
        [HttpPost]
        public async Task<TagViewResponseModel> View(TagViewModel model)
        {
            Guid? userId = null;
            if (model.SessionId != null)
            {
                userId = await context.Sessions.Where(x => x.Id == model.SessionId).Select(x => x.UserId).SingleOrDefaultAsync();
                if (Guid.Empty == userId)
                {
                    userId = null;
                }
            }
            Guid tagGuid;
            if (!Guid.TryParse(model.TagId, out tagGuid))
            {
                tagGuid = await context.Tags.Where(x => x.IsDefault && x.User.Username == model.TagId).Select(x => x.Id).SingleOrDefaultAsync();
            }
            var tagOwner = await context.Tags.Where(x => x.Id == tagGuid).Select(x => x.UserId).SingleOrDefaultAsync();
            var tagView = await context.TagViews.Where(x => x.Id == model.ViewId).SingleOrDefaultAsync();
            if (tagView == null)
            {
                tagView = new TagView();
                context.TagViews.Add(tagView);
            }

            tagView.Id = model.ViewId;
            tagView.TagId = tagGuid;
            tagView.Page = model.Page;
            tagView.Referrer = model.Referrer;
            tagView.PageId = model.PageId;
            tagView.UserId = userId;
            tagView.Timestamp = DateTime.UtcNow;
            
            await context.SaveChangesAsync();
            var response = new TagViewResponseModel();
            if (userId == null)
            {
                response.HasUser = false;
                return response;
            }
            response.HasUser = true;

            response.TotalNanrCount = await context.Clicks.Where(x => x.UserId == userId && x.Tag.UserId == tagOwner).CountAsync();


            if (!string.IsNullOrWhiteSpace(model.PageId))
            {
                response.PageNanrCount = await context.Clicks.Where(x => x.UserId == userId && x.PageId == model.PageId && x.Tag.UserId == tagOwner).CountAsync();
            }
            return response;
        }

        public async Task<Tag> GetDefaultTag(User user)
        {
            return await context.Tags.Where(x => x.UserId == user.Id && x.IsDefault).SingleAsync();
        }

        private readonly NanrDbContext context;
    }
}
