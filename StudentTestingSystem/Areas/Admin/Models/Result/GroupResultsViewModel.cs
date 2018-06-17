using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Result.Response;
using System;
using System.Collections.Generic;

namespace StudentTestingSystem.Areas.Admin.Models.Result
{
    public class GroupResultsViewModel
    {
        public Guid GroupId { get; set; }
        public PageInfo<GroupResultsResponse> PageInfo { get; set; }
        public IEnumerable<GroupResultsResponse> Results { get; set; }
        public List<Breadcrumb> Breadcrumb { get; set; }
    }
}