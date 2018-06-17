using System;
using System.Collections.Generic;

namespace StudentTestingSystem.Areas.Student.Models.Question
{
    public class QuestionViewModel
    {
        public int TotalQuestionsCount { get; set; }
        public int QuestionNumber { get; set; }
        public Guid QuestionId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public Guid ThemeId { get; set; }
        public string ThemeTitle { get; set; }
        public List<ChoiceModel> Options { get; set; }
    }
}