using Fortes.DAL;
using Fortes.Models;
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
        public CategoriaTest()
        {
            _categoriaDAL = new CategoriaDAL(new DataContext(ConnectionString));
        }

        [Theory]
        [InlineData("1")]
        public Categoria Find(string id)
        {
            return _categoriaDAL.Find(id);
        }

        [Fact]
        public void Insert()
        {
            Categoria model = new Categoria() { Id = "teste - " + Guid.NewGuid().ToString(), Nome = "teste" };
            _categoriaDAL.Insert(model);
        }

        [Fact]
        public void Update()
        {
            Categoria model = _categoriaDAL.Get.FirstOrDefault(obj => obj.Id.StartsWith("teste"));
            if (model != null)
                _categoriaDAL.Update(model);
        }

        [Fact]
        public void Delete()
        {
            Categoria model = _categoriaDAL.Get.FirstOrDefault(obj => obj.Id.StartsWith("teste"));
            if (model != null)
                _categoriaDAL.Delete(model);
        }

        [Theory]
        [InlineData("teste")]
        public void Delete(string teste)
        {
            Expression<Func<Categoria, bool>> predicate = obj => obj.Id.StartsWith(teste);
            _categoriaDAL.Delete(predicate);
        }
    }
}
