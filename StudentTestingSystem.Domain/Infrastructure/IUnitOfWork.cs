using System.Threading.Tasks;

namespace StudentTestingSystem.Domain.Infrastructure
{
    public interface IUnitOfWork : IRepositoryFactory
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}