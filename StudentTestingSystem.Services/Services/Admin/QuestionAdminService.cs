using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Domain.Models.Question;
using StudentTestingSystem.Services.Abstract.Admin;
using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Answer.Request;
using StudentTestingSystem.Services.TransportModels.Admin.Question.Request;
using StudentTestingSystem.Services.TransportModels.Admin.Question.Response;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using StudentTestingSystem.Storage;

namespace StudentTestingSystem.Services.Services.Admin
{
    public class QuestionAdminService : BaseService, IQuestionAdminService
    {
        public const string BaseContentFolder = "Uploads";
        private const string PhotoContainerName = "Photo";
        private readonly IAnswerAdminService _answerAdminService;
        private readonly IContentStorage _contentStorage;

        public QuestionAdminService(IUnitOfWork unitOfWork, IAnswerAdminService answerAdminService, IContentStorage contentStorage) : base(unitOfWork)
        {
            _answerAdminService = answerAdminService;
            _contentStorage = contentStorage;
        }

        public async Task<QuestionResponse> CreateQuestionAsync(CreateQuestionRequest model)
        {
            Question question = new Question()
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Explanation = model.Explanation,
                Created = DateTime.UtcNow
            };

            QuestionTheme questionTheme = new QuestionTheme()
            {
                QuestionId = question.Id,
                ThemeId = model.ThemeId,
                Created = DateTime.UtcNow
            };

            UnitOfWork.QuestionRepository.Add(question);
            UnitOfWork.QuestionThemeRepository.Add(questionTheme);
            await UnitOfWork.SaveChangesAsync();

            foreach (var answer in model.Answers)
            {
                answer.QuestionId = question.Id;
                await _answerAdminService.AddAnswerAsync(answer);
            }

            var newQuestion = await GetQuestionByIdAsync(question.Id);
            return newQuestion;
        }

        public async Task<string> UploadAttachmentAsync(UploadQuestionAttachmentRequest model)
        {
            var question = UnitOfWork.QuestionRepository.GetById(model.QuestionId);
            try
            {
                await DeleteAttachmentAsync(model.QuestionId);
            }
            catch (ContentStorageException ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            var fileName = Guid.NewGuid() + "." + model.Extention;
            
            await _contentStorage.SaveContentAsync(fileName, model.Image, PhotoContainerName);
            question.Image = fileName;
            UnitOfWork.QuestionRepository.Update(question);
            await UnitOfWork.SaveChangesAsync();
            return fileName;
        }

        public async Task DeleteAttachmentAsync(Guid questionId)
        {
            var question = UnitOfWork.QuestionRepository.GetById(questionId);
            if (string.IsNullOrWhiteSpace(question.Image))
                return;
            await _contentStorage.DeleteContentAsync(question.Image, PhotoContainerName);

            question.Image = null;
            UnitOfWork.QuestionRepository.Update(question);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteQuestionAsync(Guid questionId)
        {
            var question = await UnitOfWork.QuestionRepository.Query(x => x.Id == questionId)
                .FirstOrDefaultAsync();

            if (question == null)
            {
                throw new ArgumentNullException();
            }

            await _answerAdminService.DeleteAnswersByQuestionIdAsync(questionId);

            UnitOfWork.QuestionRepository.Delete(question);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteQuestionsByThemeIdAsync(Guid themeId)
        {
            var questionIds = await UnitOfWork.QuestionThemeRepository.Query(x => x.ThemeId == themeId)
                .Select(x => x.QuestionId)
                .ToListAsync();

            foreach (var questionId in questionIds)
            {
                await DeleteQuestionAsync(questionId);
            }
        }

        public async Task EditQuestionAsync(EditQuestionRequest model)
        {
            var question = await UnitOfWork.QuestionRepository.Query(x => x.Id == model.Id)
                .FirstOrDefaultAsync();

            if (question == null)
            {
                throw new ArgumentNullException();
            }

            question.Title = model.Title;
            question.Explanation = model.Explanation;

            UnitOfWork.QuestionRepository.Update(question);
            await UnitOfWork.SaveChangesAsync();

            var answers = await _answerAdminService.GetAllAnswersByQuestionIdAsync(question.Id);
            foreach (var answer in model.Answers)
            {
                if (answer.Id == Guid.Empty) // Add new answers
                {
                    AddAnswerRequest addAnswerRequest = new AddAnswerRequest()
                    {
                        Title = answer.Title,
                        IsCorrectAnswer = answer.IsCorrectAnswer,
                        QuestionId = question.Id
                    };
                    await _answerAdminService.AddAnswerAsync(addAnswerRequest);
                }
                else // Update answers
                {
                    answers.RemoveAll(x => x.Id == answer.Id);
                    await _answerAdminService.UpdateAnswerAsync(answer);
                }
            }
            foreach (var answer in answers) // Delete answers
            {
                await _answerAdminService.DeleteAnswerAsync(answer.Id);
            }
        }

        public async Task<PageInfo<QuestionResponse>> GetAllQuestionsByThemeIdAsync(int page, int pageSize, Guid themeId)
        {
            // You can't make this request async and IQueryable at the same time
            // http://go.microsoft.com/fwlink/?LinkId=287068
            var questions = UnitOfWork.QuestionThemeRepository.Query(x => x.ThemeId == themeId)
                .Select(x => x.Question)
                .OrderByDescending(x => x.Created);

            return await PagedHelper.CreatePagedResultsAsync(questions, page, pageSize, x => x.ToQuestionResponse());
        }

        public async Task<IEnumerable<QuestionResponse>> GetAllQuestionsByThemeIdAsync(Guid themeId)
        {
            var questions = await UnitOfWork.QuestionThemeRepository.Query(x => x.ThemeId == themeId)
                .Select(x => x.Question)                
                .ToListAsync();

            return questions.Select(x => x.ToQuestionResponse());
        }

        public async Task<QuestionResponse> GetQuestionByIdAsync(Guid id)
        {
            var question = await UnitOfWork.QuestionRepository.Query(x => x.Id == id)
                .Include(x => x.QuestionThemes.Select(y => y.Theme))
                .FirstOrDefaultAsync();

            if (question == null)
            {
                throw new ArgumentNullException();
            }

            return question.ToQuestionResponse();
        }

        public async Task<Guid> GetThemeIdByQuestionIdAsync(Guid questionId)
        {
            var theme = await UnitOfWork.QuestionThemeRepository.Query(x => x.QuestionId == questionId)
                .Select(x => x.Theme)
                .FirstOrDefaultAsync();

            if (theme == null)
            {
                throw new ArgumentNullException();
            }

            return theme.Id;
        }
    }
}