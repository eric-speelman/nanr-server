using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nanr.Api.Filters;
using Nanr.Api.Managers;
using Nanr.Api.Models;

namespace Nanr.Api.Controllers
{
    [ApiController]
    public class AccountController : NanrController
    {
        public AccountController(ITransactionManager transactionManager, IAuthManager authManager)
        {
            this.transactionManager = transactionManager;
            this.authManager = authManager;
        }
        [HttpGet]
        [Route("api/me")]
        public UserModel Me()
        {
            return new UserModel(NanrUser!);
        }

        [HttpGet]
        [Route("api/account/home-summary")]
        public async Task<HomeSummaryModel> Summary()
        {
            var recent = await transactionManager.RecentTransactionCount(NanrUser!);
            return new HomeSummaryModel
            {
                Balance = NanrUser!.Balance,
                Recieved = recent.recieved,
                Sent = recent.sent
            };
        }

        [HttpPost]
        [Route("api/account/reset-password")]
        [AllowAnonymous]
        public async Task Reset([FromBody]ResetPasswordStartModel model)
        {
            await authManager.SetResetCode(model.Email!);
        }

        [HttpPost]
        [Route("api/account/reset-password-set")]
        [AllowAnonymous]
        public async Task<ActionResult> ResetSet([FromBody]ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await authManager.ResetPassword(model);
            return Ok();
        }

        [HttpGet]
        [Route("api/account/profile")]
        public ProfileModel Profile()
        {
            return new ProfileModel
            {
                Id = NanrUser!.Id,
                Email = NanrUser!.Email,
                Username = NanrUser!.Username,
                BackgroundColor = NanrUser!.BackgroundColor,
                Tagline = NanrUser!.Tagline,
                Bio = NanrUser!.Bio,
                DarkText = NanrUser!.isStandTextDark
            };
        }

        [HttpPost]
        [Route("api/account/profile")]
        public async Task<IEnumerable<string>> UpdateProfile(UpdateProfileModel profile)
        {
            if (ModelState.IsValid)
            {
                var error = await authManager.UpdateProfile(profile, NanrUser!);
                if(!string.IsNullOrWhiteSpace(error))
                {
                    return new List<string>()
                    {
                        error
                    };
                }
                return new string[0];
            }
            else
            {
                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => x.Value.Errors.First().ErrorMessage);
                return errors;
            }
        }

        [HttpPost]
        [Route("api/account/change-password")]
        public async Task<IEnumerable<string>> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var hasError = await authManager.ChangePassword(model, NanrUser!);
                if (!hasError)
                {
                    return new List<string>()
                    {
                        "Incorrect password"
                    };
                }
                return new string[0];
            }
            else
            {
                var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => x.Value.Errors.First().ErrorMessage);
                return errors;
            }
        }

        [HttpPost]
        [Route("api/account/logout")]
        public async Task Logout()
        {
            await authManager.Logout(SessionId);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/account/profile-pic/{username}")]
        public async Task<IActionResult> ProfilePic(string username)
        {
            var user = await authManager.GetUser(username);
            if (user == null)
            {
                return BadRequest();
            }
            var cnt = 0;
            foreach (var extension in extensions)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\profiles\" + user.Id + "." + extension).ToString();
                if(System.IO.File.Exists(path))
                {
                    var bytes = await System.IO.File.ReadAllBytesAsync(path);
                    return File(bytes, mimeTypes[cnt]);
                }
                cnt++;
            }
            var defaultPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\profiles\default.svg");
            var defaultBytes = await System.IO.File.ReadAllBytesAsync(defaultPath);
            return File(defaultBytes, "image/svg+xml");
        }

        [HttpDelete]
        [Route("api/account/stand-pic")]
        public void DeleteStandPic()
        {
            foreach (var extension in extensions)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\stands\" + NanrUser!.Id + "." + extension).ToString();
                System.IO.File.Delete(path.ToString());
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/account/stand-pic/{username}")]
        public async Task<IActionResult> StandPic(string username, bool placeholder = false)
        {
            var user = await authManager.GetUser(username);
            if (user == null)
            {
                return BadRequest();
            }
            var cnt = 0;
            foreach (var extension in extensions)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\stands\" + user.Id + "." + extension).ToString();
                if (System.IO.File.Exists(path))
                {
                    var bytes = await System.IO.File.ReadAllBytesAsync(path);
                    return File(bytes, mimeTypes[cnt]);
                }
                cnt++;
            }
            if(!placeholder)
            {
                return NotFound();
            }
            var defaultPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\stands\no-image.png");
            var defaultBytes = await System.IO.File.ReadAllBytesAsync(defaultPath);
            return File(defaultBytes, "image/png");
        }

        [HttpPost]
        [Route("api/account/upload/profile-pic")]
        public async Task<IActionResult> UploadProfilePic([FromForm]IFormFile file)
        {
            if(file.Length > 1073741824)
            {
                return BadRequest("File too large");
            }
            var extension = file.FileName.Split('.').Last().Trim().ToLower();
            if(!extension.Contains(extension))
            {
                return BadRequest("Unknown file type");
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\profiles\" + NanrUser!.Id + "." + extension);
            var fileStream = System.IO.File.Create(path);
            var readStream = file.OpenReadStream();
            readStream.Seek(0, SeekOrigin.Begin);
            await readStream.CopyToAsync(fileStream);
            fileStream.Close();
            foreach(var delExtension in extensions.Where(x => x != extension))
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\profiles\" + NanrUser!.Id + "." + delExtension));
            }
            return Ok();
        }

        [HttpPost]
        [Route("api/account/upload/stand-pic")]
        public async Task<IActionResult> UploadStandPic([FromForm]IFormFile file)
        {
            if (file.Length > (long)1073741824 * 4)
            {
                return BadRequest("File too large");
            }
            var extension = file.FileName.Split('.').Last().Trim().ToLower();
            if (!extension.Contains(extension))
            {
                return BadRequest("Unknown file type");
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\stands\" + NanrUser!.Id + "." + extension);
            var fileStream = System.IO.File.Create(path);
            var readStream = file.OpenReadStream();
            readStream.Seek(0, SeekOrigin.Begin);
            await readStream.CopyToAsync(fileStream);
            fileStream.Close();
            foreach (var delExtension in extensions.Where(x => x != extension))
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\stands\" + NanrUser!.Id + "." + delExtension));
            }
            return Ok();
        }
        private readonly string[] extensions = { "svg", "jpg", "png", "jpeg" };
        private readonly string[] mimeTypes = { "image/svg+xml", "image/jpg", "image/png", "img/jpg" };
        private readonly ITransactionManager transactionManager;
        private readonly IAuthManager authManager;
    }
}