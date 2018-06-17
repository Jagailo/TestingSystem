using StudentTestingSystem.Services.TransportModels.Student.User.Request;

namespace StudentTestingSystem.Services.TransportModels.SuperAdmin.Profile.Request
{
    public class CreateProfileRequest : UserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
