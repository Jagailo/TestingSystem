using System;
using System.ComponentModel.DataAnnotations;

namespace StudentTestingSystem.Services.TransportModels.Admin.Result.Response
{
    public class SubjectThemeResultsResponse
    {
        public Guid Id { get; set; }
        public int CorrectAnswersCount { get; set; }
        public int AllQuestionsCount { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy H:mm tt}")]
        public DateTime Created { get; set; }
        public Guid UserProfileId { get; set; }
        public string FullName { get; set; }
        public Guid? GroupId { get; set; }
        public int? Group { get; set; }
        public int Mark { get; set; }
    }
}