using System;
using System.Collections.Generic;
using StudentTestingSystem.Domain.Infrastructure;

namespace StudentTestingSystem.Domain.Models.Question
{
    public class Question : IBaseEntity<Guid>, ICreated
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Explanation { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<QuestionTheme> QuestionThemes { get; set; }
        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }
    }
}