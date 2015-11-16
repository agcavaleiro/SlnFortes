using Fortes.DAL;
using Fortes.Models;
using Microsoft.AspNet.Mvc;
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
    public static class EnumerableExtensions
    { 
        public static TSource ObterPropriedadeJson<TSource>(this JsonResult json, string nomePropriedade)
        {
            var propertyInfo = json.Value.GetType().GetProperty(nomePropriedade);
            var reflectedValue = (TSource)propertyInfo.GetValue(json.Value, null);
            return reflectedValue;

        }
    }
}
