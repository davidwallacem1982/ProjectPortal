using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Portal.Core.Identity;
using Portal.Core.Models;
using Portal.Core.Storage;
using Portal.Storage.Util;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Portal.Storage
{
    /// <summary>
    /// Classe que representa as Table que estão no Storage do Azure
    /// </summary>
    public class AzureLogStorage
    {
        /// <summary>
        ///Insere ou Atualiza um Registro
        ///</summary>
        ///<param name="tableName">Nome da Tabela</param>
        ///<param name="entity">Entidade a ser Inserida/Alterada</param>
        ///<returns></returns>
        ///<remarks></remarks>
        public async Task<TableResult> InsertEntityTableAsync(string tableName, TableEntity entity)
        {
            // Retrieve storage account from connection-string.
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(ConnectionStorage.Connection);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            CloudTable cloudTable = tableClient.GetTableReference(tableName);
            var tableService = ServicePointManager.FindServicePoint(storageAccount.TableEndpoint);
            tableService.UseNagleAlgorithm = false;

            // Create the container if it doesn't already exist.
            await cloudTable.CreateIfNotExistsAsync();

            // table.CreateIfNotExists()
            UtilStorage.CompressDataProp(entity);

            TableOperation insertOrReplaceOperation;
            //if (Update)
            //    insertOrReplaceOperation = TableOperation.InsertOrReplace(entity);
            //else
            //    insertOrReplaceOperation = TableOperation.Insert(entity);
            insertOrReplaceOperation = TableOperation.InsertOrReplace(entity);

            return await cloudTable.ExecuteAsync(insertOrReplaceOperation);
        }

        public async Task<List<LoginUser>> QueryUsuarioLogadoAsync(string tableName, string RowKey)
        {
            // Retrieve storage account from connection-string.
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse(ConnectionStorage.Connection);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create a cloud table object for the table.
            CloudTable cloudTable = tableClient.GetTableReference(tableName);

            // Create a filter condition where the partition key e RoRowKey e se o mesmo está logado.
            //var filterPartitionKey = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, PartitionKey);
            var filterRowKey = TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, RowKey);
            var filterLogado = TableQuery.GenerateFilterCondition("Logado", QueryComparisons.Equal, "1");

            var query = new TableQuery<LoginUser>()
            .Where(TableQuery.CombineFilters(filterRowKey, TableOperators.And, filterLogado));
            //.Where(TableQuery.CombineFilters(TableQuery.CombineFilters(filterPartitionKey, TableOperators.And, filterRowKey), TableOperators.And, filterLogado));

            TableContinuationToken token = null;
            var entities = new List<LoginUser>();
            do
            {
                var result = await cloudTable.ExecuteQuerySegmentedAsync(query, token);
                entities.AddRange(result.Results);
                token = result.ContinuationToken;

            } while (token != null);
            return entities;
        }

        //public CloudTableClient tableClient;
        public async Task<bool> SaveHistoricoUsuario(ActionExecutingContext filterContext, ApplicationUser userAmbiente, bool userCkeck)
        {
            //var userAmbiente = GetUser();
            //var result = await GetUserCkeck();
            if (userCkeck)
            {
                var _historico = new HistoricosUsuarios(userAmbiente.Id);
                {
                    var withBlock = _historico;
                    withBlock.Controller = filterContext.Controller.GetType().ToString();
                    withBlock.Metodo = filterContext.ActionDescriptor.DisplayName;
                    withBlock.MetodoHTTP = filterContext.HttpContext.Request.Method;
                    withBlock.Navegador = "";//filterContext.HttpContext.Current.Request.Browser + " Version = " + filterContext.HttpContext.Request.Browser.Version.ToString;
                    withBlock.Uri = UtilStorage.ShowURL(filterContext);
                    withBlock.Data = filterContext.HttpContext.Request.Form.ToString();
                    withBlock.Detalhes = filterContext.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase;
                    withBlock.DataAcesso = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "E. South America Standard Time");

                    if (filterContext.Controller.GetType().Name != null)
                        withBlock.Titulo = filterContext.Controller.GetType().Name;
                    else
                        withBlock.Titulo = "";
                }
                await InsertEntityTableAsync("HistoricosUsuarios", _historico);
                //return saveHistoricoUsuarioCounts(_historico);
            }
            return userCkeck;
        }

        //public bool saveHistoricoUsuarioCounts(HistoricosUsuarios _historico)
        //{
        //    var bloq = false;
        //    var dia = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "E. South America Standard Time");
        //    var userAmbiente = GetUser();

        //    var HttpMethodsID = _regrasAcessos.ActionByAction(_historico.Controller, _historico.Metodo, _historico.MetodoHTTP).Value;
        //    var regras = getRegrasAlertasEmail(_historico.Controller, _historico.Metodo, _historico.MetodoHTTP);
        //    List<HistoricoUsuariosCounts> historicos;
        //    HistoricoUsuariosCounts historico;
        //    var regrasacesso = regras.Where(a => a.QuantRegistrosMinimo == null && a.QuantRegistrosMaximo == null
        //                                             && a.QuantRegistrosTotal == null).ToList;
        //    var dataInicio = new DateTime(dia.Year, dia.Month, dia.Day, 0, 0, 0);
        //    var dataFim = new DateTime(dia.Year, dia.Month, dia.Day, 0, 0, 0);

        //    if (regrasacesso.Count > 0 && regrasacesso.Where(a => !a.TempoDia == null).Count > 0)
        //        dataInicio = dataInicio.AddDays(-System.Convert.ToInt32(regrasacesso.Where(a => !a.TempoDia == null).Max(a => a.TempoDia).Value.ToString));

        //    var IP = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(); // System.Web.HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        //    if (IP == "" | IP == null)
        //        IP = "::1";

        //    if (IP == "::1")
        //    {
        //        dataFim = dataFim.AddHours(-3);
        //        dataInicio = dataInicio.AddHours(-3);
        //    }

        //    historicos = QueryHistorioUsuarioCount<HistoricoUsuariosCounts>("HistoricoUsuariosCounts", userAmbiente.Id, HttpMethodsID, dataInicio, dataFim);

        //    if (IP == "::1")
        //        dataFim = dataFim.AddHours(+3);
        //    historico = historicos.Where(a => a.DataAcesso == dataFim).FirstOrDefault();

        //    if (historico == null)
        //    {
        //        historico = new HistoricoUsuariosCounts(userAmbiente.Id);
        //        {
        //            var withBlock = historico;
        //            withBlock.UltimoHistoricoID = _historico.RowKey;
        //            withBlock.MetodoHTTPID = HttpMethodsID;
        //            withBlock.Quant = 1;
        //            withBlock.DataAcesso = new DateTime(dia.Year, dia.Month, dia.Day);
        //        }

        //        Azure.InsertEntity("HistoricoUsuariosCounts", historico, false);
        //        historicos.Add(historico);
        //    }
        //    else
        //    {
        //        {
        //            var withBlock = historico;
        //            withBlock.Quant += 1;
        //            withBlock.UltimoHistoricoID = _historico.RowKey;
        //        }
        //        Azure.InsertEntity("HistoricoUsuariosCounts", historico, true);
        //        historicos.Add(historico);
        //    }

        //    if (!regrasacesso == null)
        //        bloq = VerificacaoAcesso(regrasacesso, historicos, _historico);
        //    return bloq;
        //}

        //public List<RegrasEnvioEmailsHistoricoViewModel> getRegrasAlertasEmail(string Controle, string Metodo, string MetodoHTTP)
        //{
        //    var userAmbiente = GetUser();
        //    if (userAmbiente != null)
        //    {
        //        var actionID = _regrasAcessos.ActionByAction(Controle, Metodo, MetodoHTTP);

        //        var regraEmail = regraEmail1.ToList;

        //        List<RegrasEnvioEmailsHistoricoModels> regras = new List<RegrasEnvioEmailsHistoricoModels>();

        //        foreach (var i in bd.ExcecaoRegrasAlertaEmails.Where(a => (a.Referencia == "HttpMethods" && a.ReferenciaID == actionID.Value) || (a.Referencia == "usuario" && a.ReferenciaID == amb.usuarioID)).ToList)
        //        {
        //            foreach (var e in regraEmail.Where(a => a.RegrasEnvioEmailsID == i.RegrasEnvioEmailsID).ToList)
        //                regraEmail.Remove(e);
        //        }

        //        foreach (var i in regraEmail.Where(a => System.Web.HttpContext.Current.User.IsInRole(a.RoleExcecao)).ToList)
        //        {
        //            foreach (var e in regraEmail.Where(a => a.RegrasEnvioEmailsID == i.RegrasEnvioEmailsID).ToList)
        //                regraEmail.Remove(e);
        //        }

        //        regras.AddRange(regraEmail.Where(a => a.Role == null).ToList);

        //        foreach (var item in regraEmail.Where(a => a.Role != null).ToList)
        //        {
        //            if (System.Web.HttpContext.Current.User.IsInRole(item.Role))
        //                regras.Add(item);
        //        }
        //        Debug.WriteLine("Consulta Regras de Alertas:" + regras.Count.ToString());
        //        return regras;
        //    }
        //    else
        //        return new List<RegrasEnvioEmailsHistoricoModels>();
        //}
    }
}