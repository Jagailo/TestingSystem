using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Domain.Models.Question;

namespace StudentTestingSystem.Domain.Repositories
{
    public interface IQuestionAnswerRepository : IRepositoryWithoutKey<QuestionAnswer>
    {
    }
}