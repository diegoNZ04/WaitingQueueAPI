namespace QueueSystem.Domain.Entities.Interfaces
{
    public interface IBackgroundRepository
    {
        Task AddAsync(Background background);
        Task UpdateAsync(Background background);
        Task DeleteAsync(int id);
    }
}