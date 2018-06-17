using StudentTestingSystem.Services.TransportModels.Admin.Answer.Request;
using StudentTestingSystem.Services.TransportModels.Admin.Answer.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentTestingSystem.Services.Abstract.Admin
{
    public interface IAnswerAdminService
    {
        Task<List<AnswerListDto>> GetAllAnswersByQuestionIdAsync(Guid questionId);
        Task<AnswerDto> GetAnswerByIdAsync(Guid answerId);
        Task AddAnswerAsync(AddAnswerRequest request);
        Task UpdateAnswerAsync(UpdateAnswerRequest request);
        Task DeleteAnswerAsync(Guid answerId);
        Task DeleteAnswersByQuestionIdAsync(Guid questionId);
    }
}