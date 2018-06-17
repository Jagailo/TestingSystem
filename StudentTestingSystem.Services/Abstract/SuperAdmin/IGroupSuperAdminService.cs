using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Group.Response;
using System;
using System.Threading.Tasks;

namespace StudentTestingSystem.Services.Abstract.SuperAdmin
{
    public interface IGroupSuperAdminService
    {
        Task DeleteAllGroupResultsAsync(Guid groupId);
        Task DeleteGroupAsync(Guid groupId);
        Task<PageInfo<GroupListDto>> GetAllGroupsAsync(int page, int pageSize);
        Task<GroupListDto> GetGroupByIdAsync(Guid groupId);
        Task<bool> CheckIfGroupHasResults(Guid groupId);
    }
}
