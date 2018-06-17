using StudentTestingSystem.Domain.Repositories;
using System;
using StudentTestingSystem.Domain.Models.Subject;

namespace StudentTestingSystem.SQLRepository.Repositories
{
    public class SubjectRepository : RepositoryBase<Subject, Guid>, ISubjectRepository
    {
        public SubjectRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}