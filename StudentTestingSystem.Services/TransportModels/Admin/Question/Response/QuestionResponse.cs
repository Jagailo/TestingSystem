using StudentTestingSystem.Services.TransportModels.Admin.Answer.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentTestingSystem.Services.TransportModels.Admin.Question.Response
{
    public class QuestionResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Explanation { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy H:mm tt}")]
        public DateTime Created { get; set; }
        public Guid ThemeId { get; set; }
        public string ThemeTitle { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}