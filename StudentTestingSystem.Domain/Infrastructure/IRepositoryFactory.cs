using StudentTestingSystem.Domain.Repositories;

namespace StudentTestingSystem.Domain.Infrastructure
{
    public interface IRepositoryFactory
    {
        IUserRepository UserRepository { get; }
        IUserProfileRepository UserProfileRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IQuestionThemeRepository QuestionThemeRepository { get; }
        IThemeRepository ThemeRepository { get; }
        IResultRepository ResultRepository { get; }
        ISubjectRepository SubjectRepository { get; }
        IGroupRepository GroupRepository { get; }
        IAnswerRepository AnswerRepository { get; }
        IQuestionAnswerRepository QuestionAnswerRepository { get; }
    }
}