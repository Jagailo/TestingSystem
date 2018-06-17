using StudentTestingSystem.Domain.Repositories;
using System;
using StudentTestingSystem.Domain.Models.Question;

namespace StudentTestingSystem.SQLRepository.Repositories
{
    public class QuestionRepository : RepositoryBase<Question, Guid>, IQuestionRepository
    {
        public QuestionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}