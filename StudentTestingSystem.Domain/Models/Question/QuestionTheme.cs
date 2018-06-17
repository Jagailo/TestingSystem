using System;
using StudentTestingSystem.Domain.Infrastructure;

namespace StudentTestingSystem.Domain.Models.Question
{
    public class QuestionTheme : ICreated
    {
        public Guid QuestionId { get; set; }
        public virtual Question Question { get; set; }
        public Guid ThemeId { get; set; }
        public virtual Theme.Theme Theme { get; set; }
        public DateTime Created { get; set; }
    }
}