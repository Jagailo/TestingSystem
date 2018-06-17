using StudentTestingSystem.Areas.Admin.Models;
using StudentTestingSystem.Areas.SuperAdmin.Models.Group;
using StudentTestingSystem.Services.Abstract.Admin;
using StudentTestingSystem.Services.Abstract.SuperAdmin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StudentTestingSystem.Areas.SuperAdmin.Controllers
{
    [Route("SuperAdmin/[controller]/[action]")]
    public class GroupController : Controller
    {
        private readonly IGroupSuperAdminService _groupSuperAdminService;
        private readonly IResultAdminService _resultAdminService;

        public GroupController(
            IGroupSuperAdminService groupSuperAdminService,
            IResultAdminService resultAdminService)
        {
            _groupSuperAdminService = groupSuperAdminService;
            _resultAdminService = resultAdminService;
        }

        // GET: SuperAdmin/Group
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<ActionResult> Index(int page = 1, int pageSize = 10)
        {
            var groups = await _groupSuperAdminService.GetAllGroupsAsync(page, pageSize);

            List<Breadcrumb> breadcrumb = new List<Breadcrumb>() {
                new Breadcrumb("Home", "Index", "Home", new { Area = "" }),
                new Breadcrumb("Groups")
            };

            var model = new IndexViewModel
            {
                PageInfo = groups,
                Groups = groups.Results,
                Breadcrumb = breadcrumb
            };

            ViewBag.Title = "List of groups";
            return View(model);
        }

        // POST: SuperAdmin/Group/Delete
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task Delete(Guid groupId)
        {
            await _groupSuperAdminService.DeleteGroupAsync(groupId);
        }

        // GET: SuperAdmin/Group/Results
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<ActionResult> Results(Guid groupId, int page = 1, int pageSize = 10)
        {
            var results = await _resultAdminService.GetResultsByGroupIdAsync(groupId, page, pageSize);
            var group = await _groupSuperAdminService.GetGroupByIdAsync(groupId);

            List<Breadcrumb> breadcrumb = new List<Breadcrumb>() {
                new Breadcrumb("Home", "Index", "Home", new { Area = "" }),
                new Breadcrumb("Groups", "Index", "Group", new { Area = "SuperAdmin" }),
                new Breadcrumb("Results of the group")
            };

            var model = new Models.Results.IndexViewModel
            {
                GroupId = groupId,
                PageInfo = results,
                Results = results.Results,
                Breadcrumb = breadcrumb
            };

            ViewBag.Title = $"Results of group {group.Number}";
            return View(model);
        }

        // POST: SuperAdmin/Group/DeleteResultsAsync
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task DeleteResultsAsync(Guid groupId)
        {
            await _groupSuperAdminService.DeleteAllGroupResultsAsync(groupId);
        }
    }
}