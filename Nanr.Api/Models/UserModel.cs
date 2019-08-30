using Nanr.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Models
{
    public class UserModel
    {
        public UserModel(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Balance = user.Balance;
        }

        public Guid Id { get;  }
        public string Email { get; }
        public int Balance { get; }
    }
}
