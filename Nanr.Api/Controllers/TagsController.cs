using Microsoft.AspNetCore.Mvc;
using Nanr.Api.Filters;
using Nanr.Api.Managers;
using Nanr.Api.Models;
using System;
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
        [Route("api/tag")]
        public async Task<TagModel> GetTag()
        {
            return new TagModel(await tagManager.GetDefaultTag(NanrUser!), NanrUser!);
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