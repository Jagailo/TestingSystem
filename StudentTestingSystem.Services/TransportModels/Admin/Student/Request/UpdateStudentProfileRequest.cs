using System;

namespace StudentTestingSystem.Services.TransportModels.Admin.Student.Request
{
    public class UpdateStudentProfileRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid? GroupId { get; set; }
    }
}
