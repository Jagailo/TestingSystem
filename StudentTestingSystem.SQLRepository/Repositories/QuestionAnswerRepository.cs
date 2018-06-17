using StudentTestingSystem.Domain.Models.Question;
using StudentTestingSystem.Domain.Repositories;

namespace StudentTestingSystem.SQLRepository.Repositories
{
    public class QuestionAnswerRepository : RepositoryBaseWithoutKey<QuestionAnswer>, IQuestionAnswerRepository
    {
        public QuestionAnswerRepository(ApplicationDbContext dataContext) : base(dataContext)
        {
        }
    }
}