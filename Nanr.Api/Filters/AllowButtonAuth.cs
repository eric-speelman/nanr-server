using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AllowButtonAuth : Attribute
    {
    }
}
