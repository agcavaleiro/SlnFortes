using Fortes.DAL;
using Fortes.Models;
using Fortes.Web.Controllers;
using Fortes.Web.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.AspNet.Mvc.ViewFeatures;
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
    public class ReceitaTest : BaseTest
    {
        IReceitaDAL _receitaDAL;
        ICategoriaDAL _categoriaDAL;
        public ReceitaTest()
        {
            _receitaDAL = new ReceitaDAL(new DataContext(Startup.Configuration["Data:DefaultConnection:ConnectionString"]));
            _categoriaDAL = new CategoriaDAL(new DataContext(Startup.Configuration["Data:DefaultConnection:ConnectionString"]));
        }
        [Fact]
        public async Task Incluir()
        {
            ReceitaVM model = new ReceitaVM() { Observacao = "teste", Data = DateTime.Now, Valor = 10, CategoriaId = _categoriaDAL.Get.FirstOrDefault().Id };

            var stringContent = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost/receitas/incluir", stringContent);
            var strObj = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<dynamic>(strObj);
            Assert.Equal(resultado.status.ToString(), "OK");
        }

        [Fact]
        public async Task Editar()
        {
            string id = Guid.NewGuid().ToString();
            Receita model = new Receita() { Id = id, Observacao = "teste", Data = DateTime.Now, Valor = 10, CategoriaId = _categoriaDAL.Get.FirstOrDefault().Id };
            _receitaDAL.Insert(model);
            _receitaDAL.SaveChanges();

            ReceitaVM modelVM = new ReceitaVM();
            modelVM.Id = model.Id;
            modelVM.CategoriaId = model.CategoriaId;
            modelVM.Data = model.Data;
            modelVM.Valor = model.Valor;
            modelVM.Observacao = "teste atualizado.";
            var stringContent = new StringContent(JsonConvert.SerializeObject(modelVM), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("http://localhost/receitas/editar", stringContent);
            var strObj = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<dynamic>(strObj);
            Assert.Equal(resultado.status.ToString(), "OK");

        }

        [Fact]
        public async Task Excluir()
        {
            string id = Guid.NewGuid().ToString();
            Receita model = new Receita() { Id = id, Observacao = "teste", Data = DateTime.Now, Valor = 10, CategoriaId = _categoriaDAL.Get.FirstOrDefault().Id };
            _receitaDAL.Insert(model);
            _receitaDAL.SaveChanges();

            var response = await _httpClient.GetAsync($"http://localhost/receitas/excluir/{id}");
            var strObj = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<dynamic>(strObj);
            Assert.Equal(resultado.status.ToString(), "OK");
        }

        [Fact]
        public async Task Lista()
        {
            var response = await _httpClient.GetAsync("http://localhost/receitas/lista");
            var strObj = await response.Content.ReadAsStringAsync();
            var resultado = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(strObj);
            Assert.Equal(resultado.status.ToString(), "OK");
        }
    }
}
