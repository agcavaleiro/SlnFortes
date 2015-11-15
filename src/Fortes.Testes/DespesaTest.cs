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
    public class DespesaTest : BaseTest
    {
        IDespesaDAL _DespesaDAL;
        public DespesaTest()
        {
            _DespesaDAL = new DespesaDAL(new DataContext(ConnectionString));
        }

        [Theory]
        [InlineData("1")]
        public Despesa Find(string id)
        {
            return _DespesaDAL.Find(id);
        }

        [Fact]
        public void Insert()
        {
            Despesa model = new Despesa() { Id = "teste - " + Guid.NewGuid().ToString(), Observacao = "teste", Data = DateTime.Now, Valor = 10 };
            _DespesaDAL.Insert(model);
        }

        [Fact]
        public void Update()
        {
            Despesa model = _DespesaDAL.Get.FirstOrDefault(obj => obj.Id.StartsWith("teste"));
            if (model != null)
                _DespesaDAL.Update(model);
        }

        [Fact]
        public void Delete()
        {
            Despesa model = _DespesaDAL.Get.FirstOrDefault(obj => obj.Id.StartsWith("teste"));
            if (model != null)
                _DespesaDAL.Delete(model);
        }

        [Theory]
        [InlineData("teste")]
        public void Delete(string teste)
        {
            Expression<Func<Despesa, bool>> predicate = obj => obj.Id.StartsWith(teste);
            _DespesaDAL.Delete(predicate);
        }
    }
}
