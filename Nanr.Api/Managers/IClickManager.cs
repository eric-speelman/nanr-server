using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Managers
{
    public interface IClickManager
    {
        Task<IEnumerable<string>> Click(Guid userId, string tagId, string? page);
    }
}
