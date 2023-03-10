using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ynventory.Data
{
    public class YnventoryDbContextFactory : IDesignTimeDbContextFactory<YnventoryDbContext>
    {
        public YnventoryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<YnventoryDbContext>();
            optionsBuilder.UseNpgsql("Data Source=ynventory; Username=ynventory; Password=password");
            return new YnventoryDbContext(optionsBuilder.Options);
        }
    }
}
