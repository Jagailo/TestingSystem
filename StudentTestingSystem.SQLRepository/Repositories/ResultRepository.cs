using StudentTestingSystem.Domain.Repositories;
using System;
using StudentTestingSystem.Domain.Models.Question;

namespace StudentTestingSystem.SQLRepository.Repositories
{
    public class ResultRepository : RepositoryBase<Result, Guid>, IResultRepository
    {
        public ResultRepository(ApplicationDbContext dataContext) : base(dataContext)
        {
        }
    }
}