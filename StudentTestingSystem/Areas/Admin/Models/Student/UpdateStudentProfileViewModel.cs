using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StudentTestingSystem.Areas.Admin.Models.Student
{
    public class UpdateStudentProfileViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "First name")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 150 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Last name")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 150 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        public Guid? GroupId { get; set; }
        public List<SelectListItem> Groups { get; set; }
    }
}