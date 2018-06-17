using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Subject.Request;
using StudentTestingSystem.Services.TransportModels.Admin.Subject.Response;
using System;
using System.Threading.Tasks;

namespace StudentTestingSystem.Services.Abstract.Admin
{
    public interface ISubjectAdminService
    {
        Task CreateSubjectAsync(CreateSubjectRequest model);
        Task DeleteSubjectAsync(Guid subjectId);
        Task EditSubjectAsync(EditSubjectRequest model);
        Task<PageInfo<SubjectResponse>> GetAllSubjects(int page, int pageSize);
        Task<PageInfo<SubjectResponse>> GetAllSubjectsByUserProfileId(int page, int pageSize, Guid userProfileId);
        Task<SubjectResponse> GetSubjectByIdAsync(Guid subjectId);
    }
}