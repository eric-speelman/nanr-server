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

        [HttpPost]
        public async Task<ClickResponseModel> Click(ClickModel clickModel)
        {
            return await clickManager.Click(clickModel, NanrUser!.Id);
        }

        private readonly IClickManager clickManager;
    }
}