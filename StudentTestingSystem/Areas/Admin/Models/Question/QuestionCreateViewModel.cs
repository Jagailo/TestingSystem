using System;
using System.Web;

namespace StudentTestingSystem.Areas.Admin.Models.Question
{
    public class QuestionCreateViewModel
    {
        public string Title { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public string Explanation { get; set; }
        public Guid ThemeId { get; set; }
        public int MaxCountAnswers { get; set; }
    }
}