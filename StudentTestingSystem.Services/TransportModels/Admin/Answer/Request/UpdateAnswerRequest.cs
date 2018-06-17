using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTestingSystem.Services.TransportModels.Admin.Answer.Request
{
    public class UpdateAnswerRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }
}
