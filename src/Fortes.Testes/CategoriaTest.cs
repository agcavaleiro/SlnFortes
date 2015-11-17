using Fortes.DAL;
using Fortes.Models;
using Fortes.Web.Controllers;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.AspNet.Mvc.ViewFeatures;
using Microsoft.AspNet.TestHost;
using Microsoft.Framework.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Fortes.Testes
{
    public class CategoriaTest : BaseTest
    {
        ICategoriaDAL _categoriaDAL;
        public CategoriaTest()
        {
            _categoriaDAL = new CategoriaDAL(new DataContext(Startup.Configuration["Data:DefaultConnection:ConnectionString"]));
        }


        [Fact]
        public async Task Incluir()
        {
            Categoria model = new Categoria();
            model.Nome = $"Teste - {model.Id}";

            var stringContent = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost/categorias/incluir", stringContent);
            var strObj = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<dynamic>(strObj);
            Assert.Equal(resultado.status.ToString(), "OK");
        }

        [Fact]
        public async Task Editar()
        {

            Categoria model = new Categoria();
            model.Id = Guid.NewGuid().ToString();
            model.Nome = $"Teste - {model.Id}";

            _categoriaDAL.Insert(model);
            _categoriaDAL.SaveChanges();

            model.Nome = $"Teste - {model.Id} - Atualizado";

            var stringContent = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost/categorias/editar", stringContent);
            var strObj = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<dynamic>(strObj);
            Assert.Equal(resultado.status.ToString(), "OK");
        }

        [Fact]
        public async Task Excluir()
        {

            Categoria model = new Categoria();
            model.Id = Guid.NewGuid().ToString();
            model.Nome = $"Teste - {model.Id}";

            _categoriaDAL.Insert(model);
            _categoriaDAL.SaveChanges();

            string id = model.Id;
            var response = await _httpClient.GetAsync($"http://localhost/categorias/excluir/{id}");
            var strObj = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<dynamic>(strObj);
            Assert.Equal(resultado.status.ToString(), "OK");
        }

        [Fact]
        public async Task Lista()
        {
            var response = await _httpClient.GetAsync("http://localhost/categorias/lista");
            var strObj = await response.Content.ReadAsStringAsync();
            var resultado = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(strObj);
            Assert.Equal(resultado.status.ToString(), "OK");
        }
    }
}
