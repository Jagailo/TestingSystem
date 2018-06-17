using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Domain.Models.Profile;
using StudentTestingSystem.Domain.Models.Subject;

namespace StudentTestingSystem.Domain.Models.Group
{
    public class Group : IBaseEntity<Guid>, ICreated
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public DateTime Created { get; set; }

        public virtual ICollection<UserProfile> Students { get; set; }
    }
}