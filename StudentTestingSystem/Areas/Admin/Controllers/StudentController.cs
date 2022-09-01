using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using StudentTestingSystem.Areas.Admin.Models;
using StudentTestingSystem.Areas.Admin.Models.Student;
using StudentTestingSystem.Extensions;
using StudentTestingSystem.Services.Abstract.Admin;
using StudentTestingSystem.Services.TransportModels.Admin.Student.Request;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentTestingSystem.Areas.Admin.Controllers
{
    //TODO: routing
    [Route("Admin/[controller]/[action]")]
    public class StudentController : BaseController
    {
        private ApplicationUserManager _userManager;
        private readonly IStudentAdminService _studentAdminService;
        private readonly IGroupAdminService _groupAdminService;

        public StudentController(
            IStudentAdminService studentAdminService,
            IGroupAdminService groupAdminService)
        {
            _studentAdminService = studentAdminService;
            _groupAdminService = groupAdminService;
        }

        public StudentController(
            ApplicationUserManager userManager,
            IGroupAdminService groupAdminService)
        {
            _userManager = userManager;
            _groupAdminService = groupAdminService;
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

        // GET: Admin/Student/Index
        [HttpGet]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> Index(Guid groupId, int page = 1, int pageSize = 10)
        {
            var students = await _studentAdminService.GetStudentsByGroupIdAsync(groupId, page, pageSize);
            var allStudents = await _studentAdminService.GetStudentsByGroupIdAsync(groupId);
            var group = await _groupAdminService.GetGroupByIdAsync(groupId);

            List<Breadcrumb> breadcrumb = new List<Breadcrumb>() {
                new Breadcrumb("Home", "Index", "Home", new { Area = "" }),
                new Breadcrumb("Groups", "Index", "Group", new { Area = "Admin" }),
                new Breadcrumb("Students of the group")
            };

            var viewModel = new IndexViewModel
            {
                GroupId = groupId,
                PageInfo = students,
                Students = students.Results,
                Breadcrumb = breadcrumb
            };

            ViewBag.Title = $"List of students in group \"{group.Number}\"";
            ViewBag.SecondTitle = $"{allStudents.Count()} students";
            return View(viewModel);
        }

        // GET: Admin/Student/UpdateStudentProfile
        [HttpGet]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> UpdateStudentProfile(Guid profileId)
        {
            var student = await _studentAdminService.GetStudentByIdAsync(profileId);
            var groups = await _groupAdminService.GetAllGroupsAsync();
            var viewModel = new UpdateStudentProfileViewModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Groups = groups.ToStudentSelectListItems(student.GroupId),
                GroupId = student.GroupId
            };
            ViewBag.Title = $"Edit student \"{student.FirstName} {student.LastName}\"";
            return View(viewModel);
        }

        // POST: Admin/Student/UpdateStudentProfile
        [HttpPost]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> UpdateStudentProfile(UpdateStudentProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _studentAdminService.UpdateProfileAsync(new UpdateStudentProfileRequest
                {
                    Id = model.Id,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    GroupId = model.GroupId
                });
                return RedirectToAction("Index", new { groupId = model.GroupId });
            }
            ViewBag.Title = "Edit student";
            return View(model);
        }

        // GET: Admin/Student/Details
        [HttpGet]
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> Details(Guid profileId)
        {
            var student = await _studentAdminService.GetStudentByIdAsync(profileId);
            var viewModel = new StudentProfileDetailsViewModel
            {
                Id = student.Id,
                Created = student.Created,
                Email = student.Email,
                FirstName = student.FirstName,
                LastName = student.LastName,
                FullName = student.FirstName + " " + student.LastName,
                Group = student.Group,
                UserId = student.UserId
            };
            return View(viewModel);
        }

        // GET: /Account/ResetPassword
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> ResetPassword(Guid profileId)
        {
            var profile = await _studentAdminService.GetStudentByIdAsync(profileId);            
            var model = new ResetPasswordViewModel
            {
                Id = profileId
            };
            ViewBag.Title = $"Reset password for {profile.FirstName} {profile.LastName}";
            return View(model);
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var profile = await _studentAdminService.GetStudentByIdAsync(model.Id);
            ViewBag.Title = $"Reset password for {profile.FirstName} {profile.LastName}";
            if (!ModelState.IsValid)
            {                
                return View(model);
            }
            var user = await UserManager.FindByEmailAsync(profile.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPassword", "Student");
            }
            var code = await UserManager.GeneratePasswordResetTokenAsync(profile.UserId);
            var result = await UserManager.ResetPasswordAsync(user.Id, code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Details", "Student", new { profileId = model.Id });
            }
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