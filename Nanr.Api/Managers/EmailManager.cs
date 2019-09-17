using Nanr.Data.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nanr.Api.Managers
{
    public class EmailManager : IEmailManager
    {
        public EmailManager(string sendGridKey)
        {
            this.sendGridKey = sendGridKey;
        }

        public async Task SendEmail(string recipientEmail, string recipientName, string fromEmail, string fromName, string subject, string text)
        {
            var client = new SendGridClient(sendGridKey);
            var from = new EmailAddress(fromEmail, fromName);
            var to = new EmailAddress(recipientEmail, recipientName);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, text, null);
            var res = await client.SendEmailAsync(msg);
        }

        public async Task SendWelcome(User user)
        {
            var body = "Welcome";
            await SendEmail(user.Email, user.Username, "support@nanr.io", "Nanr Support", "Welcome to Nanr! Confirm Your Email", body);
        }

        public async Task SendPurchase(User user, Purchase purchase)
        {
            var body = $"Thank you for your recent Nanr purchase. {purchase.NanrAmount} Nanrs have been added to your stash.";
            await SendEmail(user.Email, user.Username, "support@nanr.io", "Nanr Support", "Purchase Confirmation", body);
        }

        public async Task SendWithdraw(User user)
        {
            var body = $"We recieved your withdraw. You will recieve your funds within 5 business days.";
            await SendEmail(user.Email, user.Username, "support@nanr.io", "Nanr Support", "Withdraw Confirmation", body);
        }

        public async Task SendPasswordResetEmail(User user)
        {
            var text = "If you did not request a password reset please disregard this email.  To reset your password please navigate to <a href='https://nanr.io/account/reset'>https://nanr.io/account/reset</a>.";
            await SendEmail(user.Email, user.Username, "support@nanr.io", "Nanr Support", "Nanr Password Reset", text);
        }

        private readonly string sendGridKey;
    }
}
