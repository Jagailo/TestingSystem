using StudentTestingSystem.Domain.Models.Subject;
using StudentTestingSystem.Services.TransportModels.Admin.Subject.Request;
using StudentTestingSystem.Services.TransportModels.Admin.Subject.Response;

namespace StudentTestingSystem.Services.Extensions
{
    public static class SubjectMapper
    {
        public static SubjectResponse ToSubjectResponse(this Subject subject)
        {
            SubjectResponse subjectResponse = new SubjectResponse()
            {
                Id = subject.Id,
                Title = subject.Title,
                UserProfileId = subject.UserProfileId,
                UserName = $"{subject.UserProfile.LastName} {subject.UserProfile.FirstName}",
                ThemesCount = subject.Themes.Count
            };
            return subjectResponse;
        }

        public static EditSubjectRequest ToEditSubjectRequest(this SubjectResponse subjectResponse)
        {
            EditSubjectRequest editSubjectRequest = new EditSubjectRequest()
            {
                Id = subjectResponse.Id,
                Title = subjectResponse.Title,
                UserProfileId = subjectResponse.UserProfileId
            };
            return editSubjectRequest;
        }
    }
}