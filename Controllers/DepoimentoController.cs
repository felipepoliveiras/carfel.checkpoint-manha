using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Carfel.Checkpoint.Interfaces;
using Senai.Carfel.Checkpoint.Models;
using Senai.Carfel.Checkpoint.Repositorios;

namespace Senai.Carfel.Checkpoint.Controllers
{
    public class DepoimentoController : Controller
    {
        //Criação do objeto do tipo interface para fazer polimorfismo
        private IDepoimentoRepositorio depoimentoRepositorio;
        

        public DepoimentoController()
        {
            //Instância a interface com a classe DepoimentoRepositorioSerializacao
            depoimentoRepositorio = new DepoimentoRepositorioSerializacao();
        }

        [HttpGet]
        public IActionResult Index(){
            //Atribui os depoimentos ao ViewData para serem obtidos na página cshtml
            //Filtra com linq somente para depoimentos aprovados
            ViewData["Depoimentos"] = depoimentoRepositorio.Listar().Where(d => d.Status == "Aprovado").ToList();
            
            return View();
        }

        

        [HttpPost]
        public IActionResult Cadastrar(IFormCollection form){
            //Cria um objeto do tipo DepoimentoModel
            //Atribui os valor do formuário de da sessão ao objeto
            DepoimentoModel depoimento = new DepoimentoModel();
            depoimento.Depoimento = form["depoimento"];
            depoimento.Status = "Em Espera";
            depoimento.DataCadastro = DateTime.Now;
            //depoimento.Usuario = new UsuarioModel();
            depoimento.Usuario.ID = int.Parse(HttpContext.Session.GetString("idUsuario"));
            depoimento.Usuario.Nome = HttpContext.Session.GetString("nomeUsuario");
            depoimento.Usuario.Email = HttpContext.Session.GetString("emailUsuario");
            
            //Cadastra o depoimento
            depoimentoRepositorio.Cadastrar(depoimento);

            //Exibe mensagem para o usuário
            TempData["Mensagem"] = "Depoimento Cadastrado. Aguarde a aprovação do Administrador";

            return RedirectToAction("Index");
        }
    }
}