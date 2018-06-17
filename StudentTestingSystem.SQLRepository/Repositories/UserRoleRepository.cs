using StudentTestingSystem.Domain.Models.Account;
using StudentTestingSystem.Domain.Repositories;

namespace StudentTestingSystem.SQLRepository.Repositories
{
    public class UserRoleRepository : RepositoryBaseWithoutKey<UserRoles>, IUserRoleRepository
    {
        public UserRoleRepository(ApplicationDbContext dataContext) : base(dataContext)
        {
        }
    }
}
