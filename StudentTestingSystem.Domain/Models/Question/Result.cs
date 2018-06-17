using System;
using System.ComponentModel.DataAnnotations;
using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Domain.Models.Account;
using StudentTestingSystem.Domain.Models.Profile;

namespace StudentTestingSystem.Domain.Models.Question
{
    //TODO: Relationships between result, questions, answers, students.
    //TODO: Two properties CorrectAnswersCount and AllQuestionsCount.
    public class Result : IBaseEntity<Guid>, ICreated
    {
        public Guid Id { get; set; }
        [Required]
        public int CorrectAnswersCount { get; set; }
        [Required]
        public int AllQuestionsCount { get; set; }
        [Required]
        public DateTime Created { get; set; }
        public Guid UserProfileId { get; set; }
        public virtual UserProfile Student { get; set; }
        public Guid ThemeId { get; set; }
        public virtual Theme.Theme Theme { get; set; }
    }
}