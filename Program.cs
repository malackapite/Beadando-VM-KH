using Microsoft.Extensions.Configuration;
using SimpleLocalDB;

namespace Beadando_VM_KH
{
    internal class Program
    {
        static readonly IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(AppDbContext.BasePath)
            .AddJsonFile("appconfig.json", false, true)
            .Build()
        ;

        static void Main(string[] args)
        {
            
        }
    }
}
