using StudentTestingSystem.Domain.Infrastructure;
using System;
using StudentTestingSystem.Domain.Models.Group;

namespace StudentTestingSystem.Domain.Repositories
{
    public interface IGroupRepository : IRepository<Group, Guid>
    {
    }
}