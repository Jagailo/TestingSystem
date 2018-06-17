using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Theme.Response;
using System;
using System.Collections.Generic;

namespace StudentTestingSystem.Areas.Admin.Models.Theme
{
    public class ThemeIndexViewModel
    {
        public IEnumerable<ThemeResponse> Themes { get; set; }
        public Guid SubjectId { get; set; }
        public PageInfo<ThemeResponse> PageInfo { get; set; }
        public List<Breadcrumb> Breadcrumb { get; set; }
    }
}