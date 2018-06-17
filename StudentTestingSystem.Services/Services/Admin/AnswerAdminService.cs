using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Domain.Models.Answer;
using StudentTestingSystem.Domain.Models.Question;
using StudentTestingSystem.Services.Abstract.Admin;
using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Answer.Request;
using StudentTestingSystem.Services.TransportModels.Admin.Answer.Response;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace StudentTestingSystem.Services.Services.Admin
{
    public class AnswerAdminService : BaseService, IAnswerAdminService
    {
        public AnswerAdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<AnswerListDto>> GetAllAnswersByQuestionIdAsync(Guid questionId)
        {
            var answers = await UnitOfWork.QuestionAnswerRepository.Query(x => x.QuestionId == questionId)
                .Select(x => x.Answer)
                .ToListAsync();
            return answers.Select(x => x.ToAnswerListDto()).ToList();
        }

        //TODO: Exception
        public async Task<AnswerDto> GetAnswerByIdAsync(Guid answerId)
        {
            var answer = await UnitOfWork.AnswerRepository.Query(x => x.Id == answerId)
                .FirstOrDefaultAsync();
            if (answer == null)
            {
                throw new ArgumentNullException();
            }
            return answer.ToAnswerDto();
        }

        public async Task AddAnswerAsync(AddAnswerRequest request)
        {
            var questionAnswer = new QuestionAnswer
            {
                QuestionId = request.QuestionId,
                Answer = new Answer
                {
                    Id = Guid.NewGuid(),
                    Title = request.Title,
                    IsCorrectAnswer = request.IsCorrectAnswer,
                    Created = DateTime.UtcNow
                }
            };
            UnitOfWork.QuestionAnswerRepository.Add(questionAnswer);
            await UnitOfWork.SaveChangesAsync();
        }

        //TODO: Exception
        public async Task UpdateAnswerAsync(UpdateAnswerRequest request)
        {
            var answer = await UnitOfWork.AnswerRepository.Query(x => x.Id == request.Id)
                .FirstOrDefaultAsync();
            if (answer == null)
            {
                throw new ArgumentNullException();
            }
            answer.Title = request.Title;
            answer.IsCorrectAnswer = request.IsCorrectAnswer;
            UnitOfWork.AnswerRepository.Update(answer);
            await UnitOfWork.SaveChangesAsync();
        }

        //TODO: Exception
        public async Task DeleteAnswerAsync(Guid answerId)
        {
            var answer = await UnitOfWork.AnswerRepository.Query(x => x.Id == answerId)
                .FirstOrDefaultAsync();
            if (answer == null)
            {
                throw new ArgumentNullException();
            }
            UnitOfWork.AnswerRepository.Delete(answer);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAnswersByQuestionIdAsync(Guid questionId)
        {
            var answersIds = await UnitOfWork.QuestionAnswerRepository.Query(x => x.QuestionId == questionId)
                .Select(x => x.AnswerId)
                .ToListAsync();
            foreach (var answerId in answersIds)
            {
                await DeleteAnswerAsync(answerId);
            }
        }
    }
}