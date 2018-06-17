using System;
using System.Collections.Generic;
using StudentTestingSystem.Domain.Models.Profile;

namespace StudentTestingSystem.Services.TransportModels.Admin.Result.Response
{
    public class ResultResponse
    {
        public Guid Id { get; set; }
        public int CorrectAnswersCount { get; set; }
        public int AllQuestionsCount { get; set; }
        public DateTime Created { get; set; }
        public Guid UserProfileId { get; set; }
        public Guid ThemeId { get; set; }
        public List<string> Directions { get; set; }
    }
}
