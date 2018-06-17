using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace StudentTestingSystem.Services.TransportModels.Admin.Result.Response
{
    public class ProfileResultsResponse
    {
        public Guid Id { get; set; }
        [DisplayName("Count of correct answers")]
        public int CorrectAnswersCount { get; set; }
        [DisplayName("Count of questions")]
        public int AllQuestionsCount { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy H:mm tt }")]
        public DateTime Created { get; set; }
        public Guid? GroupId { get; set; }
        [DisplayName("Group")]
        public int? Number { get; set; }
        public Guid ThemeId { get; set; }
        [DisplayName("Theme")]
        public string ThemeTitle { get; set; }
        public Guid? SubjectId { get; set; }
        public string Subject { get; set; }
        public int Mark { get; set; }
    }
}
