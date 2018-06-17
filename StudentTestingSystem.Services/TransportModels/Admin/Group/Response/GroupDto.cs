using System;

namespace StudentTestingSystem.Services.TransportModels.Admin.Group.Response
{
    public class GroupDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }
        public DateTime Created { get; set; }
    }
}
