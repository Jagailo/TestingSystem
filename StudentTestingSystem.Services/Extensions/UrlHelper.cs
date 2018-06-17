using System;

namespace StudentTestingSystem.Services.Extensions
{
    public static class UrlHelper
    {
        private const string BaseContentFolder = "Uploads";
        private const string PhotoContainerName = "Photo";

        public static string GetUri(string fileName)
        {
            return !String.IsNullOrEmpty(fileName) ? $"/{BaseContentFolder}/{PhotoContainerName}/{fileName}" : null;
        }
    }
}
