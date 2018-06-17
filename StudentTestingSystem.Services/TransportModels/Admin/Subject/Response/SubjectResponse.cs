using System;

namespace StudentTestingSystem.Services.TransportModels.Admin.Subject.Response
{
    public class SubjectResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid? UserProfileId { get; set; }
        public string UserName { get; set; }
        public int ThemesCount { get; set; }
    }
}