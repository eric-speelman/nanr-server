using Nanr.Api.Models;
using Nanr.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Managers
{
    public interface ITagManager
    {
        Task View(TagViewModel model);
    }
}
