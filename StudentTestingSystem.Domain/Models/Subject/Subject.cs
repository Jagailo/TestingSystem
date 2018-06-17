using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Domain.Models.Profile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentTestingSystem.Domain.Models.Subject
{
    public class Subject : IBaseEntity<Guid>, ICreated
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Created { get; set; }

        public Guid? UserProfileId { get; set; }
        public virtual UserProfile UserProfile { get; set; }

        public virtual ICollection<Theme.Theme> Themes { get; set; }
    }
}