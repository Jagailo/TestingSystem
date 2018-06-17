using System;
using System.IO;

namespace StudentTestingSystem.Services.TransportModels.Admin.Question.Request
{
    public class UploadQuestionAttachmentRequest
    {
        public UploadQuestionAttachmentRequest(Guid questionId, byte[] image, string extention)
        {
            QuestionId = questionId;
            Image = image;
            Extention = extention;
        }
        public Guid QuestionId { get; set; }
        public byte[] Image { get; set; }
        public string Extention { get; set; }
    }
}
