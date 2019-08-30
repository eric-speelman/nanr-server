using Nanr.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Models
{
    public class TagModel
    {
        public TagModel(Tag tag)
        {
            Id = tag.Id;
        }
        public Guid Id { get; set; }
    }
}
