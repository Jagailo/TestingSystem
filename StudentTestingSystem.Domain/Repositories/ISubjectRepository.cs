using StudentTestingSystem.Domain.Infrastructure;
using System;
using StudentTestingSystem.Domain.Models.Subject;

namespace StudentTestingSystem.Domain.Repositories
{
    public interface ISubjectRepository : IRepository<Subject, Guid>
    {
    }
}