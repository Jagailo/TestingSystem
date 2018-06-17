using StudentTestingSystem.Areas.Admin.Controllers;
using StudentTestingSystem.Areas.Admin.Models;
using StudentTestingSystem.Areas.Admin.Models.Subject;
using StudentTestingSystem.Services.Abstract.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StudentTestingSystem.Areas.Student.Controllers
{
    [Route("Student/[controller]/[action]")]
    public class SubjectController : BaseController
    {
        private readonly ISubjectAdminService _subjectAdminService;

        public SubjectController(ISubjectAdminService subjectAdminService)
        {
            _subjectAdminService = subjectAdminService;
        }

        // GET: Student/Subject
        [Authorize(Roles = "User")]
        public async Task<ActionResult> Index(int page = 1, int pageSize = 10)
        {
            var subjects = await _subjectAdminService.GetAllSubjects(page, pageSize);

            var breadcrumb = new List<Breadcrumb>
            {
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
    }
}