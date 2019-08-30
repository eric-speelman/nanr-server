using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Nanr.Api.Controllers;
using Nanr.Api.Filters;
using Nanr.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Filters
{
    public class AuthFilter : IAsyncActionFilter
    {
        public AuthFilter(NanrDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor == null)
            {
                context.Result = (await next()).Result;
            }
            else
            {
                var attributes = descriptor.MethodInfo.GetCustomAttributes(inherit: true).ToList();
                attributes.AddRange(descriptor.ControllerTypeInfo.GetCustomAttributes(inherit: true));
                if (attributes.Any(x => x.GetType() == typeof(AllowAnonymous)))
                {
                    context.Result = (await next()).Result;
                }
                else
                {
                    var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                    if (string.IsNullOrWhiteSpace(authHeader))
                    {
                        context.HttpContext.Response.StatusCode = 401;
                    }
                    else
                    {
                        var authParts = authHeader.Trim().Split(' ').ToArray();
                        if (authParts.Count() != 2 || authParts[0].ToLower() != "bearer")
                        {
                            context.HttpContext.Response.StatusCode = 401;
                        }
                        else
                        {
                            if (Guid.TryParse(authParts[1], out Guid token))
                            {
                                var session = await dbContext.Sessions.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == token);
                                if (session == null)
                                {
                                    context.HttpContext.Response.StatusCode = 401;
                                }
                                else
                                {
                                    if (context.Controller is NanrController controller)
                                    {
                                        controller.NanrUser = session.User;
                                    }
                                    context.Result = (await next()).Result;
                                }
                            }
                            else
                            {
                                context.HttpContext.Response.StatusCode = 401;
                            }
                        }
                    }
                }
            }

        }
        NanrDbContext dbContext;
    }
}
