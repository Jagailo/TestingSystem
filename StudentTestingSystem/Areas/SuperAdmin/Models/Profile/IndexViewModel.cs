using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.SuperAdmin.Profile.Response;

namespace StudentTestingSystem.Areas.SuperAdmin.Models.Profile
{
    public class IndexViewModel
    {
        public string Role { get; set; }
        public PageInfo<SuperAdminProfileResponse> PageInfo { get; set; }
        public IEnumerable<SuperAdminProfileResponse> Profiles { get; set; }
    }
}