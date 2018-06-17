using StudentTestingSystem.Domain.Repositories;
using System;
using StudentTestingSystem.Domain.Models.Group;

namespace StudentTestingSystem.SQLRepository.Repositories
{
    public class GroupRepository : RepositoryBase<Group, Guid>, IGroupRepository
    {
        public GroupRepository(ApplicationDbContext dataContext) : base(dataContext)
        {
        }
    }
}