using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.Framework.Configuration;
using Microsoft.AspNet.Hosting;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.DependencyInjection;
using Fortes.DAL;
using Microsoft.Data.Entity;
using Microsoft.Framework.Logging;

namespace Fortes.Testes
{
    public class Startup
    {

        public static IConfiguration Configuration { get; set; }
        public Startup(IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<DataContext>(options =>
                {
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]);
                });
            services.AddScoped<ICategoriaDAL, CategoriaDAL>();
            services.AddScoped<IDespesaDAL, DespesaDAL>();
            services.AddScoped<IReceitaDAL, ReceitaDAL>();
        }

        public void Configure(IApplicationBuilder app)
        {

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
