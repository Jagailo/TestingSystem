using System;
using System.ComponentModel.DataAnnotations;

namespace StudentTestingSystem.Services.TransportModels.Admin.Group.Response
{
    public class GroupListDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Created { get; set; }
    }
}
