using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Question.Request;
using StudentTestingSystem.Services.TransportModels.Admin.Question.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentTestingSystem.Services.Abstract.Admin
{
    public interface IQuestionAdminService
    {
        Task<QuestionResponse> CreateQuestionAsync(CreateQuestionRequest model);
        Task<string> UploadAttachmentAsync(UploadQuestionAttachmentRequest model);
        Task DeleteAttachmentAsync(Guid questionId);
        Task DeleteQuestionAsync(Guid questionId);
        Task DeleteQuestionsByThemeIdAsync(Guid themeId);
        Task EditQuestionAsync(EditQuestionRequest model);
        Task<PageInfo<QuestionResponse>> GetAllQuestionsByThemeIdAsync(int page, int pageSize, Guid themeId);
        Task<IEnumerable<QuestionResponse>> GetAllQuestionsByThemeIdAsync(Guid themeId);
        Task<QuestionResponse> GetQuestionByIdAsync(Guid id);
        Task<Guid> GetThemeIdByQuestionIdAsync(Guid questionId);
    }
}