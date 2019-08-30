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

        [HttpPost]
        [Route("api/tags/view")]
        [AllowAnonymous]
        public async Task<ActionResult> TagView([FromBody]TagViewModel tagViewModel) 
        {
            await tagManager.View(tagViewModel);
            return Ok();
        }

    private readonly ITagManager tagManager;
    }
}