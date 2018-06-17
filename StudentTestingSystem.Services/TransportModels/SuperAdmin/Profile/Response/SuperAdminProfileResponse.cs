using System;
using System.ComponentModel.DataAnnotations;

namespace StudentTestingSystem.Services.TransportModels.SuperAdmin.Profile.Response
{
    public class SuperAdminProfileResponse
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string Email { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Created { get; set; }
    }
}