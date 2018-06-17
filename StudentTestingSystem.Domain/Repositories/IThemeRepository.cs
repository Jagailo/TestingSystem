using StudentTestingSystem.Domain.Infrastructure;
using System;
using StudentTestingSystem.Domain.Models.Theme;

namespace StudentTestingSystem.Domain.Repositories
{
    public interface IThemeRepository : IRepository<Theme, Guid>
    {
    }
}