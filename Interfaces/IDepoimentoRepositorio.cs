using System.Collections.Generic;
using Senai.Carfel.Checkpoint.Models;

namespace Senai.Carfel.Checkpoint.Interfaces
{
    /// <summary>
    /// Interface responsável pelos metodo do repositorio Depoimento
    /// </summary>
    public interface IDepoimentoRepositorio
    {
        /// <summary>
        /// Cadastra um novo depoimento
        /// </summary>
        /// <param name="depoimento">Depoimento que será cadastrado</param>
        void Cadastrar(DepoimentoModel depoimento);

        /// <summary>
        /// Altera um depoimento
        /// </summary>
        /// <param name="depoimento">Depoimento que será alterado</param>
        void Alterar(DepoimentoModel depoimento);

        /// <summary>
        /// Busca um depoimento pelo seu Id
        /// </summary>
        /// <param name="id">Id depoimento</param>
        /// <returns>Retorna um depoimento caso encontrado ou nulo caso não encontrado</returns>
        DepoimentoModel BuscarPorId(int id);

        /// <summary>
        /// Lista todos os depoimentos cadastrados
        /// </summary>
        /// <returns>Retorna uma lista de depoimentos</returns>
        List<DepoimentoModel> Listar();
    }
}