using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using StudentTestingSystem.Areas.Admin.Controllers;
using StudentTestingSystem.Domain.Models.Account;
using StudentTestingSystem.Models;
using StudentTestingSystem.Services.Abstract.SuperAdmin;
using StudentTestingSystem.Services.TransportModels.SuperAdmin.Profile.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IndexViewModel = StudentTestingSystem.Areas.SuperAdmin.Models.Profile.IndexViewModel;

namespace StudentTestingSystem.Areas.SuperAdmin.Controllers
{
    [Route("SuperAdmin/[controller]/[action]")]
    public class ProfileController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        private readonly IProfileSuperAdminService _profileSuperAdminService;

        public ProfileController(IProfileSuperAdminService profileSuperAdminService)
        {
            _profileSuperAdminService = profileSuperAdminService;
        }

        public ProfileController(
            ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            ApplicationRoleManager roleManager,
            IProfileSuperAdminService profileSuperAdminService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _profileSuperAdminService = profileSuperAdminService;
            RoleManager = roleManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        // GET: /SuperAdmin/Profile/Index
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<ActionResult> Index(string roleName = "User", int page = 1, int pageSize = 10)
        {
            var role = await RoleManager.FindByNameAsync(roleName);

            var profiles = await _profileSuperAdminService.GetProfilesByRoleIdAsync(role.Id, page, pageSize);

            var model = new IndexViewModel
            {
                Role = roleName,
                PageInfo = profiles,
                Profiles = profiles.Results
            };

            return View(model);
        }

        // POST: /SuperAdmin/Profile/Delete
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<ActionResult> Delete(Guid profileId, string role)
        {
            string userId = await _profileSuperAdminService.GetUserIdByProfileIdAsync(profileId);
            await _profileSuperAdminService.DeleteProfileAsync(profileId);
            DeleteUserInIdentity(userId);

            return RedirectToAction("Index", "Profile", new { roleName = role });
        }

        private async void DeleteUserInIdentity(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            var logins = user.Logins;
            var rolesForUser = await UserManager.GetRolesAsync(id);

            foreach (var login in logins.ToList())
            {
                await UserManager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
            }

            if (rolesForUser.Count() > 0)
            {
                foreach (var item in rolesForUser.ToList())
                {
                    var result = await UserManager.RemoveFromRoleAsync(user.Id, item);
                }
            }

            await UserManager.DeleteAsync(user);
        }

        // GET: /SuperAdmin/Profile/ResetPassword
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> ResetPassword(Guid profileId, string role)
        {
            var profile = await _profileSuperAdminService.GetProfileByProfileIdAsync(profileId);
            var model = new Models.Profile.ResetPasswordViewModel
            {
                Id = profileId,
                Role = role
            };

            ViewBag.Title = $"Reset password for {profile.FirstName} {profile.LastName}";
            return View(model);
        }

        // POST: /SuperAdmin/Profile/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(Models.Profile.ResetPasswordViewModel model)
        {
            var profile = await _profileSuperAdminService.GetProfileByProfileIdAsync(model.Id);
            if (!ModelState.IsValid)
            {
                ViewBag.Title = $"Reset password for {profile.FirstName} {profile.LastName}";
                return View(model);
            }
            var user = await UserManager.FindByEmailAsync(profile.Email);
            if (user == null)
            {
                return RedirectToAction("ResetPassword", "Profile", new { profileId = model.Id, role = model.Role });
            }
            var code = await UserManager.GeneratePasswordResetTokenAsync(profile.UserId);
            var result = await UserManager.ResetPasswordAsync(user.Id, code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Profile", new { roleName = model.Role });
            }
            AddErrors(result);
            ViewBag.Title = $"Reset password for {profile.FirstName} {profile.LastName}";
            return View();
        }

        // GET: /SuperAdmin/Profile/Register
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.Title = "Register a new teacher";
            return View();
        }

        // POST: /SuperAdmin/Profile/Register
        // This action for registration Admins by SuperAdmin
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(AdminRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _profileSuperAdminService.CreateProfileAsync(new CreateProfileRequest
                    {
                        Email = user.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        UserId = user.Id
                    });

                    await UserManager.AddToRoleAsync(user.Id, "Admin");
                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    return RedirectToAction("Index", "Profile", new { roleName = "Admin", Area = "SuperAdmin" });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            ViewBag.Title = "Register a new teacher";
            return View(model);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                if (error.Contains("Name") && error.Contains("is already taken."))
                {
                    continue;
                }

                if (error.Contains("Email") && error.Contains("is already taken."))
                {
                    ModelState.AddModelError("Email", error);
                }
                else
                {
                    ModelState.AddModelError("", error);
                }
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}