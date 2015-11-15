using Fortes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fortes.DAL
{
    public class CategoriaDAL : BaseDAL<Categoria>, ICategoriaDAL
    {
        public CategoriaDAL(DataContext dataContext) : base(dataContext) { }
    }
    public interface ICategoriaDAL : IBaseDAL<Categoria>
    {
    }
}
