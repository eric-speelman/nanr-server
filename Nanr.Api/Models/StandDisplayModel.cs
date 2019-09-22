using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Models
{
    public class StandDisplayModel
    {
        public StandDisplayModel(string username, string appUrl, string tagline)
        {
            Username = username;
            AppUrl = appUrl;
            TagLine = tagline;
        }
        public string Username { get; }
        public string AppUrl { get; }
        public string TagLine { get; }
    }
}
