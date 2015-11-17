using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.TestHost;
using Microsoft.Dnx.Runtime;
using Microsoft.Dnx.Runtime.Infrastructure;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xunit;

namespace Fortes.Testes
{
    public class BaseTest
    {
        protected readonly Action<IApplicationBuilder> _applicationBuilder;
        protected readonly Action<IServiceCollection> _serviceColletion;
        protected readonly TestServer _testServer;
        protected readonly HttpClient _httpClient;
        public BaseTest()
        {
            var environment = CallContextServiceLocator.Locator.ServiceProvider.GetRequiredService<IApplicationEnvironment>();

            var startup = new Startup(environment);
            _applicationBuilder = startup.Configure;
            _serviceColletion = startup.ConfigureServices;

            _testServer = TestServer.Create(_applicationBuilder, _serviceColletion);
            _httpClient = _testServer.CreateClient();
        }

    }
}
