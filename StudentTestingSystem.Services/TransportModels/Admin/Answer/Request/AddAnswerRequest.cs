using System;

namespace StudentTestingSystem.Services.TransportModels.Admin.Answer.Request
{
    public class AddAnswerRequest
    {
        public Guid QuestionId { get; set; }
        public string Title { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }
}
