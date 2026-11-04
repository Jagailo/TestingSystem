using System;
using System.IO;
using System.Threading.Tasks;

namespace StudentTestingSystem.Storage
{
    public class LocalContentStorage : IContentStorage
    {
        private readonly string _contentFolder;

        public LocalContentStorage(string baseContentFolder = "Uploads")
        {
            _contentFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, baseContentFolder);
        }

        public async Task SaveContentAsync(string fileName, byte[] content, string container)
        { 
            try
            {
                var contentFolder = Path.Combine(_contentFolder, container);
                if (!Directory.Exists(contentFolder))
                {
                    Directory.CreateDirectory(contentFolder);
                }

                string filepath = Path.Combine(contentFolder, fileName);
                using (var sourceStream = File.Create(filepath))
                {
                    await sourceStream.WriteAsync(content, 0, content.Length);
                }
            }
            catch (Exception e)
            {
                throw new ContentStorageException("Error saving content", e);
            }
        }

        public Task DeleteContentAsync(string fileName, string container = "Photo")
        {
            try
            {
                var contentFolder = Path.Combine(_contentFolder, container);
                string filepath = Path.Combine(contentFolder, fileName);
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                return Task.FromException(new ContentStorageException("Error deleting content", e));
            }
        }

        public async Task<byte[]> GetContentAsync(string fileName, string container = "Photo")
        {
            try
            {
                var contentFolder = Path.Combine(_contentFolder, container);
                string filepath = Path.Combine(contentFolder, fileName);

                using (FileStream sourceStream = File.Open(filepath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var targetStream = new MemoryStream())
                {
                    // ReadAsync is allowed to return fewer bytes than requested, so the stream is copied
                    await sourceStream.CopyToAsync(targetStream);
                    return targetStream.ToArray();
                }
            }
            catch (Exception e)
            {
                throw new ContentStorageException("Error getting content", e);
            }
        }

        public bool ContentExists(string fileName, string container = "Photo")
        {
            try
            {
                var contentFolder = Path.Combine(_contentFolder, container);
                string filepath = Path.Combine(contentFolder, fileName);
                return File.Exists(filepath);
            }
            catch (Exception e)
            {
                throw new ContentStorageException("Error getting content", e);
            }
        }
    }
}
