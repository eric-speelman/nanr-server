using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nanr.Api.Filters;
using Nanr.Api.Managers;
using Nanr.Api.Models;

namespace Nanr.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IWebHostEnvironment hostEnvironment, IAuthManager authManager)
        {
            this.hostEnvironment = hostEnvironment;
            this.authManager = authManager;
        }
        [Route("api/login")]
        [HttpPost]
        public async Task<ActionResult<SessionModel>> Login(LoginModel loginModel)
        {
            if(loginModel.Email == null || loginModel.Password == null)
            {
                return Unauthorized();
            }
            var session = await authManager.CreateSession(loginModel.Email!, loginModel.Password!);

            if(session != null)
            {
                return Ok(new SessionModel(session));
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("api/signup")]
        public async Task<SignupResultModel> Signup([FromBody]SignupModel signupModel)
        {
            if(ModelState.IsValid)
            {
                return (new SignupResultModel(await authManager.Signup(signupModel)));
            }
            else
            {
                var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key.ToLower(),
                    kvp => kvp.Value.Errors.First().ErrorMessage
                );
                return new SignupResultModel(errors);
            }
        }

        private readonly IAuthManager authManager;
        private readonly IWebHostEnvironment hostEnvironment;
    }
}