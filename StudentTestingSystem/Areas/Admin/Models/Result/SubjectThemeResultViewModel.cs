using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Result.Response;
using System;
using System.Collections.Generic;

namespace StudentTestingSystem.Areas.Admin.Models.Result
{
    public class SubjectThemeResultViewModel
    {
        public Guid ThemeId { get; set; }
        public PageInfo<SubjectThemeResultsResponse> PageInfo { get; set; }
        public IEnumerable<SubjectThemeResultsResponse> Results { get; set; }
        public List<Breadcrumb> Breadcrumb { get; set; }
    }
}