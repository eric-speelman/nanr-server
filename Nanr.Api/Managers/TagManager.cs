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
        public async Task View(TagViewModel model)
        {
            Guid? userId = null;
            if(model.SessionId != null)
            {
                userId = await context.Sessions.Where(x => x.Id == model.SessionId).Select(x => x.UserId).SingleOrDefaultAsync();
            }
            Guid tagGuid;
            if(!Guid.TryParse(model.TagId, out tagGuid))
            {
                tagGuid = await context.Tags.Where(x => x.IsDefault && x.User.Username == model.TagId).Select(x => x.Id).SingleOrDefaultAsync();
            }
            var tagView = new TagView
            {
                Id = Guid.NewGuid(),
                TagId = tagGuid,
                Page = model.Page,
                UserId = userId
            };
            context.TagViews.Add(tagView);
            await context.SaveChangesAsync();
        }

        public async Task<Tag> GetDefaultTag(User user)
        {
            return await context.Tags.Where(x => x.UserId == user.Id && x.IsDefault).SingleAsync();
        }

        private readonly NanrDbContext context;
    }
}
