using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fortes.Web.Models
{
    public class RelatorioAgrupadoVM
    {
        public string Categoria { get; set; }

        public ICollection<RelatorioDetalhadoVM> Detalhes { get; set; }
    }
}
