using StudentTestingSystem.Domain.Models.Account;
using StudentTestingSystem.Domain.Repositories;

namespace StudentTestingSystem.SQLRepository.Repositories
{
    public class UserRepository : RepositoryBase<User,string>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public bool DeleteUser(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
