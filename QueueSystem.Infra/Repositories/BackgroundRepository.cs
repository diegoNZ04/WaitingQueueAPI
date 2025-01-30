using QueueSystem.Domain.Entities;
using QueueSystem.Infra.Data;
using QueueSystem.Domain.Entities.Interfaces;

namespace QueueSystem.Infra.Repositories
{
    public class BackgroundRepository : GenericRepository<Background>, IBackgroundRepository
    {
        public BackgroundRepository(ApplicationContext context) : base(context) { }
    }
}