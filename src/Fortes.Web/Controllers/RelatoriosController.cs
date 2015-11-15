using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Fortes.DAL;
using Fortes.Web.Models;
using System.Linq.Expressions;
using Fortes.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Fortes.Web.Controllers
{
    public class RelatoriosController : BaseController
    {
        private JsonResult _jsonResult;
        private IDespesaDAL _despesaDAL;
        private IReceitaDAL _receitaDAL;
        
        public RelatoriosController(IDespesaDAL despesaDAL, IReceitaDAL receitaDAL)
        {
            _despesaDAL = despesaDAL;
            _receitaDAL = receitaDAL;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Detalhado(string dataInicio, string dataFim, string categoriaId)
        {
            try
            {

                Expression<Func<Despesa, bool>> expressaoDespesa = null;
                Expression<Func<Receita, bool>> expressaoReceita = null;
                if (!string.IsNullOrEmpty(dataInicio) && !string.IsNullOrEmpty(dataFim))
                {
                    DateTime expressaoDataInicio = DateTime.Parse(dataInicio);
                    DateTime expressaoDataFim = DateTime.Parse(dataFim);
                    expressaoDespesa = obj => obj.Data >= expressaoDataInicio && obj.Data <= expressaoDataFim;
                    expressaoReceita = obj => obj.Data >= expressaoDataInicio && obj.Data <= expressaoDataFim;
                }
                if (!string.IsNullOrEmpty(categoriaId))
                {
                    expressaoDespesa = expressaoDespesa == null ? obj => obj.CategoriaId.Equals(categoriaId) : expressaoDespesa.And(obj => obj.CategoriaId.Equals(categoriaId));
                    expressaoReceita = expressaoReceita == null ? obj => obj.CategoriaId.Equals(categoriaId) : expressaoReceita.And(obj => obj.CategoriaId.Equals(categoriaId));
                }
                var despesas = new List<RelatorioDetalhadoVM>();
                var receitas = new List<RelatorioDetalhadoVM>();
                if (expressaoDespesa == null && expressaoReceita == null)
                {
                    despesas = _despesaDAL.Get.Select(sel => new RelatorioDetalhadoVM
                    {
                        Tipo = "Despesa",
                        Categoria = sel.Categoria.Nome,
                        Data = sel.Data,
                        Observacao = sel.Observacao,
                        Valor = sel.Valor
                    }).ToList();
                    receitas = _receitaDAL.Get.Select(sel => new RelatorioDetalhadoVM
                    {
                        Tipo = "Receitas",
                        Categoria = sel.Categoria.Nome,
                        Data = sel.Data,
                        Observacao = sel.Observacao,
                        Valor = sel.Valor
                    }).ToList();
                }
                else
                {
                    despesas = _despesaDAL.Get.Where(expressaoDespesa).Select(sel => new RelatorioDetalhadoVM
                    {
                        Tipo = "Despesa",
                        Categoria = sel.Categoria.Nome,
                        Data = sel.Data,
                        Observacao = sel.Observacao,
                        Valor = sel.Valor
                    }).ToList();
                    receitas = _receitaDAL.Get.Where(expressaoReceita).Select(sel => new RelatorioDetalhadoVM
                    {
                        Tipo = "Receitas",
                        Categoria = sel.Categoria.Nome,
                        Data = sel.Data,
                        Observacao = sel.Observacao,
                        Valor = sel.Valor
                    }).ToList();
                }
                var lista = despesas.Concat(receitas);
                _jsonResult = Json(new { status = "OK", resultado = lista });
            }
            catch (Exception ex)
            {
                _jsonResult = Json(new { status = "FALHA", resultado = ex.Message });
            }
            return _jsonResult;
        }
        
        public IActionResult Agrupado(string dataInicio, string dataFim, string categoriaId)
        {
            try
            {

                Expression<Func<Despesa, bool>> expressaoDespesa = null;
                Expression<Func<Receita, bool>> expressaoReceita = null;
                if (!string.IsNullOrEmpty(dataInicio) && !string.IsNullOrEmpty(dataFim))
                {
                    DateTime expressaoDataInicio = DateTime.Parse(dataInicio);
                    DateTime expressaoDataFim = DateTime.Parse(dataFim);
                    expressaoDespesa = obj => obj.Data >= expressaoDataInicio && obj.Data <= expressaoDataFim;
                    expressaoReceita = obj => obj.Data >= expressaoDataInicio && obj.Data <= expressaoDataFim;
                }
                if (!string.IsNullOrEmpty(categoriaId))
                {
                    expressaoDespesa = expressaoDespesa == null ? obj => obj.CategoriaId.Equals(categoriaId) : expressaoDespesa.And(obj => obj.CategoriaId.Equals(categoriaId));
                    expressaoReceita = expressaoReceita == null ? obj => obj.CategoriaId.Equals(categoriaId) : expressaoReceita.And(obj => obj.CategoriaId.Equals(categoriaId));
                }
                var despesas = new List<RelatorioDetalhadoVM>();
                var receitas = new List<RelatorioDetalhadoVM>();
                if (expressaoDespesa == null && expressaoReceita == null)
                {
                    despesas = _despesaDAL.Get.Select(sel => new RelatorioDetalhadoVM
                    {
                        Tipo = "Despesa",
                        Categoria = sel.Categoria.Nome,
                        Data = sel.Data,
                        Observacao = sel.Observacao,
                        Valor = sel.Valor
                    }).ToList();
                    receitas = _receitaDAL.Get.Select(sel => new RelatorioDetalhadoVM
                    {
                        Tipo = "Receitas",
                        Categoria = sel.Categoria.Nome,
                        Data = sel.Data,
                        Observacao = sel.Observacao,
                        Valor = sel.Valor
                    }).ToList();
                }
                else
                {
                    despesas = _despesaDAL.Get.Where(expressaoDespesa).Select(sel => new RelatorioDetalhadoVM
                    {
                        Tipo = "Despesa",
                        Categoria = sel.Categoria.Nome,
                        Data = sel.Data,
                        Observacao = sel.Observacao,
                        Valor = sel.Valor
                    }).ToList();
                    receitas = _receitaDAL.Get.Where(expressaoReceita).Select(sel => new RelatorioDetalhadoVM
                    {
                        Tipo = "Receitas",
                        Categoria = sel.Categoria.Nome,
                        Data = sel.Data,
                        Observacao = sel.Observacao,
                        Valor = sel.Valor
                    }).ToList();
                }
                var lista = despesas.Concat(receitas).GroupBy(sel => sel.Categoria, (k, c) => new RelatorioAgrupadoVM
                {
                    Categoria = k,
                    Detalhes = c.ToList()
                }).ToList();
                _jsonResult = Json(new { status = "OK", resultado = lista });
            }
            catch (Exception ex)
            {
                _jsonResult = Json(new { status = "FALHA", resultado = ex.Message });
            }
            return _jsonResult;
        }
    }
}
