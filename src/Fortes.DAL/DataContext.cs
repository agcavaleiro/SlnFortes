using Fortes.Models;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

namespace Fortes.DAL
{
    public class DataContext : DbContext
    {
        public string ConnectionString { get; private set; }

        public DbSet<Categoria> DbSetCategoria { get; set; }
        public DbSet<Despesa> DbSetDespesa { get; set; }
        public DbSet<Receita> DbSetReceita { get; set; }

        public DataContext() { }
        public DataContext(string connectionString)
        {
            ConnectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(ConnectionString))
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }


    }
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("../Fortes.Web/config.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<DataContext>(options =>
                {
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]);
                });
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
        }
    }
}
