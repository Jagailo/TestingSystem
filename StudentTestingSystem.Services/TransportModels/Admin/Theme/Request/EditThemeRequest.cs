using System;
using System.ComponentModel.DataAnnotations;

namespace StudentTestingSystem.Services.TransportModels.Admin.Theme.Request
{
    public class EditThemeRequest
    {
        public Guid Id { get; set; }
        public Guid SubjectId { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Title")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "The theme title must be between 3 and 250 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Time for passing the theme (in minutes)")]
        [Range(1, 300, ErrorMessage = "The time should be from 1 to 300 minutes")]
        public int TimeLine { get; set; }
    }
}