using StudentTestingSystem.Areas.Admin.Models;
using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Result.Response;
using System.Collections.Generic;

namespace StudentTestingSystem.Areas.Student.Models.Result
{
    public class ResultsIndexViewModel
    {
        public IEnumerable<ProfileResultsResponse> Results { get; set; }
        public PageInfo<ProfileResultsResponse> PageInfo { get; set; }
        public List<Breadcrumb> Breadcrumb { get; set; }
    }
}