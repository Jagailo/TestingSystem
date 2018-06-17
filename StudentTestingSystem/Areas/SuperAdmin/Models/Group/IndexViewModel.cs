using StudentTestingSystem.Areas.Admin.Models;
using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Group.Response;
using System.Collections.Generic;

namespace StudentTestingSystem.Areas.SuperAdmin.Models.Group
{
    public class IndexViewModel
    {
        public PageInfo<GroupListDto> PageInfo { get; set; }
        public IEnumerable<GroupListDto> Groups { get; set; }
        public List<Breadcrumb> Breadcrumb { get; set; }
    }
}