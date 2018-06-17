using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Result.Request;
using StudentTestingSystem.Services.TransportModels.Admin.Result.Response;

namespace StudentTestingSystem.Services.Abstract.Admin
{
    public interface IResultAdminService
    {
        Task<PageInfo<ProfileResultsResponse>> GetResultsByProfileIdAsync(Guid profileId, int page, int pageSize);
        Task<PageInfo<SubjectThemeResultsResponse>> GetSubjectThemeResultsAsync(Guid themeId, int page, int pageSize);
        Task<PageInfo<GroupResultsResponse>> GetResultsByGroupIdAsync(Guid groupId, int page, int pageSize);
        Task<PageInfo<ThemeResultsResponse>> GetResultsByGroupIdThemeIdAsync(Guid groupId, Guid themeId, int page, int pageSize);
        Task DeleteResultsByThemeIdAsync(Guid themeId);
        Task<ResultResponse> CreateResultAsync(CreateResultRequest model);
    }
}
