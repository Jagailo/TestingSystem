using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using StudentTestingSystem.Areas.Admin.Models.Teacher;
using StudentTestingSystem.Models;
using StudentTestingSystem.Services.Abstract.Admin;
using StudentTestingSystem.Services.Abstract.SuperAdmin;
using StudentTestingSystem.Services.TransportModels.Admin.Student.Request;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentTestingSystem.Areas.Admin.Controllers
{
    [Route("Admin/[controller]/[action]")]
    public class TeacherController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IStudentAdminService _studentAdminService;
        private readonly IProfileSuperAdminService _profileSuperAdminService;

        public TeacherController(
            IStudentAdminService studentAdminService,
            IProfileSuperAdminService profileSuperAdminService)
        {
            _studentAdminService = studentAdminService;
            _profileSuperAdminService = profileSuperAdminService;
        }

        public TeacherController(
            ApplicationSignInManager signInManager,
            ApplicationUserManager userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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

        // GET: /Admin/Teacher/Index
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<ActionResult> Index()
        {
            var profile = await _profileSuperAdminService.GetProfileByUserIdAsync(GetCurrentUserId());

            ViewBag.Title = "Your account";
            return View(profile);
        }

        // GET: /Admin/Teacher/Edit
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<ActionResult> Edit()
        {
            var profile = await _profileSuperAdminService.GetProfileByUserIdAsync(GetCurrentUserId());

            TeacherEditViewModel adminEditViewModel = new TeacherEditViewModel()
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = profile.Email
            };

            ViewBag.Title = "Editing an account";
            return View(adminEditViewModel);
        }

        // POST: /Admin/Teacher/Edit
        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<ActionResult> Edit(TeacherEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                UpdateStudentProfileRequest updateStudentProfileRequest = new UpdateStudentProfileRequest()
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    GroupId = null
                };
                await _studentAdminService.UpdateProfileAsync(updateStudentProfileRequest);
                return RedirectToAction("Index");
            }
            ViewBag.Title = "Editing an account";
            return View(model);

        }

        // GET: /Admin/Teacher/ChangePassword
        [Authorize(Roles = "Admin, SuperAdmin")]
        public ActionResult ChangePassword()
        {
            ViewBag.Title = "Change password";
            return View(new ChangePasswordViewModel());
        }

        // POST: /Admin/Teacher/ChangePassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Title = "Change password";
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", "Teacher", new { Area = "Admin", Message = "Password changed successfully" });
            }
            ViewBag.Title = "Change password";
            AddErrors(result);
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}