using Microsoft.EntityFrameworkCore;
using Ynventory.Backend.Services.Infrastructure;
using Ynventory.Data;

namespace Ynventory.Backend.ServiceImplementations.Infrastructure
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly YnventoryDbContext _context;

        public DatabaseInitializer(YnventoryDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();
        }
    }
}
