using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.SuperAdmin.Profile.Request;
using StudentTestingSystem.Services.TransportModels.SuperAdmin.Profile.Response;
using System;
using System.Threading.Tasks;

namespace StudentTestingSystem.Services.Abstract.SuperAdmin
{
    public interface IProfileSuperAdminService
    {
        Task<SuperAdminProfileResponse> CreateProfileAsync(CreateProfileRequest model);
        Task<PageInfo<SuperAdminProfileResponse>> GetProfilesByRoleIdAsync(string roleId, int page, int pageSize);
        Task<SuperAdminProfileResponse> GetProfileByProfileIdAsync(Guid profileId);
        Task<SuperAdminProfileResponse> GetProfileByUserIdAsync(string userId);
        Task<string> GetUserIdByProfileIdAsync(Guid profileId);
        Task DeleteProfileAsync(Guid profileId);
    }
}