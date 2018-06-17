using StudentTestingSystem.Domain.Infrastructure;
using StudentTestingSystem.Domain.Models.Question;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentTestingSystem.Domain.Models.Profile
{
    public class UserProfile : IBaseEntity<Guid>, ICreated
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SearchFullName { get; set; }
        public string Email { get; set; }
        public Guid? GroupId { get; set; }
        public virtual Group.Group Group { get; set; }
        public virtual ICollection<Result> Results { get; set; }
        public DateTime Created { get; set; }
        public virtual Account.User User { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ICollection<Subject.Subject> Subjects { get; set; }
    }
}