using Nanr.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Models
{
    public class TagModel
    {
        public TagModel(Tag tag, User user)
        {
            Id = tag.Id;
            Username = user.Username;
        }
        public Guid Id { get; set; }
        public string Username { get; set; }
    }
}
