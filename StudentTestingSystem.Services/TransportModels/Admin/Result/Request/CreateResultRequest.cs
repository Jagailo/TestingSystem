using System;
using System.Collections.Generic;

namespace StudentTestingSystem.Services.TransportModels.Admin.Result.Request
{
    public class CreateResultRequest
    {
        public bool IsAdmin { get; set; }
        public Guid ThemeId { get; set; }
        public Guid UserProfileId { get; set; }
        public List<Guid> AnswerIds { get; set; }
    }
}
