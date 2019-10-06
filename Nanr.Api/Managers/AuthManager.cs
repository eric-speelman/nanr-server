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

        public async Task<bool> ConfirmEmail(Guid code)
        {
            var user = await context.Users.SingleOrDefaultAsync(x => x.EmailConfirmationCode == code);
            if (user == null)
            {
                return false;
            }
            user.EmailConfirmationCode = null;
            await context.SaveChangesAsync();
            return true;
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
            Guid? referrerId = null;
            if (!string.IsNullOrWhiteSpace(signupModel.Referrer))
            {
                var username = signupModel.Referrer.Trim();
                referrerId = context.Users.Where(x => x.Username == username).Select(x => x.Id).SingleOrDefault();
            }
            if (errors.Any())
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
                PasswordHash = saltHash.passwordHash,
                Tagline = "Little things add up",
                BackgroundColor = "#FAFAFA",
                isStandTextDark = true,
                ReferrerId = referrerId,
                RefferrerRemainder = 0,
                EmailConfirmationCode = Guid.NewGuid()
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

        public async Task<string?> UpdateProfile(UpdateProfileModel profile, User user)
        {
            var save = false;
            if (!string.IsNullOrWhiteSpace(profile.Email))
            {
                if(await context.Users.AnyAsync(x => x.Id != user.Id && x.Email == profile.Email))
                {
                    return "That email address is already being used";
                }
                user.Email = profile.Email;
                save = true;
            }
            if(profile.Tagline != null)
            {
                user.Tagline = profile.Tagline;
                save = true;
            }

            if (profile.Bio != null)
            {
                user.Bio = profile.Bio;
                save = true;
            }

            if(profile.BackgroundColor != null)
            {
                user.BackgroundColor = profile.BackgroundColor;
                save = true;
            }

            if(profile.DarkText != null)
            {
                user.isStandTextDark = profile.DarkText.Value;
                save = true;
            }
            if(profile.AutoRefill != null)
            {
                user.Repurchase = profile.AutoRefill.Value;
                save = true;
            }
            if(save)
            {
                await context.SaveChangesAsync();
            }
            return null;
        }

        public async Task<bool> ChangePassword(ChangePasswordModel model, User user)
        {
            var currentPasswordHash = HashPassword(Convert.FromBase64String(user.Salt), model.Password!);
            if(currentPasswordHash != user.PasswordHash)
            {
                return false;
            }
            user.PasswordHash = HashPassword(Convert.FromBase64String(user.Salt), model.NewPassword!);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task Logout(Guid sessionId)
        {
            var session = await context.Sessions.Where(x => x.Id == sessionId).SingleAsync();
            context.Sessions.Remove(session);
            await context.SaveChangesAsync();
        }

        public async Task<User?> GetUser(string username)
        {
            User? user = null;
            if(Guid.TryParse(username, out Guid userId))
            {
                user = await context.Users.Where(x => x.Id == userId).SingleOrDefaultAsync();
            }
            if (user == null)
            {
                user = await context.Users.Where(x => x.Username == username).SingleOrDefaultAsync();
            }
            return user;
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
