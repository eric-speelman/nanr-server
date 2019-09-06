using Nanr.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Managers
{
    public interface IContactManager
    {
        Task NewContact(ContactModel contactModel);
    }
}
