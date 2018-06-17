using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Domain.Models.Account;

namespace StudentTestingSystem.Domain.Repositories
{
    public interface IUserRepository : IRepository<User, string>
    {
        bool DeleteUser(string id);
    }
}