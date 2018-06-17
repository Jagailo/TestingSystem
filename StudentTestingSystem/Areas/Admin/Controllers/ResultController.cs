using StudentTestingSystem.Areas.Admin.Models;
using StudentTestingSystem.Areas.Admin.Models.Result;
using StudentTestingSystem.Services.Abstract.Admin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StudentTestingSystem.Areas.Admin.Controllers
{
    [Route("Admin/[controller]/[action]")]
    public class ResultController : BaseController
    {
        private readonly IResultAdminService _resultAdminService;
        private readonly IStudentAdminService _studentAdminService;
        private readonly IGroupAdminService _groupAdminService;
        private readonly IThemeAdminService _themeAdminService;

        public ResultController(
            IResultAdminService resultAdminService,
            IStudentAdminService studentAdminService,
            IGroupAdminService groupAdminService,
            IThemeAdminService themeAdminService)
        {
            _resultAdminService = resultAdminService;
            _studentAdminService = studentAdminService;
            _groupAdminService = groupAdminService;
            _themeAdminService = themeAdminService;
        }

        // GET: Admin/Result/Index
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> ProfileResults(Guid profileId, int page = 1, int pageSize = 10)
        {
            var profile = await _studentAdminService.GetStudentByIdAsync(profileId);
            var results = await _resultAdminService.GetResultsByProfileIdAsync(profileId, page, pageSize);

            List<Breadcrumb> breadcrumb = new List<Breadcrumb>() {
                new Breadcrumb("Home", "Index", "Home", new { Area = "" }),
                new Breadcrumb("Groups", "Index", "Group", new { Area = "Admin" }),
                new Breadcrumb("Students", "Index", "Student", new { Area = "Admin", groupId = profile.GroupId }),
                new Breadcrumb("Student results")
            };

            var viewModel = new ProfileResultsViewModel
            {
                Id = profileId,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Results = results.Results,
                PageInfo = results,
                Breadcrumb = breadcrumb
            };
            return View(viewModel);
        }

        // GET: Admin/Result/ThemeAllResults
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> ThemeAllResults(Guid themeId, int page = 1, int pageSize = 10)
        {
            var results = await _resultAdminService.GetSubjectThemeResultsAsync(themeId, page, pageSize);
            var theme = await _themeAdminService.GetThemeByIdAsync(themeId);

            List<Breadcrumb> breadcrumb = new List<Breadcrumb>() {
                new Breadcrumb("Home", "Index", "Home", new { Area = "" }),
                new Breadcrumb("Subjects", "Index", "Subject", new { Area = "Admin" }),
                new Breadcrumb("Subject themes", "Index", "Theme", new { Area = "Admin", subjectId = theme.SubjectId }),
                new Breadcrumb("Results of the theme")
            };

            var viewModel = new SubjectThemeResultViewModel
            {
                ThemeId = themeId,
                PageInfo = results,
                Results = results.Results,
                Breadcrumb = breadcrumb
            };
            ViewBag.Title = $"Results of the theme \"{theme.Title}\"";
            ViewBag.SecondTitle = "All groups";
            return View(viewModel);
        }

        // GET: Admin/Result/GroupResults
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> GroupResults(Guid groupId, int page = 1, int pageSize = 10)
        {
            var results = await _resultAdminService.GetResultsByGroupIdAsync(groupId, page, pageSize);
            var group = await _groupAdminService.GetGroupByIdAsync(groupId);

            List<Breadcrumb> breadcrumb = new List<Breadcrumb>() {
                new Breadcrumb("Home", "Index", "Home", new { Area = "" }),
                new Breadcrumb("Groups", "Index", "Group", new { Area = "Admin" }),
                new Breadcrumb("Group results")
            };

            var viewModel = new GroupResultsViewModel
            {
                GroupId = groupId,
                PageInfo = results,
                Results = results.Results,
                Breadcrumb = breadcrumb
            };

            ViewBag.Title = "Results of the group";
            ViewBag.SecondTitle = $"Group {group.Number}";
            return View(viewModel);
        }

        // GET: Admin/Result/ThemeResults
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> ThemeResults(Guid groupId, Guid themeId, int page = 1, int pageSize = 10)
        {
            var group = await _groupAdminService.GetGroupByIdAsync(groupId);
            var results = await _resultAdminService.GetResultsByGroupIdThemeIdAsync(groupId, themeId, page, pageSize);
            var theme = await _themeAdminService.GetThemeByIdAsync(themeId);

            List<Breadcrumb> breadcrumb = new List<Breadcrumb>() {
                new Breadcrumb("Home", "Index", "Home", new { Area = "" }),
                new Breadcrumb("Groups", "Index", "Group", new { Area = "Admin" }),
                new Breadcrumb("Group results", "GroupResults", "Result", new { Area = "Admin", groupId }),
                new Breadcrumb("Results of the group on the theme")
            };

            var viewModel = new ThemeResultsViewModel
            {
                Group = group.Number,
                GroupId = groupId,
                ThemeId = themeId,
                PageInfo = results,
                Results = results.Results,
                Breadcrumb = breadcrumb
            };

            ViewBag.Title = $"Results of the group {group.Number}";
            ViewBag.SecondTitle = $"Theme \"{theme.Title}\"";
            return View(viewModel);
        }
    }
}