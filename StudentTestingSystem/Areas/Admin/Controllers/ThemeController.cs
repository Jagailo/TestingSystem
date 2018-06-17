using StudentTestingSystem.Areas.Admin.Models;
using StudentTestingSystem.Areas.Admin.Models.Theme;
using StudentTestingSystem.Services.Abstract.Admin;
using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Theme.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StudentTestingSystem.Areas.Admin.Controllers
{
    [Route("Admin/[controller]/[action]")]
    public class ThemeController : Controller
    {
        private readonly ISubjectAdminService _subjectAdminService;
        private readonly IThemeAdminService _themeAdminService;

        public ThemeController(
            IThemeAdminService themeAdminService,
            ISubjectAdminService subjectAdminService)
        {
            _themeAdminService = themeAdminService;
            _subjectAdminService = subjectAdminService;
        }

        // GET: Admin/Theme/Index
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> Index(Guid subjectId, int page = 1, int pageSize = 10)
        {
            var themes = await _themeAdminService.GetAllThemesBySubjectId(subjectId, page, pageSize);
            var subject = await _subjectAdminService.GetSubjectByIdAsync(subjectId);

            List<Breadcrumb> breadcrumb = new List<Breadcrumb>() {
                new Breadcrumb("Home", "Index", "Home", new { Area = "" }),
                new Breadcrumb("Subjects", "Index", "Subject", new { Area = "Admin" }),
                new Breadcrumb("Subject themes")
            };

            ThemeIndexViewModel themeIndexViewModel = new ThemeIndexViewModel
            {
                PageInfo = themes,
                SubjectId = subjectId,
                Themes = themes.Results,
                Breadcrumb = breadcrumb
            };

            ViewBag.Title = $"List of themes in subject \"{subject.Title}\"";
            return View(themeIndexViewModel);
        }

        // GET: Admin/Theme/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Guid subjectId)
        {
            CreateThemeRequest createThemeRequest = new CreateThemeRequest()
            {
                SubjectId = subjectId,
                TimeLine = 1
            };
            ViewBag.Title = "Add a new theme";
            return View(createThemeRequest);
        }

        // POST: Admin/Theme/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateThemeRequest model)
        {
            if (ModelState.IsValid)
            {
                await _themeAdminService.CreateThemeAsync(model);
                return RedirectToAction("Index", new { subjectId = model.SubjectId });
            }
            ViewBag.Title = "Add a new theme";
            return View(model);
        }

        // GET: Admin/Theme/Edit
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var theme = await _themeAdminService.GetThemeByIdAsync(id);
            EditThemeRequest editThemeRequest = theme.ToEditThemeRequest();
            ViewBag.Title = $"Edit theme \"{theme.Title}\"";
            return View(editThemeRequest);
        }

        // POST: Admin/Theme/Edit
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditThemeRequest model)
        {
            if (ModelState.IsValid)
            {
                await _themeAdminService.EditThemeAsync(model);
                return RedirectToAction("Index", new { subjectId = model.SubjectId });
            }
            ViewBag.Title = "Edit theme";
            return View(model);
        }

        // POST: Admin/Theme/Delete
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost]
        public async Task Delete(Guid id)
        {
            await _themeAdminService.DeleteThemeAsync(id);
        }
    }
}