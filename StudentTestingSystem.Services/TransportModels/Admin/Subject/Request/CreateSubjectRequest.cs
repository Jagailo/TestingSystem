using StudentTestingSystem.Services.TransportModels.Student.User.Request;
using System.ComponentModel.DataAnnotations;

namespace StudentTestingSystem.Services.TransportModels.Admin.Subject.Request
{
    public class CreateSubjectRequest : UserRequest
    {
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Title")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "The subject title must be between 3 and 250 characters")]
        public string Title { get; set; }
    }
}