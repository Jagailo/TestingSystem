using StudentTestingSystem.Services.Extensions;
using StudentTestingSystem.Services.TransportModels.Admin.Question.Response;
using System;
using System.Collections.Generic;

namespace StudentTestingSystem.Areas.Admin.Models.Question
{
    public class QuestionIndexViewModel
    {
        public IEnumerable<QuestionResponse> Questions { get; set; }
        public Guid ThemeId { get; set; }
        public PageInfo<QuestionResponse> PageInfo { get; set; }
        public List<Breadcrumb> Breadcrumb { get; set; }
    }
}