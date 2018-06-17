using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Group.Request;
using StudentTestingSystem.Services.TransportModels.Admin.Group.Response;

namespace StudentTestingSystem.Services.Abstract.Admin
{
    public interface IGroupAdminService
    {
        Task<PageInfo<GroupListDto>> GetAllGroupsAsync(int page, int pageSize);
        Task<GroupDto> GetGroupByIdAsync(Guid groupId);
        Task AddGroupAsync(AddGroupRequest request);
        Task UpdateGroupAsync(UpdateGroupRequest request);
        Task DeleteGroupAsync(Guid groupId);
        Task<IEnumerable<GroupListDto>> GetAllGroupsAsync();
    }
}
