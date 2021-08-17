using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Portal.Api.Filtro;
using Portal.Core.AzureStorage;
using Portal.Storage;

namespace Portal.Web.Controllers
{
    public class PerfilController : Controller
    {
        private readonly StorageConfig _storageConfig;

        /// <summary>
        /// Construtor DocumentosController
        /// </summary>
        public PerfilController(IConfiguration configuration)
        {
            _storageConfig = new StorageConfig();
            configuration.GetSection("StorageConfig").Bind(_storageConfig);
        }
        /// <summary>
        /// Inicial a View Index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [PortalAuthorize(Roles = "ADMIN, PORTALCLIENTE, COMERCIAL")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Salva o logo do usuário no Storage
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [PortalAuthorize(Roles = "ADMIN, PORTALCLIENTE, COMERCIAL")]
        public IActionResult UploadLogo(IFormFile file)
        {
            if (file != null)
            {

                var format = file.FileName.Trim('\"');

                var UsuarioID = HttpContext.Session.GetString("UsuarioID");
                var tabela = "logo";
                if (file.Length > 1000000) // 1 MB
                {
                    TempData["ReturnLogo"] = "Tamanho da Imagem excedeu o limite! (1 MB)";
                    return RedirectToAction("Index", "Perfil");
                }
                else
                {
                    var result = new JsonArquivo();
                    var azst = new StorageHelper(_storageConfig);


                    if (file.Length > 0)
                    {
                        if (azst.IsImage(format))
                        {
                            using var stream = file.OpenReadStream();
                            var nome = UsuarioID + ".jpg";
                            result = azst.UploadAsync(stream, nome, tabela).Result;
                        }
                        else
                        {
                            TempData["ReturnLogo"] = "Está extensão de arquivo não é aceita para Logo!";

                            return RedirectToAction("Index", "Perfil");
                        }
                    }
                    if (result.Retorno)
                    {
                        HttpContext.Session.SetString("PathLogo", result.Url);
                    }
                    else
                    {
                        HttpContext.Session.SetString("PathLogo", "/img/profile-photos/1.png");
                    }
                }

                return RedirectToAction("Index", "Perfil");
            }
            else
            {
                TempData["ReturnLogo"] = "Erro ao carregar a imagem!";
                return RedirectToAction("Index", "Perfil");
            }
        }
    }
}
