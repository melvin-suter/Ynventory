namespace Ynventory.Backend.Services.Infrastructure
{
    public interface IDatabaseInitializer
    {
        public Task SeedAsync();
    }
}
