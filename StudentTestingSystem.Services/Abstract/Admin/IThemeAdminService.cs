using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Theme.Request;
using StudentTestingSystem.Services.TransportModels.Admin.Theme.Response;
using System;
using System.Threading.Tasks;

namespace StudentTestingSystem.Services.Abstract.Admin
{
    public interface IThemeAdminService
    {
        Task CreateThemeAsync(CreateThemeRequest model);
        Task DeleteThemeAsync(Guid themeId);
        Task EditThemeAsync(EditThemeRequest model);
        Task<PageInfo<ThemeResponse>> GetAllThemesBySubjectId(Guid subjectId, int page, int pageSize);
        Task<ThemeResponse> GetThemeByIdAsync(Guid id);
    }
}