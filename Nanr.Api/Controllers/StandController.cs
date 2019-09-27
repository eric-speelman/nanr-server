using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nanr.Api.Filters;
using Nanr.Api.Managers;
using Nanr.Api.Models;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nanr.Api.Controllers
{
    public class StandController : Controller
    {
        public StandController(IConfiguration configuration, IAuthManager authManager)
        {
            this.authManager = authManager;
            this.configuration = configuration;
        }
        [AllowAnonymous]
        [Route("/s/{username}")]
        public async Task<IActionResult> Index(string username)
        {
            var user = await authManager.GetUser(username);
            if (user == null)
            {
                return NotFound();
            }
            var model = new StandDisplayModel(user.Username, configuration.GetValue<string>("appUrl"), user.Tagline);
            return View(model);
        }

        private readonly IAuthManager authManager;
        private readonly IConfiguration configuration;
    }
}
