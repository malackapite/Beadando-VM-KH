using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SimpleLocalDB
{
    /// <summary>
    /// Instantiates an <see cref="AppDbContext"/> object with a given configuration file manager object.
    /// </summary>
    /// <param name="config">Configuration file manager.</param>
    /// <author>KeresztesHunor</author>
    public abstract class AppDbContext(IConfiguration config) : DbContext()
    {
        /// <summary>
        /// The absolute path of the directory the project solution is in.
        /// </summary>
        public static readonly string BasePath = Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;

        /// <summary>
        /// Configuration file manager object.
        /// </summary>
        IConfiguration config { get; } = config;

        /// <summary>
        /// Configures database connection from the given configuration file data.
        /// </summary>
        /// <remarks>
        /// Configures the database to use SQLServer connection.
        /// </remarks>
        /// <param name="optionsBuilder">Configuration builder object</param>
        /// <exception cref="InvalidOrMissingConfigFileFieldException">If the "UseConnectionString" field is missing from the configuration file, or its value is <see langword="null"/>.</exception>
        /// <exception cref="FormatException">If the connection string in the configuration file isn't formatted correctly for <see cref="string.Format(string, object?)"/>.</exception>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string UseConnectionString = nameof(UseConnectionString);
            string? connectionString = config.GetConnectionString(config[UseConnectionString] ?? throw new InvalidOrMissingConfigFileFieldException(UseConnectionString));
            optionsBuilder.UseSqlServer(connectionString is not null ? string.Format(connectionString, AppContext.BaseDirectory) : null);
        }
    }

    /// <summary>
    /// A database context <see langword="class"/>, which can be specified to use any model to create a database from, connect to and manipulate data in.
    /// </summary>
    /// <remarks>
    /// See <see href="https://tinyurl.com/2x3x4yrz">documentation</see> for more details.
    /// </remarks>
    /// <typeparam name="T">The model type to be used for the database.</typeparam>
    /// <author>KeresztesHunor</author>
    public class AppDbContext<T> : AppDbContext where T : class
    {
        /// <summary>
        /// Indicates that the database has already been attempted to be wiped when running in dev mode.
        /// </summary>
        /// <remarks>
        /// Is static across every instance of every unique type <see cref="AppDbContext{T}"/> is used with.
        /// </remarks>
        static bool alreadyWiped = false;

        /// <summary>
        /// Represents the table within the database.
        /// </summary>
        public DbSet<T> values { get; private set; }

        /// <summary>
        /// Instantiates an <see cref="AppDbContext{T}"/> object with a given configuration file manager object.
        /// </summary>
        /// <param name="config">Configuration file manager.</param>
        public AppDbContext(IConfiguration config) : base(config)
        {
            if (!alreadyWiped)
            {
                string? runInDevEnvironment = config["RunInDevEnvironment"];
                if (runInDevEnvironment is not null && (!bool.TryParse(runInDevEnvironment, out bool result) || result))
                {
                    Database.EnsureDeleted();
                }
                alreadyWiped = true;
            }
            Database.EnsureCreated();
        }
    }
}
