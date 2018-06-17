using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTestingSystem.Services.TransportModels.Admin.Group.Request
{
    public class UpdateGroupRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }
    }
}
