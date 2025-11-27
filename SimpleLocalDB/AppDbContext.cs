using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SimpleLocalDB
{
    public abstract class AppDbContext : DbContext
    {
        public static readonly string BasePath = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;

        static bool alreadyWiped = false;

        IConfiguration config { get; }

        protected AppDbContext(IConfiguration config) : base()
        {
            this.config = config;
            const string RunInDevEnvironment = nameof(RunInDevEnvironment);
            if (!alreadyWiped)
            {
                if (bool.Parse(config[RunInDevEnvironment] ?? throw new MissingConfigFileFieldException(RunInDevEnvironment)))
                {
                    Database.EnsureDeleted();
                }
                alreadyWiped = true;
            }
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string UseConnectionString = nameof(UseConnectionString);
            optionsBuilder.UseSqlServer(config.GetConnectionString(config[UseConnectionString] ?? throw new MissingConfigFileFieldException(UseConnectionString)));
        }
    }

    public class AppDbContext<T>(IConfiguration config) : AppDbContext(config) where T : class
    {
        public DbSet<T> values { get; private set; }
    }
}
