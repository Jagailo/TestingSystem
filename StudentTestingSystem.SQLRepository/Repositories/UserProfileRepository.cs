using System;
using StudentTestingSystem.Domain.Models.Profile;
using StudentTestingSystem.Domain.Repositories;

namespace StudentTestingSystem.SQLRepository.Repositories
{
    public class UserProfileRepository : RepositoryBase<UserProfile, Guid>, IUserProfileRepository
    {
        public UserProfileRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
