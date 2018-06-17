using System;

namespace StudentTestingSystem.Areas.Student.Models.Question
{
    public class AnswerViewModel
    {
        public Guid ThemeId { get; set; }
        public Guid QuestionId { get; set; }
        public int QuestionNumber { get; set; }
        public Guid SelectedAnswerId { get; set; }
    }
}