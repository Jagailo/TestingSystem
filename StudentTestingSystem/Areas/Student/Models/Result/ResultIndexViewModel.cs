using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace StudentTestingSystem.Areas.Student.Models.Result
{
    public class ResultIndexViewModel
    {
        public Guid Id { get; set; }
        [DisplayName("Count of correct answers")]
        public int CorrectAnswersCount { get; set; }
        [DisplayName("Count of questions")]
        public int AllQuestionsCount { get; set; }
        public DateTime Created { get; set; }
        public Guid? GroupId { get; set; }
        [DisplayName("Group")]
        public int? Number { get; set; }
        public Guid ThemeId { get; set; }
        [DisplayName("Theme")]
        public string ThemeTitle { get; set; }
        public int Mark { get; set; }
        public List<string> Directions { get; set; }
    }
}