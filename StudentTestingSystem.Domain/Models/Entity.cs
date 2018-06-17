using StudentTestingSystem.Domain.Infrastructure;

namespace StudentTestingSystem.Domain.Models
{
    public class Entity<T> : IBaseEntity<T>
    {
        public virtual T Id { get; set; }
    }
}