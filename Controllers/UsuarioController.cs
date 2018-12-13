using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Carfel.Checkpoint.Interfaces;
using Senai.Carfel.Checkpoint.Models;
using Senai.Carfel.Checkpoint.Repositorios;

namespace Senai.Carfel.Checkpoint.Controllers
{
    /// <summary>
    /// Controllers de funcionalidades do modelo de Usuario
    /// </summary>
    public class UsuarioController : Controller
    {

        /// <summary>
        /// Repositório de persistência de usuários
        /// </summary>
        private IUsuarioRepositorio UsuarioRepositorio {get; set;}

        //CONSTRUTOR
        public UsuarioController()
        {
            UsuarioRepositorio = new UsuarioRepositorioSerializacao();
        }

        [HttpGet]
        public IActionResult Cadastrar() => View();

        [HttpPost]
        public IActionResult Cadastrar(IFormCollection form)
        {
            //Coletar as informações enviadas via POST
            UsuarioModel usuario = new UsuarioModel(
                nome: form["nome"],
                email: form["email"],
                senha: form["senha"]
            );

            UsuarioRepositorio.Cadastrar(usuario);

            return RedirectToAction("Login");
        }

        [HttpGet] //<- Atributos
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(IFormCollection form)
        {
            //Pegando os dados do form
            string email = form["email"];
            string senha = form["senha"];

            //Lendo o usuario do banco de dados
            UsuarioModel usuarioBuscado = UsuarioRepositorio.Login(email, senha);

            //Verificando se usuario foi buscado com sucesso
            if (usuarioBuscado != null)
            {
                HttpContext.Session.SetString("idUsuario", usuarioBuscado.ID.ToString());
                HttpContext.Session.SetString("emailUsuario", usuarioBuscado.Email);
                HttpContext.Session.SetString("nomeUsuario", usuarioBuscado.Nome);

                if(usuarioBuscado.Administrador){
                    HttpContext.Session.SetString("tipoUsuario", "Administrador");
                    return RedirectToAction("Dashboard", "Administrador");
                } else {
                    HttpContext.Session.SetString("tipoUsuario", "Usuario");
                    return RedirectToAction("Index", "Depoimento");
                }
            }
            else
            {
                //Reabre a tela de login
                ViewBag.Mensagem = "<div class='alert alert-danger' role='alert'>Usuário inválido</div>";
                
                return View();
            }
        }

        [HttpGet]
        public IActionResult Sair(){
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Pages");
        }

    }
}