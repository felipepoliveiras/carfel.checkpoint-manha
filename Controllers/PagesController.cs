using Microsoft.AspNetCore.Mvc;

namespace Senai.Carfel.Checkpoint.Controllers
{
    public class PagesController : Controller
    {
        [HttpGet]
        public IActionResult Index(){
            return View();
        }

        [HttpGet]
        public IActionResult Sobre(){
            return View();
        }

        [HttpGet]
        public IActionResult Faq(){
            return View();
        }

        [HttpGet]
        public IActionResult Contato(){
            return View();
        }
    }
}