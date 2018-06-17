using System;
using System.ComponentModel.DataAnnotations;

namespace StudentTestingSystem.Areas.Admin.Models.Group
{
    public class UpdateGroupViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Title")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "The group title must be between 3 and 250 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Group number")]
        [Range(0, 999999999, ErrorMessage = "The group number must be between 1 and 9 digits")]
        public int Number { get; set; }
    }
}