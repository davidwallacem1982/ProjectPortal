using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Filtro;
using Portal.Api.Interfaces;
using System;

namespace Portal.Web.Controllers
{
    public class SinistrosController : Controller
    {
        private readonly IAppSinistros appSinistros;

        /// <summary>
        /// Construtor SinistrosController
        /// </summary>
        public SinistrosController(IAppSinistros AppSinistros)
        {
            appSinistros = AppSinistros;
        }

        /// <summary>
        /// Inicial a View Index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PortalAuthorize(Roles = "PORTALCLIENTE")]
        public IActionResult Index()
        {
            ViewBag.versao = UtilWeb.Versao.PegarVersao();
            return View();
        }

        /// <summary>
        /// Inicial a View IndexProdutor
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PortalAuthorize(Roles = "COMERCIAL")]
        public IActionResult IndexProdutor()
        {
            ViewBag.versao = UtilWeb.Versao.PegarVersao();
            return View();
        }

        /// <summary>
        /// Inicial a View IndexAdmin
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PortalAuthorize(Roles = "ADMIN")]
        public IActionResult IndexAdmin()
        {
            ViewBag.versao = UtilWeb.Versao.PegarVersao();
            return View();
        }

        /// <summary>
        /// Retorna para a View as Informações do sinistro com base no Id do Sinistro.
        /// </summary>
        /// <returns>Informações do Sinistro</returns>
        [HttpGet]
        [PortalAuthorize(Roles = "ADMIN, PORTALCLIENTE, COMERCIAL")]
        public IActionResult VisualizarSinistro(long Id)
        {
            var result = appSinistros.GetInfoSinistroById(Id);

            return View(result);
        }

        #region Métodos Json

        /// <summary>
        /// Retorna para a View todas sinistros cadastrados no Banco
        /// </summary>
        /// <returns>Lista no formato Json de sinistros cadastrados</returns>
        [HttpPost]
        [PortalAuthorize(Roles = "ADMIN")]
        public JsonResult ListarSinistrosAll()
        {
            var result = appSinistros.GetListSinistrosAll();

            return Json(result);
        }

        /// <summary>
        /// Retorna para a View todas sinistros do Cliente(Segurado)
        /// </summary>
        /// A session ClientesID representa do ID do Cliente
        /// <returns>Lista no formato Json de sinistros do Cliente(segurado)</returns>
        [HttpPost]
        [PortalAuthorize(Roles = "PORTALCLIENTE")]
        public JsonResult ListarSinistrosCliente()
        {

            var Ambiente = HttpContext.Session.GetString("ClientesID");

            var result = appSinistros.GetListSinistrosClienteByclienteId(Convert.ToInt32(Ambiente));

            return Json(result);
        }

        /// <summary>
        /// Retorna para a View todos os sinistros dos Clientes do Produtor(Comercial)
        /// </summary>
        /// /// A session UsuarioID representa do ID do Usuário
        /// <returns>Lista no formato Json dos sinistros dos Clientes do Produtor(Comercial)</returns>
        [HttpPost]
        [PortalAuthorize(Roles = "COMERCIAL")]
        public JsonResult ListarSinistrosProdutor()
        {
            var Ambiente = HttpContext.Session.GetString("UsuarioID");

            var result = appSinistros.GetListSinistrosProdutorByusuarioID(Ambiente);

            return Json(result);
        }

        /// <summary>
        /// Retorna para o Grafico em Piza os Ramos e o Valor Prejúizo por Ramo do Cliente(Segurado)
        /// </summary>
        /// A session ClientesID representa do ID do Cliente
        /// <returns>Lista Ramos e o Valor Total do Prejuízo de cada Ramo</returns>
        [HttpPost]
        [PortalAuthorize(Roles = "PORTALCLIENTE")]
        public JsonResult GraficoPizzaSegurado()
        {

            var Ambiente = HttpContext.Session.GetString("ClientesID");

            var result = appSinistros.GetGraficoPizzaSeguradoByclienteId(Convert.ToInt32(Ambiente));

            return Json(result);
        }

        /// <summary>
        /// Retorna para a View todos Eventos por Ano, Total de Eventos, Ramo e o Total do Prejuízo por Ano do Sugurado(Cliente)
        /// </summary>
        /// A session ClienteID representa do ID do Cliente
        /// <returns>Lista no formato Json da model GraficoColunaSeguradoViewModel</returns>

        [HttpPost]
        [PortalAuthorize(Roles = "PORTALCLIENTE")]
        public JsonResult GraficoColunaLinhaSegurado()
        {
            var Ambiente = HttpContext.Session.GetString("ClientesID");

            var result = appSinistros.GetGraficoColunaLinhaSeguradoByclienteId(Convert.ToInt32(Ambiente));

            return Json(result);
        }

        /// <summary>
        /// Retorna para a View o  Ano , Total do Prejuízo por Ramo e Eventos e Total de Eventos por Ano do Segurado(Cliente)
        /// </summary>
        /// /// A session ClienteID representa do ID do Cliente
        /// <param name="ano"></param>
        /// <returns>Lista no formato Json da model GraficoColunaAdministradorAnoViewModel</returns>

        [HttpPost]
        [PortalAuthorize(Roles = "PORTALCLIENTE")]
        public JsonResult GraficoColunaLinhaSeguradoAno(string ano)
        {
            var Ambiente = HttpContext.Session.GetString("ClientesID");

            var result = appSinistros.GetGraficoColunaSeguradoByclienteIdAno(Convert.ToInt32(Ambiente), ano);

            return Json(result);
        }

        /// <summary>
        /// Retorna para a View todos Eventos por Ano, Total de Eventos, Ramo e o Total do Prejuízo por Ano do Segurado(Cliente)
        /// </summary>
        /// A session ClienteID representa do ID do Cliente
        /// <param name="ramo"></param>
        /// <returns>Lista no formato Json da model GraficoColunaSeguradoViewModel</returns>

        [HttpPost]
        [PortalAuthorize(Roles = "PORTALCLIENTE")]
        public JsonResult GraficoColunaLinhaSeguradoRamo(string ramo)
        {
            var Ambiente = HttpContext.Session.GetString("ClientesID");

            var result = appSinistros.GetGraficoColunaLinhaSeguradoByclienteIdRamo(Convert.ToInt32(Ambiente), ramo);

            return Json(result);
        }

        /// <summary>
        /// Retorna para a View todas as Data do Sinistro(Mes/Ano), Total do Valor do Prejuízo por Mês, o Ano, Eventos por Mês e o Total de Eventos por Ano do Segurado(Cliente)
        /// </summary>
        /// A session ClienteID representa do ID do Cliente
        /// <param name="ramo"></param>
        /// <param name="ano"></param>
        /// <returns>Lista no formato Json da model GraficoColunaSeguradoRamoAnoViewModel</returns>

        [HttpPost]
        [PortalAuthorize(Roles = "PORTALCLIENTE")]
        public JsonResult GraficoColunaLinhaSeguradoRamoAno(string ramo, string ano)
        {
            var Ambiente = HttpContext.Session.GetString("ClientesID");

            var result = appSinistros.GetGraficoColunaLinhaSeguradoByclienteIdRamoAno(Convert.ToInt32(Ambiente), ramo, ano);

            return Json(result);
        }

        /// <summary>
        /// Retorna para o Grafico em Pizza os Ramos e o Valor Prejúizo por Ramo dos Segurados(Clientes) do Produtor(Comercial)
        /// </summary>
        /// A session UsuarioID representa do ID do usuário
        /// <returns>Lista Ramos e o Valor Total do Prejuízo de cada Ramo</returns>

        [HttpPost]
        [PortalAuthorize(Roles = "COMERCIAL")]
        public JsonResult GraficoPizzaProdutor()
        {

            var Ambiente = HttpContext.Session.GetString("UsuarioID");

            var result = appSinistros.GetGraficoPizzaProdutorByusuarioId(Ambiente);

            return Json(result);
        }

        /// <summary>
        /// Retorna para a View todos Eventos por Ano, Total de Eventos, Ramo e o Total do Prejuízo por Ano dos Segurados(Clientes) do Produtor(Comercial)
        /// </summary>
        /// A session UsuarioID representa do ID do usuário
        /// <returns>Lista no formato Json da model GraficoColunaSeguradoViewModel</returns>
        [HttpPost]
        [PortalAuthorize(Roles = "COMERCIAL")]
        public JsonResult GraficoColunaLinhaProdutor()
        {
            var Ambiente = HttpContext.Session.GetString("UsuarioID");

            var result = appSinistros.GetGraficoColunaLinhaProdutorByusuarioId(Ambiente);

            return Json(result);
        }

        /// <summary>
        /// Retorna para a View o  Ano , Total do Prejuízo por Ramo e Eventos e Total de Eventos por Ano dos Segurados(Clientes) do Produtor
        /// </summary>
        /// A session UsuarioID representa do ID do usuário
        /// <param name="ano"></param>
        /// <returns>Lista no formato Json da model GraficoColunaAdministradorAnoViewModel</returns>

        [HttpPost]
        [PortalAuthorize(Roles = "COMERCIAL")]
        public JsonResult GraficoColunaLinhaProdutorAno(string ano)
        {
            var Ambiente = HttpContext.Session.GetString("UsuarioID");

            var result = appSinistros.GetGraficoColunaProdutorByusuarioIdAno(Ambiente, ano);

            return Json(result);
        }

        /// <summary>
        /// Retorna para a View todos Eventos por Ano, Total de Eventos, Ramo e o Total do Prejuízo por Ano dos Segurados(Clientes) do Produtor(Comercial)
        /// </summary>
        /// A session UsuarioID representa do ID do usuário
        /// <param name="ramo"></param>
        /// <returns>Lista no formato Json da model GraficoColunaSeguradoViewModel</returns>

        [HttpPost]
        [PortalAuthorize(Roles = "COMERCIAL")]
        public JsonResult GraficoColunaLinhaProdutorRamo(string ramo)
        {
            var Ambiente = HttpContext.Session.GetString("UsuarioID");

            var result = appSinistros.GetGraficoColunaLinhaProdutorByusuarioIdRamo(Ambiente, ramo);

            return Json(result);
        }

        /// <summary>
        /// Retorna para a View todas as Data do Sinistro(Mes/Ano), Total do Valor do Prejuízo por Mês, o Ano, Eventos por Mês e o Total de Eventos por Ano dos Segurados(Clientes) do Produtor(Comercial)
        /// </summary>
        /// A session UsuarioID representa do ID do usuário
        /// <param name="ramo"></param>
        /// <param name="ano"></param>
        /// <returns>Lista no formato Json da model GraficoColunaSeguradoRamoAnoViewModel</returns>

        [HttpPost]
        [PortalAuthorize(Roles = "COMERCIAL")]
        public JsonResult GraficoColunaLinhaProdutorRamoAno(string ramo, string ano)
        {
            var Ambiente = HttpContext.Session.GetString("UsuarioID");

            var result = appSinistros.GetGraficoColunaLinhaProdutorByusuarioIdRamoAno(Ambiente, ramo, ano);

            return Json(result);
        }

        /// <summary>
        /// Retorna para o Grafico em Pizza os Ramos e o Valor Prejúizo por Ramo todos os Segurados(Clientes)
        /// </summary>
        /// <returns>Lista Ramos e o Valor Total do Prejuízo de cada Ramo</returns>

        [HttpPost]
        [PortalAuthorize(Roles = "ADMIN")]
        public JsonResult GraficoPizzaAdministrador()
        {

            var result = appSinistros.GetGraficoPizzaAdministrador();

            return Json(result);
        }

        /// <summary>
        /// Retorna para a View todos Eventos por Ano, Total de Eventos, Ramo e o Total do Prejuízo por Ano de todos os Segurados(Clientes)
        /// </summary>
        /// <returns>Lista no formato Json da model GraficoColunaSeguradoViewModel</returns>
        [HttpPost]
        [PortalAuthorize(Roles = "ADMIN")]
        public JsonResult GraficoColunaLinhaAdministrador()
        {

            var result = appSinistros.GetGraficoColunaLinhaAdministrador();

            return Json(result);
        }

        /// <summary>
        /// Retorna para a View o  Ano , Total do Prejuízo por Ramo e Eventos e Total de Eventos por Ano de todos os Segurados(Clientes)
        /// </summary>
        /// <param name="ano"></param>
        /// <returns>Lista no formato Json da model GraficoColunaAdministradorAnoViewModel</returns>

        [HttpPost]
        [PortalAuthorize(Roles = "ADMIN")]
        public JsonResult GraficoColunaLinhaAdministradorAno(string ano)
        {

            var result = appSinistros.GetGraficoColunaLinhaAdministradorByAno(ano);

            return Json(result);
        }

        /// <summary>
        /// Retorna para a View todos Eventos por Ano, Total de Eventos, Ramo e o Total do Prejuízo por Ano de todos os Segurados(Clientes)
        /// </summary>
        /// <param name="ramo"></param>
        /// <returns>Lista no formato Json da model GraficoColunaSeguradoViewModel</returns>

        [HttpPost]
        [PortalAuthorize(Roles = "ADMIN")]
        public JsonResult GraficoColunaLinhaAdministradorRamo(string ramo)
        {

            var result = appSinistros.GetGraficoColunaLinhaAdministradorByRamo(ramo);

            return Json(result);
        }

        /// <summary>
        /// Retorna para a View todas as Data do Sinistro(Mes/Ano), Total do Valor do Prejuízo por Mês, o Ano, Eventos por Mês e o Total de Eventos por Ano de todos os Segurados(Clientes)
        /// </summary>
        /// <param name="ramo"></param>
        /// <param name="ano"></param>
        /// <returns>Lista no formato Json da model GraficoColunaSeguradoRamoAnoViewModel</returns>

        [HttpPost]
        [PortalAuthorize(Roles = "ADMIN")]
        public JsonResult GraficoColunaLinhaAdministradorRamoAno(string ramo, string ano)
        {

            var result = appSinistros.GetColunaLinhaAdministradorByRamoAno(ramo, ano);

            return Json(result);
        }

        #endregion
    }
}