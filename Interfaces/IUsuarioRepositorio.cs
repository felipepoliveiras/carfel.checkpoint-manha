using System.Collections.Generic;
using Senai.Carfel.Checkpoint.Models;

namespace Senai.Carfel.Checkpoint.Interfaces
{
    /// <summary>
    /// Interface para DDD de UsuarioRepositorio
    /// </summary>
    public interface IUsuarioRepositorio
    {
        /// <summary>
        /// Cadastra um usuário no banco de dados
        /// </summary>
        /// <param name="usuario">O usuário que será cadastrado</param>
        void Cadastrar(UsuarioModel usuario);

        /// <summary>
        /// Verifica se existe um usuário cadastrado no banco de dados
        /// com o e-mail e senha informados. Caso exista retorna
        /// o objeto do modelo encontrado
        /// </summary>
        /// <param name="email">O e-mail informado para busca</param>
        /// <param name="senha">A senha informado para busca</param>
        /// <returns>O usuário encontrado no banco ou,
        /// caso não encontre, retorna nulo</returns>
        UsuarioModel Login(string email, string senha);

        /// <summary>
        /// Retorna todos os usuários cadastrados no banco de dados
        /// </summary>
        List<UsuarioModel> Listar();
    }
}