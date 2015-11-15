using Fortes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fortes.DAL
{
    public class ReceitaDAL : BaseDAL<Receita>, IReceitaDAL
    {
        public ReceitaDAL(DataContext dataContext) : base(dataContext) { }
    }
    public interface IReceitaDAL : IBaseDAL<Receita>
    {

    }
}
