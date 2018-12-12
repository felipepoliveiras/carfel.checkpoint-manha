using System;

namespace Senai.Carfel.Checkpoint.Models
{
    [Serializable]
    public class BaseModel
    {
        /// <summary>
        /// Identificação única dos modelos no banco de dados
        /// </summary>
        public int ID {get; set;}

        /// <summary>
        /// A data onde o modelo foi registrado no banco de dados
        /// </summary>
        public DateTime DataCadastro {get; set;}
    }
}