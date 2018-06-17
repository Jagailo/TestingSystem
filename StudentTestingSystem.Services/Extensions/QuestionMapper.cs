using StudentTestingSystem.Domain.Models.Question;
using StudentTestingSystem.Services.TransportModels.Admin.Question.Request;
using StudentTestingSystem.Services.TransportModels.Admin.Question.Response;
using System.Linq;

namespace StudentTestingSystem.Services.Extensions
{
    public static class QuestionMapper
    {
        
        public static QuestionResponse ToQuestionResponse(this Question question)
        {
            QuestionResponse questionResponse = new QuestionResponse()
            {
                Id = question.Id,
                Title = question.Title,
                Explanation = question.Explanation,
                ImageUrl = UrlHelper.GetUri(question.Image),
                Created = question.Created,
                ThemeId = question.QuestionThemes.Select(x => x.Theme).FirstOrDefault().Id,
                ThemeTitle = question.QuestionThemes?.Select(x => x.Theme).FirstOrDefault()?.Title,
                Answers = AnswerMapper.ToListAnswerDto(question.QuestionAnswers?.Select(x => x.Answer).ToList())
            };

            return questionResponse;
        }

        public static EditQuestionRequest ToEditQuestionRequest(this Question question)
        {
            EditQuestionRequest editQuestionRequest = new EditQuestionRequest()
            {
                Id = question.Id,
                Title = question.Title,
                Explanation = question.Explanation,
                ImageUrl = UrlHelper.GetUri(question.Image),
                Answers = AnswerMapper.ToListUpdateAnswerRequest(question.QuestionAnswers.Select(x => x.Answer).ToList())
            };
            return editQuestionRequest;
        }

        public static EditQuestionRequest ToEditQuestionRequest(this QuestionResponse question)
        {
            EditQuestionRequest editQuestionRequest = new EditQuestionRequest()
            {
                Id = question.Id,
                Title = question.Title,
                Explanation = question.Explanation,
                ImageUrl = question.ImageUrl,
                ThemeId = question.ThemeId,
                Answers = AnswerMapper.ToListUpdateAnswerRequest(question.Answers)
            };
            return editQuestionRequest;
        }
    }
}
