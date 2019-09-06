using Nanr.Api.Models;
using Nanr.Data;
using Nanr.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Managers
{
    public class ContactManager : IContactManager
    {
        public ContactManager(NanrDbContext context)
        {
            this.context = context;
        }

        public async Task NewContact(ContactModel contactModel)
        {
            context.Contacts.Add(new Contact
            {
                Id = Guid.NewGuid(),
                Email = contactModel.Email,
                Message = contactModel.Message,
                Timestamp = DateTime.UtcNow
            });
            await context.SaveChangesAsync();
        }

        private readonly NanrDbContext context;
    }
}
