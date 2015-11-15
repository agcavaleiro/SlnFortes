using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fortes.Models
{
    public class Categoria : BaseEntity
    {
        [Required(ErrorMessage = "Nome é um campo obrigatório.")]
        public string Nome { get; set; }

        public virtual ICollection<Despesa> Despesas { get; set; } = new HashSet<Despesa>();
        public virtual ICollection<Receita> Receitas { get; set; } = new HashSet<Receita>();
    }
}
