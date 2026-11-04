using System;
using System.IO;

namespace StudentTestingSystem.Services.TransportModels.Admin.Question.Request
{
    public class UploadQuestionAttachmentRequest
    {
        public UploadQuestionAttachmentRequest(Guid questionId, byte[] image, string extension)
        {
            QuestionId = questionId;
            Image = image;
            Extension = extension;
        }
        public Guid QuestionId { get; set; }
        public byte[] Image { get; set; }
        public string Extension { get; set; }
    }
}
