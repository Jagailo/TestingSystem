using System;

namespace StudentTestingSystem.Domain.Models.Question
{
    public class QuestionAnswer
    {
        public Guid AnswerId { get; set; }
        public virtual Answer.Answer Answer { get; set; }
        public Guid QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}