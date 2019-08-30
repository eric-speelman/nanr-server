using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Tag>> GetTags(User user)
        {
            return await context.Tags.Where(x => x.UserId == user.Id).ToListAsync();
        }

        private readonly NanrDbContext context;
    }
}
