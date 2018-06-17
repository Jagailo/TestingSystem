using StudentTestingSystem.Domain.Repositories;
using System;
using StudentTestingSystem.Domain.Models.Theme;

namespace StudentTestingSystem.SQLRepository.Repositories
{
    public class ThemeRepository : RepositoryBase<Theme, Guid>, IThemeRepository
    {
        public ThemeRepository(ApplicationDbContext dataContext) : base(dataContext)
        {
        }
    }
}