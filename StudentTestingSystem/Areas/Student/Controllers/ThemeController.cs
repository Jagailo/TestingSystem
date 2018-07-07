using StudentTestingSystem.Areas.Admin.Controllers;
using StudentTestingSystem.Areas.Admin.Models;
using StudentTestingSystem.Areas.Admin.Models.Theme;
using StudentTestingSystem.Areas.Student.Models.Question;
using StudentTestingSystem.Services.Abstract.Admin;
using StudentTestingSystem.Services.Abstract.Student;
using StudentTestingSystem.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StudentTestingSystem.Areas.Student.Controllers
{
    [Route("Student/[controller]/[action]")]
    public class ThemeController : BaseController
    {
        private readonly IThemeAdminService _themeAdminService;
        private readonly ISubjectAdminService _subjectAdminService;
        private readonly IQuestionAdminService _questionAdminService;

        public ThemeController(
            IThemeAdminService themeAdminService, 
            ISubjectAdminService subjectAdminService, 
            IQuestionAdminService questionAdminService)
        {
            _themeAdminService = themeAdminService;
            _subjectAdminService = subjectAdminService;
            _questionAdminService = questionAdminService;
        }

        // GET: Student/Theme
        [Authorize(Roles = "User")]
        public async Task<ActionResult> Index(Guid subjectId, int page = 1, int pageSize = 10)
        {
            var themes = await _themeAdminService.GetAllThemesBySubjectId(subjectId, page, pageSize);
            var subject = await _subjectAdminService.GetSubjectByIdAsync(subjectId);

            var breadcrumb = new List<Breadcrumb>
            {
                new Breadcrumb("Home", "Index", "Home", new { Area = "" }),
                new Breadcrumb("Subjects", "Index", "Subject", new { Area = "Student" }),
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

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public async Task<ActionResult> Solve(Guid themeId, int questionNumber = 1)
        {
            var theme = await _themeAdminService.GetThemeByIdAsync(themeId);
            if (theme.QuestionsCount == 0)
            {
                Session["Token"] = null;
                Session["TokenExpire"] = null;
                Session["Answers"] = null;
                return RedirectToAction("Index", "Theme", new { subjectId = theme.SubjectId });
            }

            var sessionToken = Session["Token"];
            var sessionTokenExpire = (DateTime) Session["TokenExpiration"];
            if (sessionToken == null)
            {
                TempData["invalidToken"] = "You have an invalid token. Please try to run test again.";
                return RedirectToAction("AutoResult", "Result", new { themeId = theme.Id });
            }

            if (sessionTokenExpire < DateTime.UtcNow)
            {
                return RedirectToAction("AutoResult", "Result", new { themeId = theme.Id });
            }

            var sessionModel = (SessionModel) Session["Answers"];

            var question = (await _questionAdminService.GetAllQuestionsByThemeIdAsync(themeId))
                .Skip(questionNumber - 1)
                .Take(1)
                .Select(x => new QuestionViewModel
                {
                    TotalQuestionsCount = theme.QuestionsCount,
                    QuestionNumber = questionNumber,
                    QuestionId = x.Id,
                    Image = x.ImageUrl,
                    Title = x.Title,
                    ThemeId = x.ThemeId,
                    ThemeTitle = x.ThemeTitle,
                    Options = x.Answers.Select(y => new ChoiceModel
                    {
                        AnswerId = y.Id,
                        Title = y.Title,
                        IsCorrectAnswer = sessionModel.Answers.Any(z => z.SelectedAnswerId == y.Id)
                    })
                    .ToList()
                })
                .FirstOrDefault();

            question.Options.Shuffle();

            return View(question);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public async Task<ActionResult> Solve(AnswerViewModel model)
        {
            var theme = await _themeAdminService.GetThemeByIdAsync(model.ThemeId);

            var sessionToken = Session["Token"];
            var sessionTokenExpire = (DateTime)Session["TokenExpiration"];
            if (sessionToken == null)
            {
                TempData["invalidToken"] = "You have an invalid token. Please try to run test again.";
                return RedirectToAction("AutoResult", "Result", new { themeId = theme.Id });
            }

            if (sessionTokenExpire < DateTime.UtcNow)
            {
                return RedirectToAction("AutoResult", "Result", new { themeId = theme.Id });
            }

            var sessionModel = (SessionModel) Session["Answers"];

            var currentRecord = sessionModel.Answers.FirstOrDefault(x => x.QuestionId == model.QuestionId);

            if (currentRecord == null)
            {
                sessionModel.Answers.Add(new AnswerViewModel
                {
                    QuestionId = model.QuestionId,
                    SelectedAnswerId = model.SelectedAnswerId,
                    QuestionNumber = model.QuestionNumber,
                    ThemeId = model.ThemeId
                });
            }
            else
            {
                sessionModel.Answers.Remove(currentRecord);
                sessionModel.Answers.Add(new AnswerViewModel
                {
                    QuestionId = model.QuestionId,
                    SelectedAnswerId = model.SelectedAnswerId,
                    QuestionNumber = model.QuestionNumber,
                    ThemeId = model.ThemeId
                });
            }

            if (model.QuestionNumber == theme.QuestionsCount)
            {
                model.QuestionNumber = 0;
            }
            
            return RedirectToAction("Solve", new { themeId = model.ThemeId, questionNumber = model.QuestionNumber + 1 });
        }
    }
}