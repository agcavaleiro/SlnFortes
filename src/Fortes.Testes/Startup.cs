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
    //public class Startup
    //{
    //    public static Microsoft.Framework.ConfigurationModel.IConfiguration Configuration { get; set; }
    //    //public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
    //    //{
    //    //    var builder = new ConfigurationBuilder()
    //    //        .SetBasePath(appEnv.ApplicationBasePath)
    //    //        .AddJsonFile("config.json")
    //    //        .AddEnvironmentVariables();
    //    //    Configuration = builder.Build();
    //    //}
    //    //public void ConfigureServices(IServiceCollection services)
    //    //{
    //    //    services.AddEntityFramework()
    //    //        .AddSqlServer()
    //    //        .AddDbContext<DataContext>(options =>
    //    //        {
    //    //            options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]);
    //    //        });
    //    //}
    //    public void Configure(IApplicationBuilder app)
    //    {
    //        var config = new Configuration();
    //        config.Get("config.json");
    //    }
    //}
}
