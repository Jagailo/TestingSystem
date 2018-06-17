using System;
using StudentTestingSystem.Domain.Models.Answer;
using StudentTestingSystem.Domain.Repositories;

namespace StudentTestingSystem.SQLRepository.Repositories
{
    public class AnswerRepository : RepositoryBase<Answer, Guid>, IAnswerRepository
    {
        public AnswerRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}