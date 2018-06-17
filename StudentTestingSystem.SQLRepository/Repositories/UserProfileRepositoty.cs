using System;
using StudentTestingSystem.Domain.Models.Profile;
using StudentTestingSystem.Domain.Repositories;

namespace StudentTestingSystem.SQLRepository.Repositories
{
    public class UserProfileRepositoty : RepositoryBase<UserProfile, Guid>, IUserProfileRepository
    {
        public UserProfileRepositoty(ApplicationDbContext context) : base(context)
        {
        }
    }
}
