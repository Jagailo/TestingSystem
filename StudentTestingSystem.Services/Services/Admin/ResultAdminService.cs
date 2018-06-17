using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Security;
using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Domain.Models.Question;
using StudentTestingSystem.Services.Abstract.Admin;
using StudentTestingSystem.Services.Abstract.Student;
using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Result.Request;
using StudentTestingSystem.Services.TransportModels.Admin.Result.Response;

namespace StudentTestingSystem.Services.Services.Admin
{
    public class ResultAdminService : BaseService, IResultAdminService
    {
        private readonly IQuestionAdminService _questionAdminService;

        public ResultAdminService(
            IUnitOfWork unitOfWork, 
            IQuestionAdminService questionAdminService) : base(unitOfWork)
        {
            _questionAdminService = questionAdminService;
        }

        public async Task<PageInfo<ProfileResultsResponse>> GetResultsByProfileIdAsync(Guid profileId, int page, int pageSize)
        {
            var results = UnitOfWork.ResultRepository.Query(x => x.UserProfileId == profileId)            
                .OrderByDescending(x => x.Created);

            return await PagedHelper.CreatePagedResultsAsync(results, page, pageSize, x => x.ToProfileResultsResponse());
        }

        public async Task<PageInfo<SubjectThemeResultsResponse>> GetSubjectThemeResultsAsync(Guid themeId, int page, int pageSize)
        {
            var results = UnitOfWork.ResultRepository.Query(x => x.ThemeId == themeId)
                .OrderBy(x => x.Student.SearchFullName);

            return await PagedHelper.CreatePagedResultsAsync(results, page, pageSize, x => x.ToSubjectThemeResultsResponse());
        }

        public async Task<PageInfo<GroupResultsResponse>> GetResultsByGroupIdAsync(Guid groupId, int page, int pageSize)
        {
            var results = (await UnitOfWork.ResultRepository.Query(x => x.Student.GroupId == groupId)
                .ToListAsync())
                .DistinctBy(x => x.Theme.Title)
                .OrderBy(x => x.Theme.Title);

            return PagedHelper.CreatePagedResults(results, page, pageSize, x => x.ToGroupResultsResponse());
        }

        public async Task<PageInfo<ThemeResultsResponse>> GetResultsByGroupIdThemeIdAsync(Guid groupId, Guid themeId, int page, int pageSize)
        {
            var results = UnitOfWork.ResultRepository.Query(x => x.Student.GroupId == groupId && x.ThemeId == themeId)
                .OrderBy(x => x.Student.SearchFullName);

            return await PagedHelper.CreatePagedResultsAsync(results, page, pageSize, x => x.ToThemeResultsResponse());
        }

        public async Task DeleteResultsByThemeIdAsync(Guid themeId)
        {
            var results = await UnitOfWork.ResultRepository.Query(x => x.ThemeId == themeId)
                .ToListAsync();

            foreach (var result in results)
            {
                UnitOfWork.ResultRepository.Delete(result);
                await UnitOfWork.SaveChangesAsync();
            }
        }

        public async Task<ResultResponse> CreateResultAsync(CreateResultRequest model)
        {
            var correctAnswerIds = await UnitOfWork.QuestionAnswerRepository
                .Query(x => x.Question.QuestionThemes.Any(y => y.ThemeId == model.ThemeId) &&
                 x.Answer.IsCorrectAnswer)
                .Select(x => x.AnswerId)
                .ToListAsync();

            var questions = await _questionAdminService.GetAllQuestionsByThemeIdAsync(model.ThemeId);
            var userCorrectAnswerIds = model.AnswerIds.Where(x => correctAnswerIds.Any(y => y == x)).ToList();

            // Prepare directions
            var directions = new List<string>();

            foreach (var question in questions)
            {
                if (question.Answers.Any(x => x.IsCorrectAnswer && userCorrectAnswerIds.All(y => y != x.Id)))
                {
                    directions.Add(question.Explanation);
                }
            }

            // Add result to database
            var result = new Result
            {
                Id = Guid.NewGuid(),
                AllQuestionsCount = questions.Count(),
                CorrectAnswersCount = userCorrectAnswerIds.Count,
                Created = DateTime.UtcNow,
                UserProfileId = model.UserProfileId,
                ThemeId = model.ThemeId
            };

            if (model.IsAdmin)
            {
                return result.ToResultResponse(directions);
            }

            UnitOfWork.ResultRepository.Add(result);
            await UnitOfWork.SaveChangesAsync();

            return result.ToResultResponse(directions);
        }
    }
}