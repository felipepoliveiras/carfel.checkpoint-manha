using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Senai.Carfel.Checkpoint.Interfaces;
using Senai.Carfel.Checkpoint.Models;
using Senai.Carfel.Checkpoint.Repositorios;

namespace Senai.Carfel.Checkpoint
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Ativa as funcionalidades do MVC
            services.AddMvc();


            services.AddDistributedMemoryCache();

            //Ativa o uso de sessão
            services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(30));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Criando o adm padrao caso nao exista
            if (!File.Exists("usuario.dat"))
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


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession(); // <- Utiliza sessão

            app.UseMvc(
                rota => rota.MapRoute(
                    name: "defaults",
                    template: "{controller=Usuario}/{action=Login}"
                )
            ); // <- Habilita o uso de MVC no app

            //Permite acessso ao /wwwroot para baixar arquivos estáticos
            app.UseStaticFiles();

        

        }
    }
}
