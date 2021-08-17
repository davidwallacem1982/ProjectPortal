using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portal.Api;
using Portal.Api.Filtro;
using Portal.Api.Interfaces;
using Portal.Api.Validacao.Verificacao;
using Portal.Core.Entities;
using Portal.Core.Identity;
using Portal.Core.JsonModel;
using System.Threading.Tasks;

namespace Portal.Web.Controllers
{
    public class ContatosController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppContatosPortal cp;
        private readonly IAppListaContatosPortal lp;
        private readonly IAppCargos cg;
        private readonly IAppUsuarios user;


        /// <summary>
        /// Construtor DocumentosController
        /// </summary>
        public ContatosController(IAppContatosPortal contatosPortal, IAppListaContatosPortal listaContatosPortal, IAppCargos cargo, IAppUsuarios usuarios, UserManager<ApplicationUser> userManager)
        {
            cp = contatosPortal;
            lp = listaContatosPortal;
            cg = cargo;
            user = usuarios;
            _userManager = userManager;
        }

        /// <summary>
        /// Inicial a View Index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PortalAuthorize(Roles = "ADMIN, PORTALCLIENTE, COMERCIAL")]
        public async Task<IActionResult> Index()
        {
            var retorno = cp.GetContatos();
            for (int i = 0; i < retorno.Count; i++)
            {
                var retornoItem = lp.GetListaContatosPortal(retorno[i].Id);
                for (int j = 0; j < retornoItem.Count; j++)
                {
                    if (!string.IsNullOrWhiteSpace(retornoItem[j].Telefone))
                    {
                        retornoItem[j].Telefone = UtilWeb.Mascaras.AplicarMascaraTelefone(retornoItem[j].Telefone);
                    }
                }
                retorno[i].Itens = retornoItem;
            }
            var usuario = await user.GetExisteUsuario(HttpContext.Session.GetString("UserName"));
            var tipo = await _userManager.IsInRoleAsync(usuario, "ADMIN");
            ViewBag.Tipo = tipo;

            return View(retorno);
        }

        /// <summary>
        /// GET View CadastroContatos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PortalAuthorize(Roles = "ADMIN")]
        public IActionResult CadastroContatos()
        {
            ViewBag.Grupo = cp.GetContatos();
            ViewBag.Cargo = cg.GetCargos();
            return View();
        }

        /// <summary>
        /// Post View CadastroContatos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [PortalAuthorize(Roles = "ADMIN")]
        public IActionResult CadastroContatos(ListaContatosPortal model)
        {
            var ModelErros = VerificaDadosContatos.VerificaCamposContatos(model);

            if (!string.IsNullOrWhiteSpace(model.Nome))
            {
                if (lp.GetNomeContatoPortal(model.Nome))
                {
                    ModelErros.Add("Esse contato já está cadastrado!");
                }
            }
            if (ModelErros.Count > 0)
            {
                return Json(new JsonReturnModels
                {
                    Ok = false,
                    Title = "",
                    ListMessage = ModelErros,
                    Type = TypeJsonReturn.Error
                });
            }
            else
            {
                model.Email = UtilApi.RemoveEspaco(model.Email);
                model.Telefone = UtilApi.RemoveNaoNumericos(model.Telefone);

                var retorno = lp.SalVarListaContatosPortal(model);

                return retorno > 0 ? Json(new JsonReturnModels
                {
                    Ok = true,
                    Title = "Sucesso",
                    Message = "O Contato foi Salvo com Sucesso",
                    Type = TypeJsonReturn.Error
                }) : Json(new JsonReturnModels
                {
                    Ok = false,
                    Title = "Sucesso",
                    Message = "Tente mais tarde",
                    Type = TypeJsonReturn.Error
                });
            }
        }

        /// <summary>
        /// GET View EditarContatos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PortalAuthorize(Roles = "ADMIN")]
        public IActionResult EditarContatos(long Id)
        {
            var model = lp.GetContatoPortal(Id);
            ViewBag.Grupo = cp.GetContatos();
            ViewBag.Cargo = cg.GetCargos();

            return View(model);
        }

        /// <summary>
        /// Post View EditarContatos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [PortalAuthorize(Roles = "ADMIN")]
        public IActionResult EditarContatos(ListaContatosPortal model)
        {
            var ModelErros = VerificaDadosContatos.VerificaCamposContatos(model);

            if (ModelErros.Count > 0)
            {
                return Json(new JsonReturnModels
                {
                    Ok = false,
                    Title = "",
                    ListMessage = ModelErros,
                    Type = TypeJsonReturn.Error
                });
            }
            else
            {
                model.Email = UtilApi.RemoveEspaco(model.Email);
                model.Telefone = UtilApi.RemoveNaoNumericos(model.Telefone);

                var retorno = lp.EditarListaContatosPortal(model);

                return retorno > 0 ? Json(new JsonReturnModels
                {
                    Ok = true,
                    Title = "Sucesso",
                    Message = "O Contato foi Editado com Sucesso",
                    Type = TypeJsonReturn.Error
                }) : Json(new JsonReturnModels
                {
                    Ok = false,
                    Title = "Sucesso",
                    Message = "Tente mais tarde",
                    Type = TypeJsonReturn.Error
                });
            }
        }

        #region Métodos Json

        /// <summary>
        /// Retorna os Títulos do Contatos.
        /// <returns>Lista de Títulos</returns>
        [PortalAuthorize(Roles = "ADMIN, PORTALCLIENTE, COMERCIAL")]
        public JsonResult JsonListarTitulosContatos()
        {
            var retorno = cp.GetContatos();
            return Json(retorno);
        }

        /// <summary>
        /// Retorna os Títulos do Contatos.
        /// <returns>Lista de Títulos</returns>
        [PortalAuthorize(Roles = "ADMIN, PORTALCLIENTE, COMERCIAL")]
        public JsonResult JsonListarCargos()
        {
            var retorno = cg.GetCargos();
            return Json(retorno);
        }

        #endregion
    }
}

