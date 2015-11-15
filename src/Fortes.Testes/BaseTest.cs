using Fortes.DAL;
using Fortes.Models;
using Microsoft.Framework.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Fortes.Testes
{
    public class BaseTest
    {
        protected string ConnectionString { get; private set; }
        public BaseTest()
        {
           var builder = new ConfigurationBuilder();
            builder.AddJsonFile("../Fortes.Web/config.json");
            var config = builder.Build();
            ConnectionString = config.Get<string>("Data:DefaultConnection:ConnectionString");
        }

    }
}
