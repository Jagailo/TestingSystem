using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentTestingSystem.Services.TransportModels.Admin.Question.Response;

namespace StudentTestingSystem.Areas.Student.Models.Result
{
    public class AddResultViewModel
    {
        public int TotalQuestionsInTheme { get; set; }
        public int QuestionNumber { get; set; }
        public Guid TestId { get; set; }
        public string TestName { get; set; }
        public int Point { get; set; }
        public QuestionResponse Question { get; set; }
    }
}