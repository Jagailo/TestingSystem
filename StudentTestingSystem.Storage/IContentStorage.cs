using System.Threading.Tasks;

namespace StudentTestingSystem.Storage
{
    public interface IContentStorage
    {
        Task SaveContentAsync(string fileName, byte[] content, string container);
        Task DeleteContentAsync(string fileName, string container);
        Task<byte[]> GetContetAsync(string fileName, string container);
        bool ContentExists(string fileName, string container);
    }
}
