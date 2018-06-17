namespace StudentTestingSystem.Domain.Infrastructure
{
    public interface IBaseEntity<T> : IEntity
    {
        T Id { get; set; }
    }
}