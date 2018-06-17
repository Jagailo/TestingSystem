using System;
using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Domain.Models.Answer;

namespace StudentTestingSystem.Domain.Repositories
{
    public interface IAnswerRepository : IRepository<Answer, Guid>
    {
    }
}