using StudentTestingSystem.Domain.Models.Question;
using StudentTestingSystem.Domain.Repositories;

namespace StudentTestingSystem.SQLRepository.Repositories
{
    public class QuestionThemeRepository : RepositoryBaseWithoutKey<QuestionTheme>, IQuestionThemeRepository
    {
        public QuestionThemeRepository(ApplicationDbContext dataContext) : base(dataContext)
        {
        }
    }
}