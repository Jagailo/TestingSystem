using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Student.Profile.Response;
using System;
using System.Collections.Generic;

namespace StudentTestingSystem.Areas.Admin.Models.Student
{
    public class IndexViewModel
    {
        public Guid GroupId { get; set; }
        public IEnumerable<ProfileResponse> Students { get; set; }
        public PageInfo<ProfileResponse> PageInfo { get; set; }
        public List<Breadcrumb> Breadcrumb { get; set; }
    }
}