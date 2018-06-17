using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Result.Response;
using System;
using System.Collections.Generic;

namespace StudentTestingSystem.Areas.Admin.Models.Result
{
    public class ProfileResultsViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<ProfileResultsResponse> Results { get; set; }
        public PageInfo<ProfileResultsResponse> PageInfo { get; set; }
        public List<Breadcrumb> Breadcrumb { get; set; }
    }
}