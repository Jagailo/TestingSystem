using System.Collections.Generic;
using StudentTestingSystem.Domain.Models.Answer;
using StudentTestingSystem.Services.TransportModels.Admin.Answer.Request;
using StudentTestingSystem.Services.TransportModels.Admin.Answer.Response;

namespace StudentTestingSystem.Services.Extensions
{
    public static class AnswerMapper
    {
        public static AnswerListDto ToAnswerListDto(this Answer answer)
        {
            var answerListDto = new AnswerListDto();
            if (answer != null)
            {
                answerListDto.Id = answer.Id;
                answerListDto.Created = answer.Created;
                answerListDto.IsCorrectAnswer = answer.IsCorrectAnswer;
                answerListDto.Title = answer.Title;
            }
            return answerListDto;
        }

        public static AnswerDto ToAnswerDto(this Answer answer)
        {
            var answerDto = new AnswerDto();
            if (answer != null)
            {
                answerDto.Id = answer.Id;
                answerDto.Created = answer.Created;
                answerDto.IsCorrectAnswer = answer.IsCorrectAnswer;
                answerDto.Title = answer.Title;
            }
            return answerDto;
        }

        public static UpdateAnswerRequest ToUpdateAnswerRequest(this Answer answer)
        {
            var updateAnswerRequest = new UpdateAnswerRequest();
            if (answer != null)
            {
                updateAnswerRequest.Id = answer.Id;
                updateAnswerRequest.IsCorrectAnswer = answer.IsCorrectAnswer;
                updateAnswerRequest.Title = answer.Title;
            }
            return updateAnswerRequest;
        }

        public static UpdateAnswerRequest ToUpdateAnswerRequest(this AnswerDto answer)
        {
            var updateAnswerRequest = new UpdateAnswerRequest();
            if (answer != null)
            {
                updateAnswerRequest.Id = answer.Id;
                updateAnswerRequest.IsCorrectAnswer = answer.IsCorrectAnswer;
                updateAnswerRequest.Title = answer.Title;
            }
            return updateAnswerRequest;
        }

        public static List<AnswerDto> ToListAnswerDto(List<Answer> answers)
        {
            List<AnswerDto> answerDtoList = new List<AnswerDto>();
            foreach(var answer in answers)
            {
                answerDtoList.Add(ToAnswerDto(answer));
            }
            return answerDtoList;
        }

        public static List<UpdateAnswerRequest> ToListUpdateAnswerRequest(List<Answer> answers)
        {
            List<UpdateAnswerRequest> updateAnswerRequestList = new List<UpdateAnswerRequest>();
            foreach (var answer in answers)
            {
                updateAnswerRequestList.Add(ToUpdateAnswerRequest(answer));
            }
            return updateAnswerRequestList;
        }

        public static List<UpdateAnswerRequest> ToListUpdateAnswerRequest(List<AnswerDto> answers)
        {
            List<UpdateAnswerRequest> updateAnswerRequestList = new List<UpdateAnswerRequest>();
            foreach (var answer in answers)
            {
                updateAnswerRequestList.Add(ToUpdateAnswerRequest(answer));
            }
            return updateAnswerRequestList;
        }
    }
}
