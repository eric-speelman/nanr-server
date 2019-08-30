using Microsoft.AspNetCore.Mvc;
using Nanr.Api.Filters;
using Nanr.Api.Managers;
using Nanr.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Controllers
{
    public class TagsController : NanrController
    {
        public TagsController(ITagManager tagManager)
        {
            this.tagManager = tagManager;
        }

        [HttpGet]
        [Route("api/tags")]
        public async Task<IEnumerable<TagModel>> GetTags()
        {
            return (await tagManager.GetTags(NanrUser!))
                .Select(x => new TagModel(x));
        }

    private readonly ITagManager tagManager;
    }
}