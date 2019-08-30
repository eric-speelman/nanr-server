using Nanr.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Models
{
    public class SessionModel
    {
        public SessionModel(Session session)
        {
            Id = session.Id;
            User = new UserModel(session.User);
        }

        public Guid Id { get; }
        public UserModel User { get; }
    }
}
