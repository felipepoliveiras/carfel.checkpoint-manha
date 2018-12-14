using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Carfel.Checkpoint.Interfaces;
using Senai.Carfel.Checkpoint.Models;
using Senai.Carfel.Checkpoint.Repositorios;
using Senai.Carfel.Checkpoint.ViewModels.Administrador;

namespace Senai.Carfel.Checkpoint.Controllers
{
    //Controller responsável pelas telas do administrador
    public class AdministradorController : Controller
    {
        //Declaração da interface para trabalhar com polimorfismo
        private IUsuarioRepositorio usuarioRepositorio;
        private IDepoimentoRepositorio depoimentoRepositorio;

        public AdministradorController()
        {
            //Instancia a classe Usuario
            usuarioRepositorio = new UsuarioRepositorioSerializacao();
            depoimentoRepositorio = new DepoimentoRepositorioSerializacao();
        }
        [HttpGet]
        public IActionResult Usuarios(){
            if(HttpContext.Session.GetString("tipoUsuario") != "Administrador"){
                return RedirectToAction("Login", "Usuario");
            }

            ViewData["Usuarios"] = usuarioRepositorio.Listar();

            return View();
        }

        [HttpGet]
        public IActionResult Depoimentos(){
            if(HttpContext.Session.GetString("tipoUsuario") != "Administrador"){
                return RedirectToAction("Login", "Usuario");
            }

            ViewData["Depoimentos"] = depoimentoRepositorio.Listar();

            return View();
        }

        [HttpGet]
        public IActionResult AlterarDepoimento(int id, string status){
            if(HttpContext.Session.GetString("tipoUsuario") != "Administrador"){
                return RedirectToAction("Login", "Usuario");
            }

            //Busca o depoimento pelo seu Id
            DepoimentoModel depoimento = depoimentoRepositorio.BuscarPorId(id);

            //Verifica se o depoimento é nulo
            if(depoimento == null){
                //Caso seja nulo envia mensagem para o usuário
                TempData["Mensagem"] = "Informe um depoimento válido";
                return RedirectToAction("Depoimentos");
            }

            //Caso exista altera o status para o passado como parametro
            depoimento.Status = status;

            //Altera o depoimento na base de dados
            depoimentoRepositorio.Alterar(depoimento);

            return RedirectToAction("Depoimentos");
        }
    
        [HttpGet]
        public IActionResult DashBoard(){
            if(HttpContext.Session.GetString("tipoUsuario") != "Administrador"){
                return RedirectToAction("Login", "Usuario");
            }

            DashboardViewModel dashBoard = new DashboardViewModel();

            List<DepoimentoModel> lsDepoimentos = depoimentoRepositorio.Listar();
            List<UsuarioModel> lsUsuarios = usuarioRepositorio.Listar();

            dashBoard.QuantUsuario = lsUsuarios.Count;
            dashBoard.QuantDepoimento = lsDepoimentos.Count;
            dashBoard.QuantDepoimentoAprovado = lsDepoimentos.Count(d => d.Status == "Aprovado");
            dashBoard.QuantDepoimentoReprovado = lsDepoimentos.Count(d => d.Status == "Reprovado");

            dashBoard.ListaUsuarios = lsUsuarios.Take(5).OrderByDescending(d => d.DataCadastro).ToList();
            dashBoard.ListaDepoimentos = lsDepoimentos.Take(5).OrderByDescending(d => d.DataCadastro).ToList();

            return View(dashBoard);
        }
    }
}