using Nanr.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Managers
{
    public interface IClickManager
    {
        Task<ClickResponseModel> Click(ClickModel clickModel, Guid userId);
    }
}
