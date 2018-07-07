using System;

namespace StudentTestingSystem.Areas.Student.Models.Question
{
    public class ChoiceModel
    {
        public Guid AnswerId { get; set; }
        public string Title { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }
}