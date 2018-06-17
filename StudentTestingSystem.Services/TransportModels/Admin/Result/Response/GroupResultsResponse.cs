using System;

namespace StudentTestingSystem.Services.TransportModels.Admin.Result.Response
{
    public class GroupResultsResponse
    {
        public Guid ThemeId { get; set; }
        public string Theme { get; set; }
        public Guid? SubjectId { get; set; }
        public string Subject { get; set; }
    }
}