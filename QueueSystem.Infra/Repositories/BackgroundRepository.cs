using QueueSystem.Domain.Entities;
using QueueSystem.Domain.Entities.Interfaces;
using QueueSystem.Infra.Data;

namespace QueueSystem.Infra.Repositories
{
    public class BackgroundRepository : IBackgroundRepository
    {
        private readonly ApplicationContext _context;
        public BackgroundRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Background background)
        {
            await _context.Backgrounds.AddAsync(background);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var background = await _context.Backgrounds.FindAsync(id);
            if (background != null)
            {
                _context.Backgrounds.Remove(background);
                await _context.SaveChangesAsync();
            }
        }

        public Task UpdateAsync(Background background)
        {
            throw new NotImplementedException();
        }
    }
}