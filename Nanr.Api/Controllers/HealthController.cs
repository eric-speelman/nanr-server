using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nanr.Api.Filters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nanr.Api.Controllers
{
    [AllowAnonymous]
    public class HealthController : NanrController
    {
        [Route("api/health")]
        public string Index()
        {
            return "ok";
        }
    }
}
