using Fortes.DAL;
using Fortes.Models;
using Fortes.Web.Controllers;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Fortes.Testes
{
    public class CategoriaTest : BaseTest
    {
        ICategoriaDAL _categoriaDAL;
        CategoriasController _categoriasController;
        public CategoriaTest()
        {
            _categoriaDAL = new CategoriaDAL(new DataContext(ConnectionString));
            _categoriasController = new CategoriasController(_categoriaDAL);
        }

        [Fact]
        public void Find()
        {
            string id = Guid.NewGuid().ToString();
            Categoria model = new Categoria() { Id = id, Nome = "teste" };

            _categoriaDAL.Insert(model);
            _categoriaDAL.SaveChanges();

            Assert.Equal(model, _categoriaDAL.Find(id));

            _categoriaDAL.Delete(obj => obj.Id.Equals(id));
            _categoriaDAL.SaveChanges();
        }

        [Fact]
        public void Insert()
        {
            string id = Guid.NewGuid().ToString();
            Categoria model = new Categoria() { Id = id, Nome = "teste" };
            _categoriaDAL.Insert(model);
            Assert.Equal(_categoriaDAL.SaveChanges(), 1);

            _categoriaDAL.Delete(obj => obj.Id.Equals(id));
            _categoriaDAL.SaveChanges();
        }

        [Fact]
        public void Update()
        {
            string id = Guid.NewGuid().ToString();
            Categoria model = new Categoria() { Id = id, Nome = "teste" };
            _categoriaDAL.Insert(model);
            _categoriaDAL.SaveChanges();

            model.Nome = "teste atualizado.";
            _categoriaDAL.Update(model);
            Assert.Equal(_categoriaDAL.SaveChanges(), 1);

            _categoriaDAL.Delete(obj => obj.Id.Equals(id));
            _categoriaDAL.SaveChanges();
        }

        [Fact]
        public void Delete()
        {
            string id = Guid.NewGuid().ToString();
            Categoria model = new Categoria() { Id = id, Nome = "teste" };
            _categoriaDAL.Insert(model);
            _categoriaDAL.SaveChanges();


            _categoriaDAL.Delete(obj => obj.Id.Equals(id));
            Assert.Equal(_categoriaDAL.SaveChanges(), 1);
        }

        [Theory]
        [InlineData("teste")]
        public void Delete(string nome)
        {
            string id = Guid.NewGuid().ToString();
            Categoria model = new Categoria() { Id = id, Nome = nome };
            _categoriaDAL.Insert(model);
            _categoriaDAL.SaveChanges();

            Expression<Func<Categoria, bool>> predicate = obj => obj.Id.Equals(id);
            _categoriaDAL.Delete(predicate);
            Assert.Equal(_categoriaDAL.SaveChanges(), 1);
        }


        [Fact]
        public void Incluir()
        {
            string id = Guid.NewGuid().ToString();
            Categoria model = new Categoria() { Id = id, Nome = "teste" };
            JsonResult resultado = (JsonResult)_categoriasController.Incluir(model);
            Assert.Equal(resultado.ObterPropriedadeJson<string>("status"), "OK");
            _categoriaDAL.Delete(obj => obj.Id.Equals(id));
            _categoriaDAL.SaveChanges();
        }

        //[Fact]
        //public void Editar()
        //{
        //    string id = Guid.NewGuid().ToString();
        //    Categoria model = new Categoria() { Id = id, Nome = "teste" };
        //    _categoriaDAL.Insert(model);
        //    _categoriaDAL.SaveChanges();

        //    model.Nome = "teste atualizado.";
        //    JsonResult resultado = (JsonResult)_categoriasController.Editar(model);
        //    Assert.Equal(resultado.ObterPropriedadeJson<string>("status"), "OK");
        //    _categoriaDAL.Delete(obj => obj.Id.Equals(id));
        //    _categoriaDAL.SaveChanges();
        //}

        //[Fact]
        //public void Excluir()
        //{
        //    string id = Guid.NewGuid().ToString();
        //    Categoria model = new Categoria() { Id = id, Nome = "teste" };
        //    _categoriaDAL.Insert(model);
        //    _categoriaDAL.SaveChanges();

        //    JsonResult resultado = (JsonResult)_categoriasController.Excluir(id);
        //    Assert.Equal(resultado.ObterPropriedadeJson<string>("status"), "OK");
        //}

        //public void Lista()
        //{
        //    _categoriasController.Lista(null);
        //}
    }
}
