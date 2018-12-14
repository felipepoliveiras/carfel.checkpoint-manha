using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Senai.Carfel.Checkpoint.Interfaces;
using Senai.Carfel.Checkpoint.Models;
using Senai.Carfel.Checkpoint.Repositorios;

namespace Senai.Carfel.Checkpoint
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Criando o adm padrao caso nao exista
            if (!File.Exists("usuarios.dat"))
            {
                UsuarioModel adm = new UsuarioModel(
                    nome: "Administrador do Sistema" ,
                    email: "adm@carfel.com",
                    senha: "admin"
                );
                adm.Administrador = true;
                adm.DataCadastro = DateTime.Now;

                IUsuarioRepositorio usuarioRepositorio;
                usuarioRepositorio = new UsuarioRepositorioSerializacao();

                usuarioRepositorio.Cadastrar(adm);
            }

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
