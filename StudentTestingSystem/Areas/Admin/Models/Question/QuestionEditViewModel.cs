using StudentTestingSystem.Services.TransportModels.Admin.Answer.Request;
using System;
using System.Collections.Generic;
using System.Web;

namespace StudentTestingSystem.Areas.Admin.Models.Question
{
    public class QuestionEditViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public bool DeleteImage { get; set; }
        public HttpPostedFileBase File { get; set; }
        public Guid ThemeId { get; set; }
        public string Explanation { get; set; }
        public int MaxCountAnswers { get; set; }
        public List<UpdateAnswerRequest> Answers { get; set; }        
    }
}