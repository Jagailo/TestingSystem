using System.Threading.Tasks;
using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Domain.Repositories;
using StudentTestingSystem.SQLRepository.Repositories;

namespace StudentTestingSystem.SQLRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        protected ApplicationDbContext DbContext;

        public UnitOfWork(ApplicationDbContext context)
        {
            DbContext = context;
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return DbContext.SaveChangesAsync();
        }

        #region Repositories

        private IUserRoleRepository _userRoleRepository;
        public IUserRoleRepository UserRoleRepository => _userRoleRepository ?? (_userRoleRepository = new UserRoleRepository(DbContext));

        private IUserRepository _userRepository;
        public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(DbContext));

        private IUserProfileRepository _userProfileRepository;
        public IUserProfileRepository UserProfileRepository => _userProfileRepository ?? (_userProfileRepository = new UserProfileRepositoty(DbContext));

        private IGroupRepository _groupRepository;
        public IGroupRepository GroupRepository => _groupRepository ?? (_groupRepository = new GroupRepository(DbContext));

        private IQuestionRepository _questionRepository;
        public IQuestionRepository QuestionRepository => _questionRepository ?? (_questionRepository = new QuestionRepository(DbContext));

        private IQuestionThemeRepository _questionThemeRepository;
        public IQuestionThemeRepository QuestionThemeRepository => _questionThemeRepository ?? (_questionThemeRepository = new QuestionThemeRepository(DbContext));

        private IResultRepository _resultRepository;
        public IResultRepository ResultRepository => _resultRepository ?? (_resultRepository = new ResultRepository(DbContext));

        private ISubjectRepository _subjectRepository;
        public ISubjectRepository SubjectRepository => _subjectRepository ?? (_subjectRepository = new SubjectRepository(DbContext));

        private IThemeRepository _themeRepository;
        public IThemeRepository ThemeRepository => _themeRepository ?? (_themeRepository = new ThemeRepository(DbContext));

        private IAnswerRepository _answerRepository;
        public IAnswerRepository AnswerRepository => _answerRepository ?? (_answerRepository = new AnswerRepository(DbContext));

        private IQuestionAnswerRepository _questionAnswerRepository;
        public IQuestionAnswerRepository QuestionAnswerRepository => _questionAnswerRepository ?? (_questionAnswerRepository = new QuestionAnswerRepository(DbContext));

        #endregion
    }
}