using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Fortes.DAL;
using Fortes.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Fortes.Web.Controllers
{
    public class CategoriasController : BaseController
    {
        private JsonResult _jsonResult;
        private ICategoriaDAL _categoriaDAL;

        public CategoriasController(ICategoriaDAL categoriaDAL)
        {
            _categoriaDAL = categoriaDAL;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Incluir([FromBody]Categoria model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    model.Id = Guid.NewGuid().ToString();
                    _categoriaDAL.Insert(model);
                    _categoriaDAL.SaveChanges();
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
        public IActionResult Editar([FromBody]Categoria model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    _categoriaDAL.Update(model);
                    _categoriaDAL.SaveChanges();
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
                _categoriaDAL.Delete(obj => obj.Id.Equals(id));
                _categoriaDAL.SaveChanges();
                _jsonResult = Json(new { status = "OK", resultado = "Registro excluído com sucesso!" });
            }
            catch (Exception ex)
            {
                _jsonResult = Json(new { status = "FALHA", resultado = ex.Message });
            };
            return _jsonResult;
        }
        
        public JsonResult Lista(string valor)
        {
            try
            {
                var lista = string.IsNullOrEmpty(valor) ? _categoriaDAL.Get.OrderBy(obj => obj.Nome).ToList() : _categoriaDAL.Get.Where(obj => obj.Nome.Contains(valor)).OrderBy(obj => obj.Nome).ToList();
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
