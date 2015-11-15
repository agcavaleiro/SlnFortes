using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fortes.Web.Models
{
    public class DespesaVM
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Valor é um campo obrigatório.")]
        public decimal? Valor { get; set; }

        [Required(ErrorMessage = "Data é um campo obrigatório.")]
        public DateTime? Data { get; set; }

        [Required(ErrorMessage = "Observação é um campo obrigatório.")]
        public string Observacao { get; set; }

        [Required(ErrorMessage = "Categoria é um campo obrigatório.")]
        public string CategoriaId { get; set; }

        public string CategoriaNome { get; set; }
    }
}
