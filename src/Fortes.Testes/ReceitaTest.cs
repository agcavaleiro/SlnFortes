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
    public class ReceitaTest : BaseTest
    {
        IReceitaDAL _ReceitaDAL;
        public ReceitaTest()
        {
            _ReceitaDAL = new ReceitaDAL(new DataContext(ConnectionString));
        }

        [Theory]
        [InlineData("1")]
        public Receita Find(string id)
        {
            return _ReceitaDAL.Find(id);
        }

        [Fact]
        public void Insert()
        {
            Receita model = new Receita() { Id = "teste - " + Guid.NewGuid().ToString(), Observacao = "teste", Data = DateTime.Now, Valor = 10 };
            _ReceitaDAL.Insert(model);
        }

        [Fact]
        public void Update()
        {
            Receita model = _ReceitaDAL.Get.FirstOrDefault(obj => obj.Id.StartsWith("teste"));
            if (model != null)
                _ReceitaDAL.Update(model);
        }

        [Fact]
        public void Delete()
        {
            Receita model = _ReceitaDAL.Get.FirstOrDefault(obj => obj.Id.StartsWith("teste"));
            if (model != null)
                _ReceitaDAL.Delete(model);
        }

        [Theory]
        [InlineData("teste")]
        public void Delete(string teste)
        {
            Expression<Func<Receita, bool>> predicate = obj => obj.Id.StartsWith(teste);
            _ReceitaDAL.Delete(predicate);
        }
    }
}
