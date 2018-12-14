using System.Collections.Generic;
using Senai.Carfel.Checkpoint.Models;

namespace Senai.Carfel.Checkpoint.ViewModels.Administrador
{
    public class DashboardViewModel
    {
        public int QuantUsuario { get; set; }
        public int QuantDepoimento { get; set; }
        public int QuantDepoimentoAprovado { get; set; }
        public int QuantDepoimentoReprovado { get; set; }

        public List<DepoimentoModel> ListaDepoimentos { get; set; }
        public List<UsuarioModel> ListaUsuarios { get; set; }
    }
}