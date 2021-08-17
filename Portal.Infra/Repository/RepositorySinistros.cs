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
    public class RepositorySinistros : Repository<Sinistros>, IRepositorySinistros
    {
        private readonly DbContext context;
        public RepositorySinistros(DbContext context) : base(context)
        {
            this.context = context;
        }

        /// <summary>
        /// Buscar os Todos os Sinistros cadastrados no Banco.
        /// </summary>
        /// <returns>Retorna uma Lista com o ID, Cliente, DataSinistro, UltimoStatus, Seguradora, Protocolo, ValorCarga do Sinistro, Valor do Semaforo e se possue Documentos Pendentes</returns>
        public List<SinistrosAllViewModel> SelectSinistrosAll()
        {
            var query = "sp_AllSinistros";
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;

            context.Database.OpenConnection();
            var arquivos = command.ExecuteReader();
            var sinistros = new List<SinistrosAllViewModel>();

            while (arquivos.Read())
            {
                var ent = new SinistrosAllViewModel()
                {
                    ID = Convert.ToInt64(arquivos.GetValue(0)),
                    DataSinistro = Convert.ToDateTime(arquivos.GetValue(1)),
                    Segurado = arquivos.GetValue(2).ToString(),
                    UltimoStatus = arquivos.GetValue(3).ToString(),
                    Ramo = arquivos.GetValue(4).ToString(),
                    Seguradora = arquivos.GetValue(5).ToString(),
                    Apelido = arquivos.GetValue(6).ToString(),
                    Protocolo = Convert.ToInt64(arquivos.GetValue(7)),
                    Valor = Convert.ToDecimal(arquivos.GetValue(8)),
                    Semaforo = Convert.ToInt32(arquivos.GetValue(9)),
                    Pendente = Convert.ToBoolean(arquivos.GetValue(10))
                };
                sinistros.Add(ent);
            }
            return sinistros;
        }

        /// <summary>
        /// Buscar os Sinistros do Cliente e as dados das tabelas relacionadas com base no Id do Cliente.
        /// </summary>
        /// <returns>Retorna uma Lista com o ID, DataSinistro, UltimoStatus, Seguradora, Protocolo, ValorCarga do Sinistro, Valor do Semaforo e se possue Documentos Pendentes</returns>
        public List<SinistrosViewModel> SelectSinistrosClienteByClienteId(long clienteId)
        {
            var query = "sp_AllSinistrosSegurado";
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;
            var parameter = new SqlParameter("@ClienteID", clienteId);

            command.Parameters.Add(parameter);

            context.Database.OpenConnection();

            var arquivos = command.ExecuteReader();
            var sinistros = new List<SinistrosViewModel>();

            while (arquivos.Read())
            {
                var ent = new SinistrosViewModel()
                {
                    ID = Convert.ToInt64(arquivos.GetValue(0)),
                    DataSinistro = Convert.ToDateTime(arquivos.GetValue(1)),
                    UltimoStatus = arquivos.GetValue(2).ToString(),
                    Ramo = arquivos.GetValue(3).ToString(),
                    Seguradora = arquivos.GetValue(4).ToString(),
                    Apelido = arquivos.GetValue(5).ToString(),
                    Protocolo = Convert.ToInt64(arquivos.GetValue(6)),
                    Valor = Convert.ToDecimal(arquivos.GetValue(7)),
                    Semaforo = Convert.ToInt32(arquivos.GetValue(8)),
                    Pendente = Convert.ToBoolean(arquivos.GetValue(9))
                };
                sinistros.Add(ent);
            }
            return sinistros;
        }

        /// <summary>
        /// Buscar os Sinistros do Produtor e as dados das tabelas relacionadas com base no Id do Usuário.
        /// </summary>
        /// <returns>Retorna uma Lista com o ID, Cliente, DataSinistro, UltimoStatus, Seguradora, Protocolo, ValorCarga do Sinistro, Valor do Semaforo e se possue Documentos Pendentes</returns>
        public List<SinistrosProdutorViewModel> SelectSinistrosProdutorByusuariosID(string usuariosID)
        {
            var query = "sp_AllSinistrosProdutor";
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;
            var parameter = new SqlParameter("@UsuarioID", usuariosID);

            command.Parameters.Add(parameter);

            context.Database.OpenConnection();

            var arquivos = command.ExecuteReader();
            var sinistros = new List<SinistrosProdutorViewModel>();

            while (arquivos.Read())
            {
                var ent = new SinistrosProdutorViewModel()
                {
                    ID = Convert.ToInt64(arquivos.GetValue(0)),
                    DataSinistro = Convert.ToDateTime(arquivos.GetValue(1)),
                    Segurado = arquivos.GetValue(2).ToString().Substring(0, 20),
                    UltimoStatus = arquivos.GetValue(3).ToString(),
                    Ramo = arquivos.GetValue(4).ToString(),
                    Seguradora = arquivos.GetValue(5).ToString(),
                    Apelido = arquivos.GetValue(6).ToString(),
                    Protocolo = Convert.ToInt64(arquivos.GetValue(7)),
                    Valor = Convert.ToDecimal(arquivos.GetValue(8)),
                    Semaforo = Convert.ToInt32(arquivos.GetValue(9)),
                    Pendente = Convert.ToBoolean(arquivos.GetValue(10))
                };
                sinistros.Add(ent);
            }
            return sinistros;
        }

        /// <summary>
        /// Buscar o TiposSinistrosID com base no seu ID
        /// </summary>
        /// <param name="Id">Identificador único do Sinistro</param>
        /// <returns>Retorna SOMENTE o TiposSinistrosID do Sinistro</returns>
        public int SelectByTiposSinistrosID(long Id) =>

            Items.Where(s => s.Id == Id)
                 .Select(sin => new Sinistros
                 {
                     TiposSinistrosID = sin.TiposSinistrosID
                 }).FirstOrDefault().TiposSinistrosID;

        /// <summary>
        /// Buscar as informações do Sinistros pelo Id do Sinistro.
        /// </summary>
        /// <param name="Id">Identificador único do Sinistro</param>
        /// <returns>Retorna as Informações do Sinistro que foi selecionado.</returns>
        public InfoSinistroViewModel SelectInfoSinistroById(long Id) =>

            Items.Where(s => s.Id == Id)
                 .Include(s => s.Unidades)
                 .Include(s => s.Seguradoras)
                 .Include(s => s.Reguladoras)
                 .Include(s => s.RamosSeguros)
                 .Include(s => s.TiposSinistros)
                 .Include(s => s.TiposNaturezaSinistros)
                 .Select(sin => new InfoSinistroViewModel
                 {
                     ID = sin.Id,
                     Cliente = sin.Unidades.Nome,
                     Protocolo = sin.Protocolo,
                     Seguradora = sin.Seguradoras.NomePrincipal.Substring(0, 20),
                     Reguladora = sin.Reguladoras.NomePrincipal.Substring(0, 20),
                     Ramos = sin.RamosSeguros.Tipo,
                     DataSinistro = sin.DataSinistro,
                     DataAviso = sin.DataAviso,
                     TiposSinistros = sin.TiposSinistros.Tipo,
                     NaturezaSinistro = sin.TiposNaturezaSinistros.Tipo,
                     ValorCarga = sin.ValorCarga,
                     ValorIndenizado = sin.ValorIndenizado,
                     ValorSinistro = sin.ValorSinistro,
                     DataConclusao = sin.DataConclusao,
                     DataIndenizacao = sin.DataIndenizacao,
                     UltimoStatus = sin.Situacao,
                     CidadeOrigem = sin.CidadeOrigem,
                     CidadeDestino = sin.CidadeDestino,
                     Mecadoria = sin.Mecadoria,
                     NomeMotorista = String.IsNullOrEmpty(sin.CPFMotorista) ? "" : sin.CPFMotorista,
                     UFOrigem = String.IsNullOrEmpty(sin.UFOrigem) ? "" : sin.UFOrigem,
                     UFDestino = String.IsNullOrEmpty(sin.UFDestino) ? "" : sin.UFDestino,
                     UFSinistro = String.IsNullOrEmpty(sin.UFSinistro) ? "" : sin.UFSinistro,
                     PlacaVeiculo = sin.PlacaVeiculo,
                     PlacaCarreta = sin.PlacaCarreta,
                     LocalSinistro = sin.LocalSinistro,
                     CidadeSinistro = sin.CidadeSinistro,
                 }).FirstOrDefault();

        /// <summary>
        /// Buscar todos os Ramos dos Sinistros so Segurado pelo Id do Sinistro e soma todos os Valores Total Prejuízo(ValorSinistro) de cada Ramo.
        /// </summary>
        /// <param name="clienteId"></param>
        /// <returns>Ramo e o Valor Total do Prejuízo de cada Ramo</returns>
        public List<GraficoPizzaSeguradoViewModel> SelectGraficoPizzaSeguradoByclienteId(long clienteId)
        {
            var query = "sp_GraficoSeguradoPizzaAll";

            using var command = context.Database.GetDbConnection().CreateCommand();

            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;

            var parameter = new SqlParameter("@clienteid", clienteId);

            command.Parameters.Add(parameter);

            context.Database.OpenConnection();

            var itens = command.ExecuteReader();
            var grafico = new List<GraficoPizzaSeguradoViewModel>();

            while (itens.Read())
            {
                var ent = new GraficoPizzaSeguradoViewModel()
                {
                    Ramo = itens.GetValue(0).ToString(),
                    ValorTotalPrejuizo = Convert.ToDecimal(itens.GetValue(1))
                };
                grafico.Add(ent);
            }
            return grafico;
        }

        /// <summary>
        /// Buscar as informações necessarias para o grafico em coluna do segurado pelo Id do Cliente do Sinistro.
        /// </summary>
        /// <param name="clienteId"></param>
        /// <returns>retorna o Eventos por Ano, o total de Eventos, o Ano, o Ramo e o Total do Valor do Prejuízo Por Ano</returns>
        public List<GraficoColunaSeguradoViewModel> SelectGraficoColunaLinhaSeguradoByclienteId(long clienteId)
        {
            var query = "sp_GraficoSeguradoColunaAll";
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;

            var parameter = new SqlParameter("@clienteid", clienteId);

            command.Parameters.Add(parameter);

            context.Database.OpenConnection();

            var itens = command.ExecuteReader();
            var grafico = new List<GraficoColunaSeguradoViewModel>();

            while (itens.Read())
            {
                var ent = new GraficoColunaSeguradoViewModel()
                {
                    Ano = itens.GetValue(0).ToString(),
                    ValorPrejuizoAno = Convert.ToDecimal(itens.GetValue(1)),
                    EventosAno = Convert.ToInt32(itens.GetValue(2))
                };
                grafico.Add(ent);
            }
            return grafico;
        }

        /// <summary>
        /// Buscar as informações necessarias para o grafico em coluna do Administrador pelo id do cliente e Ano do Sinistro do Segurado.
        /// </summary>
        /// <param name="clienteId"></param>
        /// <param name="ano"></param> 
        /// <returns>retorna o Eventos por Ano, o total de Eventos, o Ano, o Ramo e o Total do Valor do Prejuízo Por Ano</returns>
        public List<GraficoColunaSeguradoAnoViewModel> SelectGraficoColunaLinhaSeguradoByclienteIdAno(long clienteId, string ano)
        {
            var query = "sp_GraficoSeguradoColunaporAno";
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;
            var parameter = new SqlParameter("@clienteid", clienteId);
            var parameter2 = new SqlParameter("@ano", ano);

            command.Parameters.Add(parameter);
            command.Parameters.Add(parameter2);

            context.Database.OpenConnection();

            var itens = command.ExecuteReader();
            var grafico = new List<GraficoColunaSeguradoAnoViewModel>();

            while (itens.Read())
            {
                var ent = new GraficoColunaSeguradoAnoViewModel()
                {
                    Ano = itens.GetValue(0).ToString(),
                    Ramo = itens.GetValue(1).ToString(),
                    ValorPrejuizoAno = Convert.ToDecimal(itens.GetValue(2)),
                    Eventos = Convert.ToInt32(itens.GetValue(3)),
                };
                grafico.Add(ent);
            }
            return grafico;
        }

        /// <summary>
        /// Buscar as informações necessarias para o grafico em coluna do segurado pelo Id do Cliente e o Ramo do Sinistro.
        /// </summary>
        /// <param name="clienteId"></param>
        /// <param name="ramo"></param> 
        /// <returns>retorna o Eventos por Ano, o total de Eventos, o Ano, o Ramo e o Total do Valor do Prejuízo Por Ano</returns>
        public List<GraficoColunaSeguradoRamoViewModel> SelectGraficoColunaLinhaSeguradoByclienteIdRamo(long clienteId, string ramo)
        {
            var query = "sp_GraficoSeguradoColunaPorRamo";
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;
            var parameter = new SqlParameter("@clienteid", clienteId);
            var parameter2 = new SqlParameter("@ramo", ramo);

            command.Parameters.Add(parameter);
            command.Parameters.Add(parameter2);

            context.Database.OpenConnection();

            var itens = command.ExecuteReader();
            var grafico = new List<GraficoColunaSeguradoRamoViewModel>();

            while (itens.Read())
            {
                var ent = new GraficoColunaSeguradoRamoViewModel()
                {
                    Ano = itens.GetValue(0).ToString(),
                    Ramo = itens.GetValue(1).ToString(),
                    ValorPrejuizoAno = Convert.ToDecimal(itens.GetValue(2)),
                    EventosAno = Convert.ToInt32(itens.GetValue(3)),
                };
                grafico.Add(ent);
            }
            return grafico;
        }

        /// <summary>
        /// Buscar as informações necessarias para o grafico em coluna do segurado pelo Id do Cliente, Ramo e o ano do Sinistro e Ano dos Sinistros.
        /// </summary>
        /// <param name="clienteId"></param>
        /// <param name="ramo"></param>
        /// /// <param name="ano"></param>
        /// <returns>retorna o Mês/Ano, o total valor prejuízo por Mês, Eventos Mês e os Eventos do Ano</returns>
        public List<GraficoColunaSeguradoRamoAnoViewModel> SelectGraficoColunaLinhaSeguradoByclienteIdRamoAno(long clienteId, string ramo, string ano)
        {
            var query = "sp_GraficoSeguradoColunaPorRamoAno";
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;
            var parameter = new SqlParameter("@clienteid", clienteId);
            var parameter2 = new SqlParameter("@ramo", ramo);
            var parameter3 = new SqlParameter("@ano", ano);

            command.Parameters.Add(parameter);
            command.Parameters.Add(parameter2);
            command.Parameters.Add(parameter3);

            context.Database.OpenConnection();

            var itens = command.ExecuteReader();
            var grafico = new List<GraficoColunaSeguradoRamoAnoViewModel>();

            while (itens.Read())
            {
                var ent = new GraficoColunaSeguradoRamoAnoViewModel()
                {
                    MesAno = itens.GetValue(0).ToString(),
                    Ramo = itens.GetValue(1).ToString(),
                    ValorPrejuizoMes = Convert.ToDecimal(itens.GetValue(2)),
                    EventosMes = Convert.ToInt32(itens.GetValue(3))
                };
                grafico.Add(ent);
            }
            return grafico;
        }

        /// <summary>
        /// Buscar todos os Ramos dos Sinistros do Produtor pelo Id do Usuario e soma todos os Valores Total Prejuízo(ValorSinistro) de cada Ramo.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns>Ramo e o Valor Total do Prejuízo de cada Ramo</returns>
        public List<GraficoPizzaProdutorViewModel> SelectGraficoPizzaProdutorByusuarioId(string usuarioId)
        {
            var query = "sp_GraficoProdutorPizzaAll";

            using var command = context.Database.GetDbConnection().CreateCommand();

            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;

            var parameter = new SqlParameter("@usuarioId", usuarioId);

            command.Parameters.Add(parameter);

            context.Database.OpenConnection();

            var itens = command.ExecuteReader();
            var grafico = new List<GraficoPizzaProdutorViewModel>();

            while (itens.Read())
            {
                var ent = new GraficoPizzaProdutorViewModel()
                {
                    Ramo = itens.GetValue(0).ToString(),
                    ValorTotalPrejuizo = Convert.ToDecimal(itens.GetValue(1))
                };
                grafico.Add(ent);
            }
            return grafico;
        }

        /// <summary>
        /// Buscar as informações necessarias para o grafico em coluna do produtor pelo Id do usuário.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns>retorna o Eventos por Ano, o total de Eventos, o Ano, o Ramo e o Total do Valor do Prejuízo Por Ano</returns>
        public List<GraficoColunaProdutorViewModel> SelectGraficoColunaLinhaProdutorByusuarioId(string usuarioId)
        {
            var query = "sp_GraficoProdutorColunaAll";
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;

            var parameter = new SqlParameter("@usuarioId", usuarioId);

            command.Parameters.Add(parameter);

            context.Database.OpenConnection();

            var itens = command.ExecuteReader();
            var grafico = new List<GraficoColunaProdutorViewModel>();

            while (itens.Read())
            {
                var ent = new GraficoColunaProdutorViewModel()
                {
                    Ano = itens.GetValue(0).ToString(),
                    ValorPrejuizoAno = Convert.ToDecimal(itens.GetValue(1)),
                    EventosAno = Convert.ToInt32(itens.GetValue(2))
                };
                grafico.Add(ent);
            }
            return grafico;
        }

        /// <summary>
        /// Buscar as informações necessarias para o grafico em coluna do Administrador pelo id do usuário e Ano do Sinistro do Produtor.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="ano"></param> 
        /// <returns>retorna o Eventos por Ano, o total de Eventos, o Ano, o Ramo e o Total do Valor do Prejuízo Por Ano</returns>
        public List<GraficoColunaProdutorAnoViewModel> SelectGraficoColunaLinhaProdutorByAno(string usuarioId, string ano)
        {
            var query = "sp_GraficoProdutorColunaporAno";

            using var command = context.Database.GetDbConnection().CreateCommand();

            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;

            var parameter = new SqlParameter("@usuarioId", usuarioId);
            var parameter2 = new SqlParameter("@ano", ano);

            command.Parameters.Add(parameter);
            command.Parameters.Add(parameter2);

            context.Database.OpenConnection();

            var itens = command.ExecuteReader();
            var grafico = new List<GraficoColunaProdutorAnoViewModel>();

            while (itens.Read())
            {
                var ent = new GraficoColunaProdutorAnoViewModel()
                {
                    Ano = itens.GetValue(0).ToString(),
                    Ramo = itens.GetValue(1).ToString(),
                    ValorPrejuizoAno = Convert.ToDecimal(itens.GetValue(2)),
                    Eventos = Convert.ToInt32(itens.GetValue(3)),
                };
                grafico.Add(ent);
            }
            return grafico;
        }

        /// <summary>
        /// Buscar as informações necessarias para o grafico em coluna do produtor pelo Id do usuário e o Ramo do Sinistro.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="ramo"></param> 
        /// <returns>retorna o Eventos por Ano, o total de Eventos, o Ano, o Ramo e o Total do Valor do Prejuízo Por Ano</returns>
        public List<GraficoColunaProdutorRamoViewModel> SelectGraficoColunaLinhaProdutorByusuarioIdRamo(string usuarioId, string ramo)
        {
            var query = "sp_GraficoProdutorColunaporRamo";
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;
            var parameter = new SqlParameter("@usuarioId", usuarioId);
            var parameter2 = new SqlParameter("@ramo", ramo);

            command.Parameters.Add(parameter);
            command.Parameters.Add(parameter2);

            context.Database.OpenConnection();

            var itens = command.ExecuteReader();
            var grafico = new List<GraficoColunaProdutorRamoViewModel>();

            while (itens.Read())
            {
                var ent = new GraficoColunaProdutorRamoViewModel()
                {
                    Ano = itens.GetValue(0).ToString(),
                    Ramo = itens.GetValue(1).ToString(),
                    ValorPrejuizoAno = Convert.ToDecimal(itens.GetValue(2)),
                    EventosAno = Convert.ToInt32(itens.GetValue(3)),
                };
                grafico.Add(ent);
            }
            return grafico;
        }

        /// <summary>
        /// Buscar as informações necessarias para o grafico em coluna do produtor pelo Id do usuário, Ramo e o ano do Sinistro e Ano dos Sinistros.
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="ramo"></param>
        /// /// <param name="ano"></param>
        /// <returns>retorna o Mês/Ano, o total valor prejuízo por Mês, Eventos Mês e os Eventos do Ano</returns>
        public List<GraficoColunaProdutorRamoAnoViewModel> SelectGraficoColunaLinhaProdutorByusuarioIdRamoAno(string usuarioId, string ramo, string ano)
        {
            var query = "sp_GraficoProdutorColunaPorRamoAno";

            using var command = context.Database.GetDbConnection().CreateCommand();

            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;

            var parameter = new SqlParameter("@usuarioId", usuarioId);
            var parameter2 = new SqlParameter("@ramo", ramo);
            var parameter3 = new SqlParameter("@ano", ano);

            command.Parameters.Add(parameter);
            command.Parameters.Add(parameter2);
            command.Parameters.Add(parameter3);

            context.Database.OpenConnection();

            var itens = command.ExecuteReader();
            var grafico = new List<GraficoColunaProdutorRamoAnoViewModel>();

            while (itens.Read())
            {
                var ent = new GraficoColunaProdutorRamoAnoViewModel()
                {
                    MesAno = itens.GetValue(0).ToString(),
                    Ramo = itens.GetValue(1).ToString(),
                    ValorPrejuizoMes = Convert.ToDecimal(itens.GetValue(2)),
                    EventosMes = Convert.ToInt32(itens.GetValue(3))
                };
                grafico.Add(ent);
            }
            return grafico;
        }

        /// <summary>
        /// Buscar todos os Ramos dos Sinistros e soma todos os Valores Total Prejuízo(ValorSinistro) de cada Ramo.
        /// </summary>
        /// <returns>Ramo e o Valor Total do Prejuízo de cada Ramo</returns>
        public List<GraficoPizzaAdministradorViewModel> SelectGraficoPizzaAdministrador()
        {
            var query = "sp_GraficoAdministradorPizzaAll";
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;

            context.Database.OpenConnection();

            var itens = command.ExecuteReader();
            var grafico = new List<GraficoPizzaAdministradorViewModel>();

            while (itens.Read())
            {
                var ent = new GraficoPizzaAdministradorViewModel()
                {
                    Ramo = itens.GetValue(0).ToString(),
                    ValorTotalPrejuizo = Convert.ToDecimal(itens.GetValue(1))
                };
                grafico.Add(ent);
            }
            return grafico;
        }

        /// <summary>
        /// Buscar as informações necessarias para o grafico em coluna do Administrador.
        /// </summary>
        /// <returns>retorna o Eventos por Ano, o total de Eventos, o Ano, o Ramo e o Total do Valor do Prejuízo Por Ano</returns>
        public List<GraficoColunaAdministradorViewModel> SelectGraficoColunaLinhaAdministrador()
        {
            var query = "sp_GraficoAdministradorColunaAll";
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;

            context.Database.OpenConnection();

            var itens = command.ExecuteReader();
            var grafico = new List<GraficoColunaAdministradorViewModel>();

            while (itens.Read())
            {
                var ent = new GraficoColunaAdministradorViewModel()
                {
                    Ano = itens.GetValue(0).ToString(),
                    ValorPrejuizoAno = Convert.ToDecimal(itens.GetValue(1)),
                    EventosAno = Convert.ToInt32(itens.GetValue(2))
                };
                grafico.Add(ent);
            }
            return grafico;
        }

        /// <summary>
        /// Buscar as informações necessarias para o grafico em coluna do Administrador pelo Ano do Sinistro.
        /// </summary>
        /// <param name="ano"></param> 
        /// <returns>retorna o Eventos por Ano, o total de Eventos, o Ano, o Ramo e o Total do Valor do Prejuízo Por Ano</returns>
        public List<GraficoColunaAdministradorAnoViewModel> SelectGraficoColunaLinhaAdministradorByAno(string ano)
        {
            var query = "sp_GraficoAdministradorColunaporAno";
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;
            var parameter = new SqlParameter("@ano", ano);

            command.Parameters.Add(parameter);

            context.Database.OpenConnection();

            var itens = command.ExecuteReader();
            var grafico = new List<GraficoColunaAdministradorAnoViewModel>();

            while (itens.Read())
            {
                var ent = new GraficoColunaAdministradorAnoViewModel()
                {
                    Ano = itens.GetValue(0).ToString(),
                    Ramo = itens.GetValue(1).ToString(),
                    ValorPrejuizoAno = Convert.ToDecimal(itens.GetValue(2)),
                    Eventos = Convert.ToInt32(itens.GetValue(3)),
                };
                grafico.Add(ent);
            }
            return grafico;
        }

        /// <summary>
        /// Buscar as informações necessarias para o grafico em coluna do Administrador pelo Ramo do Sinistro.
        /// </summary>
        /// <param name="ramo"></param> 
        /// <returns>retorna o Eventos por Ano, o total de Eventos, o Ano, o Ramo e o Total do Valor do Prejuízo Por Ano</returns>
        public List<GraficoColunaAdministradorRamoViewModel> SelectGraficoColunaLinhaAdministradorByRamo(string ramo)
        {
            var query = "sp_GraficoAdministradorColunaporRamo";
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;
            var parameter = new SqlParameter("@ramo", ramo);

            command.Parameters.Add(parameter);

            context.Database.OpenConnection();

            var itens = command.ExecuteReader();
            var grafico = new List<GraficoColunaAdministradorRamoViewModel>();

            while (itens.Read())
            {
                var ent = new GraficoColunaAdministradorRamoViewModel()
                {
                    Ano = itens.GetValue(0).ToString(),
                    Ramo = itens.GetValue(1).ToString(),
                    ValorPrejuizoAno = Convert.ToDecimal(itens.GetValue(2)),
                    EventosAno = Convert.ToInt32(itens.GetValue(3)),
                };
                grafico.Add(ent);
            }
            return grafico;
        }

        /// <summary>
        /// Buscar as informações necessarias para o grafico em coluna do Administrador pelo Ramo e o ano dos Sinistros.
        /// </summary>
        /// <param name="ramo"></param>
        /// /// <param name="ano"></param>
        /// <returns>retorna o Mês/Ano, o total valor prejuízo por Mês, Eventos Mês e os Eventos do Ano</returns>
        public List<GraficoColunaAdministradorRamoAnoViewModel> SelectGraficoColunaLinhaAdministradorByRamoAno(string ramo, string ano)
        {
            var query = "sp_GraficoAdministradorColunaPorRamoAno";
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.StoredProcedure;
            var parameter = new SqlParameter("@ramo", ramo);
            var parameter2 = new SqlParameter("@ano", ano);

            command.Parameters.Add(parameter);
            command.Parameters.Add(parameter2);

            context.Database.OpenConnection();

            var itens = command.ExecuteReader();
            var grafico = new List<GraficoColunaAdministradorRamoAnoViewModel>();

            while (itens.Read())
            {
                var ent = new GraficoColunaAdministradorRamoAnoViewModel()
                {
                    MesAno = itens.GetValue(0).ToString(),
                    Ramo = itens.GetValue(1).ToString(),
                    ValorPrejuizoMes = Convert.ToDecimal(itens.GetValue(2)),
                    EventosMes = Convert.ToInt32(itens.GetValue(3))
                };
                grafico.Add(ent);
            }
            return grafico;
        }
    }
}
