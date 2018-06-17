using System;
using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Domain.Models.Profile;

namespace StudentTestingSystem.Domain.Repositories
{
    public interface IUserProfileRepository : IRepository<UserProfile, Guid>
    {
    }
}
