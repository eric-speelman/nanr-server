using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nanr.Api.Models;

namespace Nanr.Api.Controllers
{
    [ApiController]
    public class AccountController : NanrController
    {
        [HttpGet]
        [Route("api/me")]
        public UserModel Me()
        {
            return new UserModel(NanrUser!);
        }
    }
}