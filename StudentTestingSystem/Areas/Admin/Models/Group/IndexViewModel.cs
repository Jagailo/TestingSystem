using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Group.Response;
using System.Collections.Generic;

namespace StudentTestingSystem.Areas.Admin.Models.Group
{
    public class IndexViewModel
    {
        public IEnumerable<GroupListDto> Groups { get; set; }
        public PageInfo<GroupListDto> PageInfo { get; set; }
        public List<Breadcrumb> Breadcrumb { get; set; }
    }
}