using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentTestingSystem.Areas.Student.Models.Question
{
    public class ChoiceModel
    {
        public Guid AnswerId { get; set; }
        public string Title { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }
}