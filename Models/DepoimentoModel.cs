using System;

namespace Senai.Carfel.Checkpoint.Models
{
    /// <summary>
    /// Modelo de Classe responsável pelos depoimento
    /// </summary>
    [Serializable]
    public class DepoimentoModel : BaseModel
    {
        /// <summary>
        /// Mensagem do depoimento
        /// </summary>
        public string Depoimento { get; set; }
        
        /// <summary>
        /// Armazena o status do depoimento(Em espera, Aprovado e Rejeitado)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Armazena o usuário que cadastrou o depoimento
        /// </summary>
        public UsuarioModel Usuario { get; set; }
    
        public DepoimentoModel()
        {
            Usuario = new UsuarioModel();
        }
    }
}