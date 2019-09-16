using Nanr.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Managers
{
    public interface IEmailManager
    {
        Task SendEmail(string recipientEmail, string recipientName, string fromEmail, string fromName, string subject, string text);
        Task SendWelcome(User user);
        Task SendPurchase(User user, Purchase purchase);
        Task SendWithdraw(User user);
    }
}
