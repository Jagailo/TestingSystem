using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Result.Response;
using System;
using System.Collections.Generic;

namespace StudentTestingSystem.Areas.Admin.Models.Result
{
    public class ThemeResultsViewModel
    {
        public int Group { get; set; }
        public Guid GroupId { get; set; }
        public Guid ThemeId { get; set; }
        public PageInfo<ThemeResultsResponse> PageInfo { get; set; }
        public IEnumerable<ThemeResultsResponse> Results { get; set; }
        public List<Breadcrumb> Breadcrumb { get; set; }
    }
}