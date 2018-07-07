using StudentTestingSystem.Areas.Admin.Models.Subject;
using StudentTestingSystem.Services.Abstract.Admin;
using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Subject.Request;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using StudentTestingSystem.Services.Abstract.SuperAdmin;
using StudentTestingSystem.Areas.Admin.Models;
using System.Collections.Generic;

namespace StudentTestingSystem.Areas.Admin.Controllers
{
    [Route("Admin/[controller]/[action]")]
    public class SubjectController : BaseController
    {
        private readonly ISubjectAdminService _subjectAdminService;
        private readonly IProfileSuperAdminService _profileSuperAdminService;
        
        public SubjectController(
            ISubjectAdminService subjectAdminService,
            IProfileSuperAdminService profileSuperAdminService)
        {
            _subjectAdminService = subjectAdminService;
            _profileSuperAdminService = profileSuperAdminService;
        }

        // GET: Admin/Subject/Index
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> Index(int page = 1, int pageSize = 10)
        {
            if (IsUserExistInRole("SuperAdmin"))
            {
                return RedirectToAction("Index", "Subject", new { Area = "SuperAdmin" });
            }

            var userId = GetCurrentUserId();
            var profile = await _profileSuperAdminService.GetProfileByUserIdAsync(userId);
            var subjects = await _subjectAdminService.GetAllSubjectsByUserProfileId(page, pageSize, profile.Id);

            List<Breadcrumb> breadcrumb = new List<Breadcrumb>() {
                new Breadcrumb("Home", "Index", "Home", new { Area = "" }),
                new Breadcrumb("Subjects")
            };

            SubjectIndexViewModel subjectIndexViewModel = new SubjectIndexViewModel
            {
                PageInfo = subjects,
                Subjects = subjects.Results,
                Breadcrumb = breadcrumb
            };

            ViewBag.Title = "Subject list";
            return View(subjectIndexViewModel);
        }

        // GET: Admin/Subject/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.Title = "Add a new subject";
            return View();
        }

        // POST: Admin/Subject/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateSubjectRequest model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = GetCurrentUserId();
                await _subjectAdminService.CreateSubjectAsync(model);
                return RedirectToAction("Index");
            }
            ViewBag.Title = "Add a new subject";
            return View(model);
        }

        // GET: Admin/Subject/Edit
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var subject = await _subjectAdminService.GetSubjectByIdAsync(id);
            EditSubjectRequest editSubjectRequest = subject.ToEditSubjectRequest();

            ViewBag.Title = $"Edit subject \"{subject.Title}\"";
            return View(editSubjectRequest);
        }

        // POST: Admin/Subject/Edit
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditSubjectRequest model)
        {
            if (ModelState.IsValid)
            {
                await _subjectAdminService.EditSubjectAsync(model);
                return RedirectToAction("Index");
            }
            ViewBag.Title = "Edit subject";
            return View(model);
        }

        // POST: Admin/Subject/Delete
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost]
        public async Task Delete(Guid id)
        {
            await _subjectAdminService.DeleteSubjectAsync(id);
        }

        // GET: Admin/Subject/Details
        public async Task<ActionResult> Details(Guid id)
        {
            var subject = await _subjectAdminService.GetSubjectByIdAsync(id);

            ViewBag.Title = $"Details of the subject \"{subject.Title}\"";
            return View(subject);
        }
    }
}