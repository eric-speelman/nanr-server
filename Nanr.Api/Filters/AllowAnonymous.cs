using System;

namespace Nanr.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AllowAnonymous : Attribute
    {
    }
}
