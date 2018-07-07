using StudentTestingSystem.Domain.Infrastructure;

namespace StudentTestingSystem.Services.Services
{
    public class BaseService
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
