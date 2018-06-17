using StudentTestingSystem.Areas.Admin.Models;
using StudentTestingSystem.Areas.Admin.Models.Question;
using StudentTestingSystem.Services.Abstract.Admin;
using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Answer.Request;
using StudentTestingSystem.Services.TransportModels.Admin.Question.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using StudentTestingSystem.Extensions;

namespace StudentTestingSystem.Areas.Admin.Controllers
{
    [Route("Admin/[controller]/[action]")]
    public class QuestionController : Controller
    {
        private readonly IThemeAdminService _themeAdminService;
        private readonly IQuestionAdminService _questionAdminService;
        private readonly int _maxCountAnswers = 4; // For creating and editing (min 2)

        public QuestionController(
            IQuestionAdminService questionAdminService,
            IThemeAdminService themeAdminService)
        {
            _themeAdminService = themeAdminService;
            _questionAdminService = questionAdminService;
        }

        // GET: Admin/Question/Index
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> Index(Guid themeId, int page = 1, int pageSize = 10)
        {
            var questions = await _questionAdminService.GetAllQuestionsByThemeIdAsync(page, pageSize, themeId);
            var theme = await _themeAdminService.GetThemeByIdAsync(themeId);

            List<Breadcrumb> breadcrumb = new List<Breadcrumb>() {
                new Breadcrumb("Home", "Index", "Home", new { Area = "" }),
                new Breadcrumb("Subjects", "Index", "Subject", new { Area = "Admin" }),
                new Breadcrumb("Subject themes", "Index", "Theme", new { Area = "Admin", subjectId = theme.SubjectId }),
                new Breadcrumb("Questions of the theme")
            };

            QuestionIndexViewModel questionIndexViewModel = new QuestionIndexViewModel
            {
                PageInfo = questions,
                ThemeId = themeId,
                Questions = questions.Results,
                Breadcrumb = breadcrumb
            };

            ViewBag.Title = $"List of questions to the theme \"{theme.Title}\" of the subject \"{theme.SubjectTitle}\"";
            ViewBag.HeadFirstTitle = $"List of questions to the theme \"{theme.Title}\"";
            ViewBag.HeadSecondTitle = $"Subject \"{theme.SubjectTitle}\"";
            return View(questionIndexViewModel);
        }

        // GET: Admin/Question/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Guid themeId)
        {
            QuestionCreateViewModel questionCreateViewModel = new QuestionCreateViewModel
            {
                ThemeId = themeId,
                MaxCountAnswers = _maxCountAnswers
            };

            ViewBag.Title = "Add a new question";
            return View(questionCreateViewModel);
        }

        // POST: Admin/Question/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(QuestionCreateViewModel model, FormCollection formCollection)
        {
            List<AddAnswerRequest> answers = new List<AddAnswerRequest>();
            for (int i = 4; i < formCollection.Count; i += 2)
            {
                AddAnswerRequest addAnswerRequest = new AddAnswerRequest()
                {
                    Title = formCollection[i],
                    IsCorrectAnswer = Convert.ToBoolean(formCollection[i + 1])
                };
                answers.Add(addAnswerRequest);
            }

            CreateQuestionRequest createQuestionRequest = new CreateQuestionRequest()
            {
                Title = model.Title,
                Explanation = model.Explanation,
                ThemeId = model.ThemeId,
                Answers = answers
            };

            var question = await _questionAdminService.CreateQuestionAsync(createQuestionRequest);
            
            if (model.Image != null && model.Image.ContentLength > 0)
            {
                var extension = AttachmentsUtil.GetExtentionFromMimeType(model.Image.ContentType);
                var content = GetAttachmentContent(model.Image);
                await _questionAdminService.UploadAttachmentAsync(new UploadQuestionAttachmentRequest(question.Id, content, extension));
            }

            return RedirectToAction("Index", new { themeId = model.ThemeId });
        }

        // GET: Admin/Question/Edit
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var question = await _questionAdminService.GetQuestionByIdAsync(id);
            EditQuestionRequest editQuestionRequest = question.ToEditQuestionRequest();
            editQuestionRequest.Answers.Shuffle();
            QuestionEditViewModel questionEditViewModel = new QuestionEditViewModel()
            {
                Id = editQuestionRequest.Id,
                Title = editQuestionRequest.Title,
                ImageUrl = editQuestionRequest.ImageUrl,
                ThemeId = editQuestionRequest.ThemeId,
                DeleteImage = false,
                Explanation = editQuestionRequest.Explanation,
                Answers = editQuestionRequest.Answers,
                MaxCountAnswers = _maxCountAnswers
            };

            ViewBag.Title = $"Edit question \"{question.Title}\"";
            return View(questionEditViewModel);
        }

        // POST: Admin/Question/Edit
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(QuestionEditViewModel model, FormCollection formCollection)
        {
            if (model.File != null && model.File.ContentLength > 0) // Replace image
            {
                if (model.ImageUrl != null)
                {
                    await _questionAdminService.DeleteAttachmentAsync(model.Id);
                    model.ImageUrl = null;
                }
                var extension = AttachmentsUtil.GetExtentionFromMimeType(model.File.ContentType);
                var content = GetAttachmentContent(model.File);
                model.ImageUrl = await _questionAdminService.UploadAttachmentAsync(new UploadQuestionAttachmentRequest(model.Id, content, extension));
            }
            else if (model.DeleteImage && model.ImageUrl != null) // Delete image
            {
                await _questionAdminService.DeleteAttachmentAsync(model.Id);
                model.ImageUrl = null;
            }

            List<UpdateAnswerRequest> answers = new List<UpdateAnswerRequest>();
            for (int i = 6; i < formCollection.Count; i += 3)
            {
                UpdateAnswerRequest updateAnswerRequest = new UpdateAnswerRequest()
                {
                    Id = Guid.Parse(formCollection[i]),
                    IsCorrectAnswer = Convert.ToBoolean(formCollection[i + 1]),
                    Title = formCollection[i + 2]
                };
                answers.Add(updateAnswerRequest);
            }

            EditQuestionRequest editQuestionRequest = new EditQuestionRequest()
            {
                Id = model.Id,
                Title = model.Title,
                ImageUrl = model.ImageUrl,
                Explanation = model.Explanation,
                Answers = answers
            };

            await _questionAdminService.EditQuestionAsync(editQuestionRequest);
            Guid themeId = await _questionAdminService.GetThemeIdByQuestionIdAsync(model.Id);
            return RedirectToAction("Index", new { themeId });
        }

        // POST: Admin/Question/Delete
        [Authorize(Roles = "SuperAdmin, Admin")]
        [HttpPost]
        public async Task Delete(Guid id)
        {
            var question = await _questionAdminService.GetQuestionByIdAsync(id);
            if (question.ImageUrl != null)
            {
                await _questionAdminService.DeleteAttachmentAsync(question.Id);
            }
            await _questionAdminService.DeleteQuestionAsync(id);
        }

        // GET: Admin/Question/Details
        [Authorize(Roles = "SuperAdmin, Admin")]
        public async Task<ActionResult> Details(Guid id)
        {
            var question = await _questionAdminService.GetQuestionByIdAsync(id);
            question.Answers.Shuffle();

            ViewBag.Title = $"Details of the question \"{question.Title}\"";
            return View(question);
        }

        private byte[] GetAttachmentContent(HttpPostedFileBase attachment)
        {
            byte[] content;
            using (var inputStream = attachment.InputStream)
            {
                if (!(inputStream is MemoryStream memoryStream))
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                content = memoryStream.ToArray();
            }
            return content;
        }
    }
}