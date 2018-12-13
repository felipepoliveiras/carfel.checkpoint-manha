using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Senai.Carfel.Checkpoint.Interfaces;
using Senai.Carfel.Checkpoint.Models;

namespace Senai.Carfel.Checkpoint.Repositorios
{
    public class DepoimentoRepositorioSerializacao : IDepoimentoRepositorio
    {
        //Irá armazenar os depoimentos
        private List<DepoimentoModel> DepoimentosSalvos {get; set; }

        public DepoimentoRepositorioSerializacao()
        {
            if(!File.Exists("depoimentos.dat")){
                DepoimentosSalvos = new List<DepoimentoModel>();
            } else {
                DesserializarLista();
            }
        }

        /// <summary>
        /// Desserializa o arquivo depoimentos.dat
        /// Obtêm todos os dados do arquivo
        /// </summary>
        private void DesserializarLista()
        {
            //Le os dados do arquivo depoimentos.dat
            byte[] bytesArquivo = File.ReadAllBytes("depoimentos.dat");
            MemoryStream memoria = new MemoryStream(bytesArquivo);
            BinaryFormatter serializador = new BinaryFormatter();
            
            //Atribui os depoimento a lista, faz a conversão do objeto para List<DepoimentoModel>
            DepoimentosSalvos = (List<DepoimentoModel>)serializador.Deserialize(memoria);
        }

        /// <summary>
        /// Serializa o arquivo depoimentos.dat
        /// Armazena o dado na lista
        /// </summary>
        private void SerializarLista(){
            MemoryStream memoria = new MemoryStream();
            BinaryFormatter serializador = new BinaryFormatter();

            serializador.Serialize(memoria, DepoimentosSalvos);
            File.WriteAllBytes("depoimentos.dat", memoria.ToArray());
        }

        public void Alterar(DepoimentoModel depoimento)
        {
            //Percorre a lista de Depoimentos
            for (int i = 0; i < DepoimentosSalvos.Count; i++)
            {
                //Verifica se o id do depoimento informado é o mesmo
                if(DepoimentosSalvos[i].ID == depoimento.ID){
                    //Altera os dados do depoimento para os dados passados
                    DepoimentosSalvos[i] = depoimento;
                    //Grava as informações
                    SerializarLista();
                    //Sai do for
                    break;
                }
            }
        }

        public DepoimentoModel BuscarPorId(int id)
        {

            foreach (var depoimento in DepoimentosSalvos)
            {
                if(depoimento.ID == id){
                    return depoimento;
                }
            }

            return null;
        }

        public void Cadastrar(DepoimentoModel depoimento)
        {
            //Altera o Id do depoimento, incrementa 1 ao id
            depoimento.ID = DepoimentosSalvos.Count + 1;
            //Adiciona o objeto depoimento a lista
            DepoimentosSalvos.Add(depoimento);
            //Grava a informação
            SerializarLista();
        }

        public List<DepoimentoModel> Listar()
        {
            return DepoimentosSalvos;
        }
    }
}