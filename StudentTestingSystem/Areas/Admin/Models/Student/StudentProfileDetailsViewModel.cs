using System;
using System.ComponentModel.DataAnnotations;

namespace StudentTestingSystem.Areas.Admin.Models.Student
{
    public class StudentProfileDetailsViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Domain.Models.Group.Group Group { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Created { get; set; }
        public string UserId { get; set; }
    }
}