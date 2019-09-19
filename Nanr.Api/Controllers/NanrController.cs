using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nanr.Data.Models;

namespace Nanr.Api.Controllers
{
    public abstract class NanrController : ControllerBase
    {
        public User? NanrUser { get; set; }
        public Guid SessionId { get; set; }
    }
}