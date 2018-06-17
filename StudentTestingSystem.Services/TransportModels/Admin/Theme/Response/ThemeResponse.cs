using System;

namespace StudentTestingSystem.Services.TransportModels.Admin.Theme.Response
{
    public class ThemeResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid SubjectId { get; set; }
        public string SubjectTitle { get; set; }
        public int TimeLine { get; set; }
        public int QuestionsCount { get; set; }
    }
}