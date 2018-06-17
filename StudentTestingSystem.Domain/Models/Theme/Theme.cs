using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Domain.Models.Question;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentTestingSystem.Domain.Models.Theme
{
    public class Theme : IBaseEntity<Guid>, ICreated
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Created { get; set; }

        public Guid SubjectId { get; set; }
        public virtual Subject.Subject Subject { get; set; }
        public int TimeLine { get; set; } // Time for passing test in minutes

        public virtual ICollection<Result> Results { get; set; }
        public virtual ICollection<QuestionTheme> QuestionThemes { get; set; }
    }
}