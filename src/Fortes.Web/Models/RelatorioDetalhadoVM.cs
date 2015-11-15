using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fortes.Web.Models
{
    public class RelatorioDetalhadoVM
    {
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
        public string Categoria { get; set; }
        public DateTime Data { get; set; }
        public string Observacao { get; set; }
    }
}
