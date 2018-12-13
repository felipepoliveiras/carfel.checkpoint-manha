using Microsoft.AspNetCore.Mvc;
using Senai.Carfel.Checkpoint.Interfaces;
using Senai.Carfel.Checkpoint.Repositorios;

namespace Senai.Carfel.Checkpoint.Controllers
{
    public class DepoimentoController : Controller
    {
        private IDepoimentoRepositorio depoimentoRepositorio;
        
        public DepoimentoController()
        {
            depoimentoRepositorio = new DepoimentoRepositorioSerializacao();
        }

        [HttpGet]
        public IActionResult Index(){
            //Atribui os depoimentos ao ViewData para serem obtidos na página cshtml
            ViewData["Depoimentos"] = depoimentoRepositorio.Listar();
            
            return View();
        }
    }
}