using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentTestingSystem.Domain.Models.Profile;
using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Student.Request;
using StudentTestingSystem.Services.TransportModels.Student.Profile.Response;

namespace StudentTestingSystem.Services.Abstract.Admin
{
    public interface IStudentAdminService
    {
        Task<PageInfo<ProfileResponse>> GetStudentsByGroupIdAsync(Guid groupId, int page, int pageSize);
        Task<IEnumerable<UserProfile>> GetStudentsByGroupIdAsync(Guid groupId);
        Task<ProfileResponse> GetStudentByIdAsync(Guid profileId);
        Task UpdateProfileAsync(UpdateStudentProfileRequest request);
    }
}
