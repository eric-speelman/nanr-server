﻿using Nanr.Api.Models;
using Nanr.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Managers
{
    public interface ITagManager
    {
        Task<TagViewResponseModel> View(TagViewModel model);
        Task<Tag> GetDefaultTag(User user);
    }
}
