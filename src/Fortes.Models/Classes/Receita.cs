using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Fortes.Models
{
    public class Receita : BaseEntity
    {
        [Required(ErrorMessage = "Valor é um campo obrigatório.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Data é um campo obrigatório.")]
        public DateTime Data { get; set; }

        public string Observacao { get; set; }

        public string CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public virtual Categoria Categoria { get; set; }
    }
}
