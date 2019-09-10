using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nanr.Api.Filters;
using Nanr.Api.Managers;
using Nanr.Api.Managers.Models;
using Nanr.Api.Models;

namespace Nanr.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClickController : NanrController
    {
        public ClickController(IClickManager clickManager)
        {
            this.clickManager = clickManager;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ClickResponseModel> Click(ClickModel clickModel)
        {
            var errors = new List<string>();
            errors.AddRange(await clickManager.Click(NanrUser!.Id, clickModel.TagId!, clickModel.Page));
            return new ClickResponseModel(errors);
        }

        private readonly IClickManager clickManager;
    }
}