using StudentTestingSystem.Areas.Admin.Models;
using StudentTestingSystem.Areas.Admin.Models.Group;
using StudentTestingSystem.Services.Abstract.Admin;
using StudentTestingSystem.Services.TransportModels.Admin.Group.Request;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StudentTestingSystem.Areas.Admin.Controllers
{
    [Route("Admin/[controller]/[action]")]
    public class GroupController : BaseController
    {
        private readonly IGroupAdminService _groupAdminService;

        public GroupController(IGroupAdminService groupAdminService)
        {
            _groupAdminService = groupAdminService;
        }

        // GET: Admin/Group/Index
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpGet]
        public async Task<ActionResult> Index(int page = 1, int pageSize = 10)
        {
            var groups = await _groupAdminService.GetAllGroupsAsync(page, pageSize);

            List<Breadcrumb> breadcrumb = new List<Breadcrumb>() {
                new Breadcrumb("Home", "Index", "Home", new { Area = "" }),
                new Breadcrumb("Groups")
            };

            var viewModel = new IndexViewModel
            {
                Groups = groups.Results,
                PageInfo = groups,
                Breadcrumb = breadcrumb
            };
            return View(viewModel);
        }

        // GET: Admin/Group/AddGroup
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpGet]
        public ActionResult AddGroup()
        {
            return View(new AddGroupViewModel());
        }

        // POST: Admin/Group/AddGroup
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost]
        public async Task<ActionResult> AddGroup(AddGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _groupAdminService.AddGroupAsync(new AddGroupRequest
                {
                    Number = model.Number,
                    Title = model.Title
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Admin/Group/UpdateGroup
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpGet]
        public async Task<ActionResult> UpdateGroup(Guid groupId)
        {
            var group = await _groupAdminService.GetGroupByIdAsync(groupId);
            var viewModel = new UpdateGroupViewModel
            {
                Id = groupId,
                Title = group.Title,
                Number = group.Number
            };
            ViewBag.Title = $"Edit group \"{group.Number}\"";
            return View(viewModel);
        }

        // POST: Admin/Group/UpdateGroup
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost]
        public async Task<ActionResult> UpdateGroup(UpdateGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _groupAdminService.UpdateGroupAsync(new UpdateGroupRequest
                {
                    Id = model.Id,
                    Number = model.Number,
                    Title = model.Title
                });
                return RedirectToAction("Index");
            }
            ViewBag.Title = "Edit group";
            return View(model);
        }

        // POST: Admin/Group/Delete
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost]
        public async Task<ActionResult> Delete(Guid groupId)
        {
            await _groupAdminService.DeleteGroupAsync(groupId);
            return RedirectToAction("Index");
        }
    }
}