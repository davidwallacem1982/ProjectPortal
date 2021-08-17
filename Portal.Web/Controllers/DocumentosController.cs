using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Portal.Api.Filtro;
using Portal.Api.Interfaces;
using Portal.Core.AzureStorage;
using Portal.Core.Models;
using Portal.Storage;
using System.Collections.Generic;
using System.IO;

namespace Portal.Web.Controllers
{
    public class DocumentosController : Controller
    {

        private readonly IAppArquivos arq;
        private readonly IAppTiposArquivos tpArquivos;
        private readonly StorageConfig _storageConfig;

        /// <summary>
        /// Construtor DocumentosController
        /// </summary>
        public DocumentosController(IAppArquivos AppArquivos, IAppTiposArquivos TpArquivos, IConfiguration configuration)
        {
            arq = AppArquivos;
            tpArquivos = TpArquivos;
            _storageConfig = new StorageConfig();
            configuration.GetSection("StorageConfig").Bind(_storageConfig);
        }

        /// <summary>
        /// Inicial a View Index
        /// </summary>
        /// <param name="sinistrosId">Identificação única do Sinistro</param>
        /// <param name="protocolo">Protocolo do Sinistro</param>
        /// <param name="reposta"></param>
        /// <returns></returns>
        [HttpGet]
        [PortalAuthorize(Roles = "ADMIN, PORTALCLIENTE, COMERCIAL")]
        public IActionResult Index(long sinistrosId, long protocolo, int reposta = 0)
        {
            if (sinistrosId != 0)
            {
                ViewBag.ID = sinistrosId;
                ViewBag.Protocolo = protocolo;
                ViewBag.Resposta = reposta;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        /// <summary>
        /// Metodo para Enviar os Documentos para no Storage e salva o caminho no banco de dados
        /// </summary>
        /// <param name="files">Lista de Documentos a serem salvos</param>
        /// <param name="tiposArquivosId">Identificação única do Tipo de Arquivo</param>
        /// <param name="sinistrosId">Identificação única do Sinistro</param>
        /// <param name="protocolo">Número de Protocolo do Sinistro</param>
        /// <param name="model">Modelo de Arquivos do Sinistro</param>
        /// <returns></returns>
        [HttpPost]
        [PortalAuthorize(Roles = "ADMIN, PORTALCLIENTE, COMERCIAL")]
        public IActionResult EnviarDocumentos(List<IFormFile> files, long tiposArquivosId, int sinistrosId, long protocolo, ArquivosSinistroViewModel model)
        {
            if (files.Count != 0)
            {
                if (tiposArquivosId != 0)
                {
                    var controller = 1;
                    var Ambiente = HttpContext.Session.GetString("UserName");
                    var tabela = "sinistros";

                    foreach (var arquivo in files)
                    {
                        var cont2 = 1;
                        var tipo = tpArquivos.GetFirstTiposArquivosById(tiposArquivosId);
                        var cont = arq.GetCountByArquivosAprovado(tipo.Tipo, controller, sinistrosId);

                        // Define um nome para o arquivo enviado incluindo o sufixo obtido de milesegundos
                        var nomeArquivoUrl = arq.NomearArquivo(arquivo, tiposArquivosId, sinistrosId);
                        var stream = new MemoryStream();

                        arquivo.CopyTo(stream);

                        var storage = new StorageHelper(_storageConfig);
                        var result = storage.UploadAsync(stream, nomeArquivoUrl, tabela);

                        if (cont > 0)
                        {
                            while (cont2 <= cont)
                            {
                                cont2 = cont + 1;
                                model.Sequencial = cont2;
                            }
                        }
                        else
                        {
                            model.Sequencial = cont2;
                        }

                        model.Titulo = tipo.Nome;
                        model.Substituido = 0;
                        model.Extensao = UtilWeb.VerificaTipoArquivo.VerificaTipo(arquivo.ContentType);
                        model.Tipo = tipo.Tipo;
                        model.DataHora = UtilWeb.DataHora.Agora;
                        model.ControllerID = controller;
                        model.SinistrosID = sinistrosId;
                        model.UserName = Ambiente;
                        model.url = nomeArquivoUrl;

                        var contRep = arq.GetCountByArquivosReprovado(tipo.Tipo, controller, sinistrosId, model.Sequencial);

                        while ((contRep > 0))
                        {
                            var ArqRep = arq.GetByArquivosRepetido(model.Tipo, model.Sequencial, 3, 0);

                            ArqRep.Substituido = 1;
                            arq.AtualizarArquivo(ArqRep);

                            contRep -= 1;
                        }

                        model.UserName = Ambiente;
                        model.Status = 2;

                        arq.SalvarArquivo(model);
                    }
                    return RedirectToAction(nameof(Index), new { sinistrosId, protocolo, reposta = 1 });
                }
                else
                {
                    return RedirectToAction(nameof(Index), new { sinistrosId, protocolo, reposta = 3 });
                }

            }
            else
            {
                return RedirectToAction(nameof(Index), new { sinistrosId, protocolo, reposta = 2 });
            }
        }

        #region Métodos Json

        /// <summary>
        /// Retorna Tipos de Arquivos.
        /// <returns>Lista todos os Tipos de Arquivos</returns>
        [PortalAuthorize(Roles = "ADMIN, PORTALCLIENTE, COMERCIAL")]
        public JsonResult JsonListarTiposArquivos()
        {
            return Json(tpArquivos.RetornarListaTiposArquivos());
        }

        /// <summary>
        /// Retorna Documentos do Sinistro.
        /// <returns>Lista com os Documentos do Sinistro</returns>
        [PortalAuthorize(Roles = "ADMIN, PORTALCLIENTE, COMERCIAL")]
        public JsonResult JsonDocumentosSinistro(long sinistrosId)
        {
            return Json(arq.GetByDocumentosSinistro(sinistrosId));
        }

        /// <summary>
        /// Retorna Documentos Pendentes.
        /// <returns>Lista com os Documentos Pendentes</returns>
        [PortalAuthorize(Roles = "ADMIN, PORTALCLIENTE, COMERCIAL")]
        public JsonResult JsonDocumentosPendentes(long sinistrosId)
        {
            return Json(arq.GetByDocumentosPendentes(sinistrosId));
        }

        #endregion
    }
}