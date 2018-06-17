using StudentTestingSystem.Domain.Infrastructure;
using System;
using StudentTestingSystem.Domain.Models.Question;

namespace StudentTestingSystem.Domain.Repositories
{
    public interface IResultRepository : IRepository<Result, Guid>
    {
    }
}