using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Core.Interfaces;
using Portal.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Portal.Infra.Repository
{
    public class RepositoryArquivos : Repository<Arquivos>, IRepositoryArquivos
    {
        private readonly DbContext context;
        public RepositoryArquivos(DbContext context) : base(context)
        {
            this.context = context;
        }

        /// <summary>
        /// Buscar Arquivos Pendentes com base no ID do Sinistro e no TipoSinistrosID
        /// </summary>
        /// <param name="sinistrosID">Identificador único do Sinistro</param>
        /// /// <param name="tipoSinistrosID">Identificador único do Tipo do Sinistro</param>
        /// <returns>Retorna uma lista somente com o TiposID e oTipo Arquivo</returns>
        public List<DocumentosPendentesViewModel> SelectByArquivosPendentes(long sinistrosID, int tipoSinistrosID)
        {
            var query = "sp_ArquivosPendentes";
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;

            var parameter = new SqlParameter("@SinistrosID", sinistrosID);
            var parameter2 = new SqlParameter("@TipoSinistrosID", tipoSinistrosID);

            command.Parameters.Add(parameter);
            command.Parameters.Add(parameter2);

            context.Database.OpenConnection();
            var arquivos = command.ExecuteReader();
            var resultPendencia = new List<DocumentosPendentesViewModel>();

            while (arquivos.Read())
            {
                var ent = new DocumentosPendentesViewModel()
                {
                    TiposID = Convert.ToInt64(arquivos.GetValue(0)),
                    Tipo = arquivos.GetValue(1).ToString()
                };
                resultPendencia.Add(ent);
            }
            return resultPendencia;
        }

        /// <summary>
        /// Buscar algum arquivo com base no ID do Sinistro e com a Url != "deleted"
        /// </summary>
        /// <param name="sinistrosID">Identificador único do Sinistro</param>
        /// <returns>Retorna uma lista somente com o ID, Título, Tipo, Status, Extensão, SinistrosID, Sequecial, AtendimentoID e a Url do Arquivo</returns>
        public List<DocumentosSinistroViewModel> SelectByDocumentosSinistro(long sinistrosID) =>

            Items.Where(a => a.SinistroID == sinistrosID
                        && a.url != "deleted"
                        && a.Status != 4)
                 .Select(arq => new DocumentosSinistroViewModel
                 {
                     Id = arq.Id,
                     Titulo = arq.Titulo,
                     Tipo = arq.Tipo,
                     Status = arq.Status,
                     SinistroID = arq.SinistroID,
                     Sequencial = arq.Sequencial,
                     AtendimentoID = arq.AtendimentoID,
                     url = arq.url
                 }).ToList();

        /// <summary>
        /// Conta a quantidade de arquivo aprovado ou em analise com base no Arquivo, na Controller e no SinistroID
        /// </summary>
        /// <param name="Arquivo">Tipo do Arquivo</param>
        /// <param name="Controller">Identificador da Controller</param>
        /// <param name="SinistroID">Identificador único do Sinistro</param>
        /// <returns>Retorna a contagem dos arquivos aprovado ou em analise</returns>
        public int CountByArquivosAprovado(string Arquivo, long Controller, long SinistroID) =>

            Items.Where(a => a.ControllerID == Controller
                        && a.SinistroID == SinistroID
                        && a.Tipo.Contains(Arquivo)
                        && (a.Status == 1 | a.Status == 2)).Count();

        /// <summary>
        /// Conta a quantidade dos arquivos reprovado com base no Arquivo, na Controller, SinistroID e no Sequencial
        /// </summary>
        /// <param name="Arquivo">Tipo do Arquivo</param>
        /// <param name="Controller">Identificador da Controller</param>
        /// <param name="SinistroID">Identificador único do Sinistro</param>
        /// <param name="Sequencial">Atributo Sequencial</param>
        /// <returns>Retorna a contagem dos arquivo reprovado</returns>
        public int CountArquivosReprovado(string Arquivo, long Controller, long SinistroID, long Sequencial) =>

            Items.Where(a => a.ControllerID == Controller
                        && a.SinistroID == SinistroID
                        && a.Tipo.Contains(Arquivo)
                        && a.Status == 3
                        && a.Sequencial == Sequencial
                        && a.Substituido == 0).Count();

        /// <summary>
        /// Buscar algum arquivo Repetido com base no Tipo, Sequencial, Status e no Substituido
        /// </summary>
        /// <param name="Tipo">Tipo do Arquivo</param>
        /// <param name="Sequencial">Atributo Sequencial</param>
        /// <param name="Status">Status do Arquivo</param>
        /// <param name="Substituido">Atributo Substituido</param>
        /// <returns>Retorna uma lista somente com o ID, Titulo, Tipo, Extensao, url, AtendimentoID, Status, Motivo, Sequencial e o Substituido</returns>
        public Arquivos SelectByArquivosRepetido(string Tipo, int Sequencial, long Status, int Substituido) =>

            Items.Where(a => a.Tipo == Tipo
                        && a.Sequencial == Sequencial
                        && a.Status == Status
                        && a.Substituido == Substituido)
                 .Select(arq => new Arquivos
                 {
                     Id = arq.Id,
                     Titulo = arq.Titulo,
                     Tipo = arq.Tipo,
                     Extensao = arq.Extensao,
                     url = arq.url,
                     AtendimentoID = arq.AtendimentoID,
                     Status = arq.Status,
                     Motivo = arq.Motivo,
                     Sequencial = arq.Sequencial,
                     Substituido = arq.Substituido
                 }).FirstOrDefault();

        /// <summary>
        /// Conta os Arquivos Pendentes com base no ID do Sinistro e no TipoSinistrosID
        /// </summary>
        /// <param name="sinistrosID">Identificador único do Sinistro</param>
        /// /// <param name="tipoSinistrosID">Identificador único do Tipo do Sinistro</param>
        /// <returns>Retorna a contagem total de arquivos pendentes</returns>
        public int SelectByContaArquivosPendentes(long sinistrosID, int tipoSinistrosID)
        {
            var query = "sp_ContaArquivosPendentes";
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;
            var parameter = new SqlParameter("@SinistrosID", sinistrosID);
            var parameter2 = new SqlParameter("@TipoSinistrosID", tipoSinistrosID);
            command.Parameters.Add(parameter);
            command.Parameters.Add(parameter2);
            context.Database.OpenConnection();
            var result = command.ExecuteScalar();
            return Convert.ToInt32(result);
        }
    }
}
