using StudentTestingSystem.Domain.Models.Question;
using StudentTestingSystem.Services.TransportModels.Admin.Result.Response;
using System;
using System.Collections.Generic;

namespace StudentTestingSystem.Services.Extensions
{
    public static class ResultMapper
    {
        public static ProfileResultsResponse ToProfileResultsResponse(this Result result)
        {
            var resultResponse = new ProfileResultsResponse();
            if (result != null)
            {
                resultResponse.Id = result.Id;
                resultResponse.AllQuestionsCount = result.AllQuestionsCount;
                resultResponse.CorrectAnswersCount = result.CorrectAnswersCount;
                resultResponse.GroupId = result.Student?.GroupId;
                resultResponse.Created = result.Created;
                resultResponse.Number = result.Student?.Group?.Number;
                resultResponse.ThemeId = result.ThemeId;
                resultResponse.ThemeTitle = result.Theme?.Title;
                resultResponse.SubjectId = result.Theme?.Subject?.Id;
                resultResponse.Subject = result.Theme?.Subject?.Title;
                resultResponse.Mark = Convert.ToInt32(Math.Round((double) result.CorrectAnswersCount / result.AllQuestionsCount * 10, MidpointRounding.AwayFromZero));
            }

            return resultResponse;
        }

        public static ThemeResultsResponse ToThemeResultsResponse(this Result result)
        {
            var resultResponse = new ThemeResultsResponse();
            if (result != null)
            {
                resultResponse.Id = result.Id;
                resultResponse.AllQuestionsCount = result.AllQuestionsCount;
                resultResponse.CorrectAnswersCount = result.CorrectAnswersCount;
                resultResponse.FullName = result.Student?.FirstName + " " + result.Student?.LastName;
                resultResponse.Created = result.Created;
                resultResponse.UserProfileId = result.UserProfileId;
                resultResponse.SubjectId = result.Theme?.SubjectId;
                resultResponse.Subject = result.Theme?.Subject?.Title;
                resultResponse.ThemeId = result.ThemeId;
                resultResponse.Theme = result.Theme?.Title;
                resultResponse.Mark = Convert.ToInt32(Math.Round((double)result.CorrectAnswersCount / result.AllQuestionsCount * 10, MidpointRounding.AwayFromZero));
            }

            return resultResponse;
        }

        public static SubjectThemeResultsResponse ToSubjectThemeResultsResponse(this Result result)
        {
            var resultResponse = new SubjectThemeResultsResponse();
            if (result != null)
            {
                resultResponse.Id = result.Id;
                resultResponse.AllQuestionsCount = result.AllQuestionsCount;
                resultResponse.CorrectAnswersCount = result.CorrectAnswersCount;
                resultResponse.FullName = result.Student?.FirstName + " " + result.Student?.LastName;
                resultResponse.Created = result.Created;
                resultResponse.UserProfileId = result.UserProfileId;
                resultResponse.Group = result.Student?.Group?.Number;
                resultResponse.GroupId = result.Student?.GroupId;
                resultResponse.Mark = Convert.ToInt32(Math.Round((double)result.CorrectAnswersCount / result.AllQuestionsCount * 10, MidpointRounding.AwayFromZero));
            }

            return resultResponse;
        }

        public static GroupResultsResponse ToGroupResultsResponse(this Result result)
        {
            var resultResponse = new GroupResultsResponse();
            if (result != null)
            {
                resultResponse.ThemeId = result.ThemeId;
                resultResponse.Theme = result.Theme?.Title;
                resultResponse.SubjectId = result.Theme?.SubjectId;
                resultResponse.Subject = result.Theme?.Subject?.Title;
            }

            return resultResponse;
        }

        public static ResultResponse ToResultResponse(this Result result, List<string> directions)
        {
            var resultResponse = new ResultResponse();
            if (result != null)
            {
                resultResponse.Id = result.Id;
                resultResponse.AllQuestionsCount = result.AllQuestionsCount;
                resultResponse.CorrectAnswersCount = result.CorrectAnswersCount;
                resultResponse.Created = result.Created;
                resultResponse.Directions = directions;
                resultResponse.ThemeId = result.ThemeId;
                resultResponse.UserProfileId = result.UserProfileId;
            }

            return resultResponse;
        }
    }
}