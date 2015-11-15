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
    public class ReceitasController : BaseController
    {
        private JsonResult _jsonResult;
        private IReceitaDAL _ReceitaDAL;

        public ReceitasController(IReceitaDAL ReceitaDAL)
        {
            _ReceitaDAL = ReceitaDAL;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Incluir([FromBody]ReceitaVM modelVM)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var model = new Receita();
                    model.Id = Guid.NewGuid().ToString();
                    model.CategoriaId = modelVM.CategoriaId;
                    model.Data = modelVM.Data.Value;
                    model.Observacao = modelVM.Observacao;
                    model.Valor = modelVM.Valor.Value;
                    _ReceitaDAL.Insert(model);
                    _ReceitaDAL.SaveChanges();
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
        public IActionResult Editar([FromBody]ReceitaVM modelVM)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var model = new Receita();
                    model.Id = modelVM.Id;
                    model.CategoriaId = modelVM.CategoriaId;
                    model.Data = modelVM.Data.Value;
                    model.Observacao = modelVM.Observacao;
                    model.Valor = modelVM.Valor.Value;
                    _ReceitaDAL.Update(model);
                    _ReceitaDAL.SaveChanges();
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
                _ReceitaDAL.Delete(obj => obj.Id.Equals(id));
                _ReceitaDAL.SaveChanges();
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
                Expression<Func<Receita, bool>> expressao = null;
                if (!string.IsNullOrEmpty(data))
                {
                    DateTime expressaoData = DateTime.Parse(data);
                    expressao = obj => obj.Data.Equals(expressaoData);
                }
                if (!string.IsNullOrEmpty(categoriaId))
                {
                    expressao = expressao == null ? obj => obj.CategoriaId.Equals(categoriaId) : expressao.And(obj => obj.CategoriaId.Equals(categoriaId));
                }
                var lista = expressao == null ? _ReceitaDAL.Get.OrderBy(obj => obj.Data)
                    .Select(sel => new ReceitaVM
                    {
                        CategoriaId = sel.Categoria.Id,
                        CategoriaNome = sel.Categoria.Nome,
                        Data = sel.Data,
                        Id = sel.Id,
                        Observacao = sel.Observacao,
                        Valor = sel.Valor
                    })
                    .ToList() : _ReceitaDAL.Get.Where(expressao).OrderBy(obj => obj.Data)
                    .Select(sel => new ReceitaVM
                    {
                        CategoriaId = sel.Categoria.Id,
                        CategoriaNome = sel.Categoria.Nome,
                        Data = sel.Data,
                        Id = sel.Id,
                        Observacao = sel.Observacao,
                        Valor = sel.Valor
                    })
                    .ToList<ReceitaVM>();

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
