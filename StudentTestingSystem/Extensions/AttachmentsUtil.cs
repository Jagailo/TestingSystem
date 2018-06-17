using System;
using System.IO;

namespace StudentTestingSystem.Extensions
{
    public static class AttachmentsUtil
    {
        public static string GetExtentionFromMimeType(string mimeType)
        {
            switch (mimeType)
            {
                case "image/jpeg": return "jpg";
                case "image/png": return "png";
                case "video/mp4": return "mp4";
                default: return null;
            }
        }
    }
}