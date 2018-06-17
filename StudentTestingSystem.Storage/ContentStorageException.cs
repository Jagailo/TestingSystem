using System;

namespace StudentTestingSystem.Storage
{
    public class ContentStorageException : Exception
    {
        public ContentStorageException(string message)
            : base(message)
        {
        }

        public ContentStorageException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
