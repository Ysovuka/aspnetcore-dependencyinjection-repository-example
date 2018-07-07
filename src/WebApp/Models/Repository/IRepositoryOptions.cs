namespace WebApp.Models.Repository
{
    public interface IRepositoryOptions
    {
        string ConnectionString { get; }
        string DatabaseName { get; }
        bool Migrate { get; }
        bool UseInMemoryDatabase { get; }
    }
}