using StudentTestingSystem.Domain.Models.Group;
using System;

namespace StudentTestingSystem.Services.TransportModels.Student.Profile.Response
{
    public class ProfileResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Guid? GroupId { get; set; }
        public virtual Group Group { get; set; }
        public DateTime Created { get; set; }
        public string UserId { get; set; }
    }
}