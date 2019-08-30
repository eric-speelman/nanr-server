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
            var tagView = new TagView
            {
                Id = Guid.NewGuid(),
                TagId = model.TagId,
                Page = model.Page,
                UserId = userId
            };
            context.TagViews.Add(tagView);
            await context.SaveChangesAsync();
        }

        private readonly NanrDbContext context;
    }
}
