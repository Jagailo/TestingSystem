using StudentTestingSystem.Services.TransportModels.Admin.Answer.Request;
using System;
using System.Collections.Generic;
using System.Web;

namespace StudentTestingSystem.Services.TransportModels.Admin.Question.Request
{
    public class CreateQuestionRequest
    {
        public string Title { get; set; }
        public string Explanation { get; set; }
        public Guid ThemeId { get; set; }
        public List<AddAnswerRequest> Answers { get; set; }
    }
}