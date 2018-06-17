using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTestingSystem.Services.TransportModels.Admin.Result.Response
{
    public class ThemeResultsResponse
    {
        public Guid Id { get; set; }
        public int CorrectAnswersCount { get; set; }
        public int AllQuestionsCount { get; set; }
        public DateTime Created { get; set; }
        public Guid UserProfileId { get; set; }
        public string FullName { get; set; }
        public Guid? SubjectId { get; set; }
        public string Subject { get; set; }
        public Guid? ThemeId { get; set; }
        public string Theme { get; set; }
        public int Mark { get; set; }
    }
}
