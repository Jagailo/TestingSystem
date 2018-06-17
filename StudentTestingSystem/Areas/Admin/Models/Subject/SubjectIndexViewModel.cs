using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Subject.Response;
using System.Collections.Generic;

namespace StudentTestingSystem.Areas.Admin.Models.Subject
{
    public class SubjectIndexViewModel
    {
        public IEnumerable<SubjectResponse> Subjects { get; set; }
        public PageInfo<SubjectResponse> PageInfo { get; set; }
        public List<Breadcrumb> Breadcrumb { get; set; }
    }
}