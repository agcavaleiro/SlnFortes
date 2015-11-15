using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Fortes.DAL;
using Fortes.Models;
using System.Linq.Expressions;
using Fortes.Web.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Fortes.Web.Controllers
{
    public class DespesasController : BaseController
    {
        private JsonResult _jsonResult;
        private IDespesaDAL _DespesaDAL;

        public DespesasController(IDespesaDAL DespesaDAL)
        {
            _DespesaDAL = DespesaDAL;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Incluir([FromBody]DespesaVM modelVM)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var model = new Despesa();
                    model.Id = Guid.NewGuid().ToString();
                    model.CategoriaId = modelVM.CategoriaId;
                    model.Data = modelVM.Data.Value;
                    model.Observacao = modelVM.Observacao;
                    model.Valor = modelVM.Valor.Value;
                    _DespesaDAL.Insert(model);
                    _DespesaDAL.SaveChanges();
                    _jsonResult = Json(new { status = "OK", resultado = "Registro adicionado com sucesso!" });
                }
                else
                {
                    var erros = "";
                    foreach (var erro in ModelState.Values.SelectMany(sel => sel.Errors))
                    {
                        erros += $"{ erro.Exception?.Message ?? erro.ErrorMessage}<br/>";
                    }
                    _jsonResult = Json(new { status = "FALHA", resultado = erros });
                }
            }
            catch (Exception ex)
            {
                _jsonResult = Json(new { status = "FALHA", resultado = ex.Message });
            };
            return _jsonResult;
        }

        [HttpPost]
        public IActionResult Editar([FromBody]DespesaVM modelVM)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var model = new Despesa();
                    model.Id = modelVM.Id;
                    model.CategoriaId = modelVM.CategoriaId;
                    model.Data = modelVM.Data.Value;
                    model.Observacao = modelVM.Observacao;
                    model.Valor = modelVM.Valor.Value;
                    _DespesaDAL.Update(model);
                    _DespesaDAL.SaveChanges();
                    _jsonResult = Json(new { status = "OK", resultado = "Registro alterado com sucesso!" });
                }
                else
                {
                    var erros = "";
                    foreach (var erro in ModelState.Values.SelectMany(sel => sel.Errors))
                    {
                        erros += $"{ erro.Exception?.Message ?? erro.ErrorMessage}<br/>";
                    }
                    _jsonResult = Json(new { status = "FALHA", resultado = erros });
                }
            }
            catch (Exception ex)
            {
                _jsonResult = Json(new { status = "FALHA", resultado = ex.Message });
            };
            return _jsonResult;
        }
        
        public IActionResult Excluir(string id)
        {
            try
            {
                _DespesaDAL.Delete(obj => obj.Id.Equals(id));
                _DespesaDAL.SaveChanges();
                _jsonResult = Json(new { status = "OK", resultado = "Registro excluído com sucesso!" });
            }
            catch (Exception ex)
            {
                _jsonResult = Json(new { status = "FALHA", resultado = ex.Message });
            };
            return _jsonResult;
        }
        
        public JsonResult Lista(string data, string categoriaId)
        {
            try
            {
                Expression<Func<Despesa, bool>> expressao = null;
                if (!string.IsNullOrEmpty(data))
                {
                    DateTime expressaoData = DateTime.Parse(data);
                    expressao = obj => obj.Data.Equals(expressaoData);
                }
                if (!string.IsNullOrEmpty(categoriaId))
                {
                    expressao = expressao == null ? obj => obj.CategoriaId.Equals(categoriaId) : expressao.And(obj => obj.CategoriaId.Equals(categoriaId));
                }
                var lista = expressao == null ? _DespesaDAL.Get.OrderBy(obj => obj.Data)
                    .Select(sel => new DespesaVM
                    {
                        CategoriaId = sel.Categoria.Id,
                        CategoriaNome = sel.Categoria.Nome,
                        Data = sel.Data,
                        Id = sel.Id,
                        Observacao = sel.Observacao,
                        Valor = sel.Valor
                    })
                    .ToList() : _DespesaDAL.Get.Where(expressao).OrderBy(obj => obj.Data)
                    .Select(sel => new DespesaVM
                    {
                        CategoriaId = sel.Categoria.Id,
                        CategoriaNome = sel.Categoria.Nome,
                        Data = sel.Data,
                        Id = sel.Id,
                        Observacao = sel.Observacao,
                        Valor = sel.Valor
                    })
                    .ToList<DespesaVM>();

                _jsonResult = Json(new { status = "OK", resultado = lista });

            }
            catch (Exception ex)
            {
                _jsonResult = Json(new { status = "FALHA", resultado = ex.Message });
            };
            return _jsonResult;
        }
    }
}
