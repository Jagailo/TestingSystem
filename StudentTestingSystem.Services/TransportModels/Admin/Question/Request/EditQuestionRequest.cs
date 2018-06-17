using StudentTestingSystem.Services.TransportModels.Admin.Answer.Request;
using System;
using System.Collections.Generic;

namespace StudentTestingSystem.Services.TransportModels.Admin.Question.Request
{
    public class EditQuestionRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid ThemeId { get; set; }
        public string ImageUrl { get; set; }
        public string Explanation { get; set; }
        public List<UpdateAnswerRequest> Answers { get; set; }
    }
}