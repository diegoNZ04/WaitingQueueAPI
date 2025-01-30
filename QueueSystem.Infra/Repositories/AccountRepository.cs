using Microsoft.EntityFrameworkCore;
using QueueSystem.Domain.Entities;
using QueueSystem.Domain.Entities.Interfaces;
using QueueSystem.Infra.Data;

namespace QueueSystem.Infra.Repositories
{
    public class AccountRepository : GenericRepository<User>, IAccountRepository
    {
        public AccountRepository(ApplicationContext context) : base(context) { }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}