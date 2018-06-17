using StudentTestingSystem.Areas.Admin.Controllers;
using StudentTestingSystem.Areas.Admin.Models;
using StudentTestingSystem.Areas.Student.Models.Question;
using StudentTestingSystem.Areas.Student.Models.Result;
using StudentTestingSystem.Services.Abstract.Admin;
using StudentTestingSystem.Services.Abstract.Student;
using StudentTestingSystem.Services.TransportModels.Admin.Result.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StudentTestingSystem.Areas.Student.Controllers
{
    [Route("Student/[controller]/[action]")]
    public class ResultController : BaseController
    {
        private readonly IResultAdminService _resultAdminService;
        private readonly IProfileService _profileService;
        private readonly IThemeAdminService _themeAdminService;

        public ResultController(
            IResultAdminService resultAdminService,
            IProfileService profileService,
            IThemeAdminService themeAdminService)
        {
            _resultAdminService = resultAdminService;
            _profileService = profileService;
            _themeAdminService = themeAdminService;
        }

        // GET: Student/Result
        [Authorize(Roles = "User")]
        public async Task<ActionResult> Index(int page = 1, int pageSize = 10)
        {
            var userId = GetCurrentUserId();
            var profile = await _profileService.GetProfileByUserIdAsync(userId);
            var results = await _resultAdminService.GetResultsByProfileIdAsync(profile.Id, page, pageSize);

            List<Breadcrumb> breadcrumb = new List<Breadcrumb>
            {
                new Breadcrumb("Home", "Index", "Home", new { Area = "" }),
                new Breadcrumb("Results")
            };

            var model = new ResultsIndexViewModel
            {
                PageInfo = results,
                Results = results.Results,
                Breadcrumb = breadcrumb
            };

            return View(model);
        }

        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult> CreateSession(Guid subjectId, Guid themeId)
        {
            var userId = GetCurrentUserId();
            var theme = await _themeAdminService.GetThemeByIdAsync(themeId);

            var token = Session["Token"];
            var tokenExpiration = Session["TokenExpiration"];

            if (token == null || tokenExpiration == null)
            {
                Session["Token"] = userId;
                Session["TokenExpiration"] = DateTime.UtcNow.AddMinutes(theme.TimeLine);
                Session["Answers"] = new SessionModel
                {
                    Answers = new List<AnswerViewModel>()
                };
            }

            return RedirectToAction("Solve", "Theme", new { subjectId, themeId });
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public async Task<ActionResult> Result(CreateResultRequest request)
        {
            var sessionModel = (SessionModel) Session["Answers"];
            request.AnswerIds = sessionModel.Answers.Select(x => x.SelectedAnswerId).ToList();

            // Clear session
            Session["Token"] = null;
            Session["TokenExpire"] = null;
            Session["Answers"] = null;

            var userId = GetCurrentUserId();
            var isAdmin = IsUserExistInRole("Admin");
            var profile = await _profileService.GetProfileByUserIdAsync(userId);
            request.UserProfileId = profile.Id;
            request.IsAdmin = isAdmin;

            var theme = await _themeAdminService.GetThemeByIdAsync(request.ThemeId);

            var result = await _resultAdminService.CreateResultAsync(request);

            var model = new ResultIndexViewModel
            {
                Id = result.Id,
                AllQuestionsCount = result.AllQuestionsCount,
                CorrectAnswersCount = result.CorrectAnswersCount,
                Created = result.Created,
                GroupId = profile.GroupId,
                Number = profile.Group?.Number,
                ThemeId = result.ThemeId,
                ThemeTitle = theme.Title,
                Mark = Convert.ToInt32(Math.Round((double) result.CorrectAnswersCount / result.AllQuestionsCount * 10, MidpointRounding.AwayFromZero))
            };

            TempData["directions"] = result.Directions;

            return RedirectToAction("Result", model);
        }

        [Authorize(Roles = "Admin, User")]
        public async Task<JsonResult> AutoResult(CreateResultRequest request)
        {
            var sessionModel = (SessionModel)Session["Answers"];
            var sessionTokenExpire = (DateTime) Session["TokenExpiration"];
            request.AnswerIds = sessionModel.Answers.Select(x => x.SelectedAnswerId).ToList();

            TempData["testTimeLineExpired"] = "The time for passing the test is over.";

            // Clear session
            Session["Token"] = null;
            Session["TokenExpire"] = null;
            Session["Answers"] = null;

            var userId = GetCurrentUserId();
            var isAdmin = IsUserExistInRole("Admin");
            var profile = await _profileService.GetProfileByUserIdAsync(userId);
            request.UserProfileId = profile.Id;
            request.IsAdmin = isAdmin;

            var theme = await _themeAdminService.GetThemeByIdAsync(request.ThemeId);

            var result = await _resultAdminService.CreateResultAsync(request);

            var model = new ResultIndexViewModel
            {
                Id = result.Id,
                AllQuestionsCount = result.AllQuestionsCount,
                CorrectAnswersCount = result.CorrectAnswersCount,
                Created = result.Created,
                GroupId = profile.GroupId,
                Number = profile.Group?.Number,
                ThemeId = result.ThemeId,
                ThemeTitle = theme.Title,
                Mark = Convert.ToInt32(Math.Round((double)result.CorrectAnswersCount / result.AllQuestionsCount * 10, MidpointRounding.AwayFromZero))
            };

            TempData["directions"] = result.Directions;

            return Json(Url.Action("Result", "Result", new
            {
                id = model.Id,
                allQuestionsCount = model.AllQuestionsCount,
                correctAnswersCount = model.CorrectAnswersCount,
                created = model.Created,
                groupId = model.GroupId,
                number = model.Number,
                themeId = model.ThemeId,
                themeTitle = model.ThemeTitle,
                mark = model.Mark
            }));
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public ActionResult Result(ResultIndexViewModel model)
        {
            var list = TempData["directions"] as List<string>;
            model.Directions = list.Where(x => !string.IsNullOrEmpty(x)).ToList();
            return View(model);
        }
    }
}