using StudentTestingSystem.Domain.Models.Theme;
using StudentTestingSystem.Services.TransportModels.Admin.Theme.Request;
using StudentTestingSystem.Services.TransportModels.Admin.Theme.Response;

namespace StudentTestingSystem.Services.Extensions
{
    public static class ThemeMapper
    {
        public static ThemeResponse ToThemeResponse(this Theme theme)
        {
            ThemeResponse themeResponse = new ThemeResponse()
            {
                Id = theme.Id,
                Title = theme.Title,
                SubjectId = theme.SubjectId,
                SubjectTitle = theme.Subject.Title,
                TimeLine = theme.TimeLine,
                QuestionsCount = theme.QuestionThemes.Count
            };
            return themeResponse;
        }

        public static EditThemeRequest ToEditThemeRequest(this ThemeResponse themeResponse)
        {
            EditThemeRequest editThemeRequest = new EditThemeRequest()
            {
                Id = themeResponse.Id,
                SubjectId = themeResponse.SubjectId,
                Title = themeResponse.Title,
                TimeLine = themeResponse.TimeLine
            };
            return editThemeRequest;
        }
    }
}