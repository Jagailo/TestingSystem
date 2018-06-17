using System;
using System.Collections.Generic;
using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Domain.Models.Question;

namespace StudentTestingSystem.Domain.Models.Answer
{
    public class Answer : IBaseEntity<Guid>, ICreated
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsCorrectAnswer { get; set; }
        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }
        public DateTime Created { get; set; }
    }
}