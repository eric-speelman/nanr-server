using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nanr.Api.Filters;
using Nanr.Api.Managers;
using Nanr.Api.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nanr.Api.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        public ContactController(IContactManager contactManager)
        {
            this.contactManager = contactManager;
        }
        // POST api/<controller>
        [AllowAnonymous]
        [HttpPost]
        public async Task Post([FromBody]ContactModel contactModel)
        {
            await contactManager.NewContact(contactModel);
        }

        private readonly IContactManager contactManager;
    }
}
