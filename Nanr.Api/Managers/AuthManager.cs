using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Nanr.Api.Managers.Models;
using Nanr.Api.Models;
using Nanr.Data;
using Nanr.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Nanr.Api.Managers
{
    public class AuthManager : IAuthManager
    {
        public AuthManager(NanrDbContext context, IEmailManager emailManager)
        {
            this.context = context;
            this.emailManager = emailManager;
        }
        public static (string salt, string passwordHash) SaltAndHash(string password)
        {
            byte[] salt = new byte[128 / 8];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            string passwordHash = HashPassword(salt, password);
            return (Convert.ToBase64String(salt), passwordHash);
        }

        public async Task<Session?> CreateSession(string email, string password)
        {
            var user = await context.Users.Where(x => x.Email == email).SingleOrDefaultAsync();
            if (user == null)
            {
                return null;
            }
            var passwordHash = HashPassword(Convert.FromBase64String(user.Salt), password);
            if (passwordHash != user.PasswordHash)
            {
                return null;
            }
            var session = new Session
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                User = user
            };
            context.Sessions.Add(session);
            await context.SaveChangesAsync();
            
            return session;
        }

        public async Task SetResetCode(string email)
        {
            var user = await context.Users.Where(x => x.Email == email).SingleOrDefaultAsync();
            if (user != null)
            {
                user.ResetCode = Guid.NewGuid();
                await context.SaveChangesAsync();
                await emailManager.SendPasswordResetEmail(user);
            }
        }

        public async Task ResetPassword(ResetPasswordModel model)
        {
            var user = await context.Users.Where(x => x.ResetCode == model.Token).SingleAsync();
            var saltHash = SaltAndHash(model.Password!);
            user.PasswordHash = saltHash.passwordHash;
            user.Salt = saltHash.salt;
            user.ResetCode = null;
            await context.SaveChangesAsync();
        }

        public async Task<SignupResponseModel> Signup(SignupModel signupModel)
        {
            var errors = new Dictionary<string, string>();
            if(await context.Users.AnyAsync(x => x.Email == signupModel.Email))
            {
                errors.Add("email", "Email address already being used");
            }
            if(await context.Users.AnyAsync(x => x.Username == signupModel.Username))
            {
                errors.Add("username", "Username already being used");
            }
            if(errors.Any())
            {
                return new SignupResponseModel(false, errors, null);
            }
            var saltHash = SaltAndHash(signupModel.Password!);
            var user = new User
            {
                Id = Guid.NewGuid(),
                Balance = 0,
                CreatedOn = DateTime.UtcNow,
                Email = signupModel.Email,
                Username = signupModel.Username,
                Salt = saltHash.salt,
                PasswordHash = saltHash.passwordHash
            };
            var tag = new Tag
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                IsDefault = true
            };
            context.Users.Add(user);
            context.Tags.Add(tag);
            await context.SaveChangesAsync();
            var session = await CreateSession(signupModel.Email!, signupModel.Password!);
            await emailManager.SendWelcome(user);
            return new SignupResponseModel(true, errors, session);
        }

        private static string HashPassword(byte[] salt, string password)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }

        private NanrDbContext context;
        private readonly IEmailManager emailManager;
    }
}
