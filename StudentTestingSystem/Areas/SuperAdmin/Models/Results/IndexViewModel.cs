using StudentTestingSystem.Areas.Admin.Models;
using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Result.Response;
using System;
using System.Collections.Generic;

namespace StudentTestingSystem.Areas.SuperAdmin.Models.Results
{
    public class IndexViewModel
    {
        public Guid GroupId { get; set; }
        public PageInfo<GroupResultsResponse> PageInfo { get; set; }
        public IEnumerable<GroupResultsResponse> Results { get; set; }
        public List<Breadcrumb> Breadcrumb { get; set; }
    }
}