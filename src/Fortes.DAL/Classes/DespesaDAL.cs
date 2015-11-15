using Fortes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fortes.DAL
{
    public class DespesaDAL : BaseDAL<Despesa>, IDespesaDAL
    {
        public DespesaDAL(DataContext dataContext) : base(dataContext) { }
    }
    public interface IDespesaDAL : IBaseDAL<Despesa>
    {

    }
}
