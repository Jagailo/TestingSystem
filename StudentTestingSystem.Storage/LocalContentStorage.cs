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

        public async Task DeleteContentAsync(string fileName, string container = "Photo")
        {
            var contentFolder = Path.Combine(_contentFolder, container);
            string filepath = Path.Combine(contentFolder, fileName);
            await Task.Run(() => { File.Delete(filepath); });
        }

        public async Task<byte[]> GetContetAsync(string fileName, string container = "Photo")
        {
            try
            {
                var contentFolder = Path.Combine(_contentFolder, container);
                string filepath = Path.Combine(contentFolder, fileName);

                byte[] result;
                using (FileStream sourceStream = File.Open(filepath, FileMode.Open))
                {
                    result = new byte[sourceStream.Length];
                    await sourceStream.ReadAsync(result, 0, (int)sourceStream.Length);
                }
                return result;
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
