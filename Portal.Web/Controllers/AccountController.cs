using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Portal.Api.Interfaces;
using Portal.Core.AzureStorage;
using Portal.Core.Identity;
using Portal.Storage;
using Portal.Web.Models.AccountViewModels;
using Portal.Web.Services;
using Portal.Web.UtilWeb;
using System;
using System.Threading.Tasks;

namespace Portal.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAppLog _appLog;
        private readonly IAppUsuarios _usuarios;
        private readonly IAppClientes _clientes;
        private readonly IEmailSender _emailSender;
        private readonly MakeEmail _makeEmail;
        private readonly StorageConfig _storageConfig;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAppLog appLog,
            IAppUsuarios usuarios,
            IAppClientes AppClientes,
            IEmailSender emailSender,
            MakeEmail makeEmail,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appLog = appLog;
            _usuarios = usuarios;
            _clientes = AppClientes;
            _emailSender = emailSender;
            _makeEmail = makeEmail;
            _storageConfig = new StorageConfig();
            configuration.GetSection("StorageConfig").Bind(_storageConfig);
        }

        //[TempData]
        //8public string ErrorMessage { get; set; }

        /// <summary>
        /// Inicial a View
        /// </summary>
        /// <param name="returnUrl">retorno da Url</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {

            // Limpa o cookie externo existente para garantir um processo de login limpo
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        /// <summary>
        /// Método para fazer o login do usuário no Sistema do Portal.
        /// </summary>
        /// <param name="model">Model LoginViewModel com os dados do Login</param>
        /// <param name="returnUrl">retorno da Url</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                //Pagar essa linha abaixo e use o método GetExisteUsuario que está na AppUsuarios passando o model.UserName
                var user = await _usuarios.GetExisteUsuario(model.UserName);

                if (user != null)
                {
                    var administrador = await _userManager.IsInRoleAsync(user, "ADMIN");
                    var segurado = await _userManager.IsInRoleAsync(user, "PORTALCLIENTE");
                    var comercial = await _userManager.IsInRoleAsync(user, "COMERCIAL");

                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        return Json(new { ok = false, message = "Você ainda não confirmou o usuário com e-mail que enviamos, confirme o seu usuário ou clique Reenviar Email!", reenviar = true });
                    }
                    if (segurado)
                    {
                        //Esse if é para verificar se o Acesso do usuário é PORTALCLIENTE e
                        //não deixar o mesmo acessar o sistema por que no TNIX o mesmo tem
                        //a role PORTALCLIENTE mas o Acesso dele é diferenciado a não pode
                        //acessar o Portal Trade por que se não o mesmo não vai conseguir 
                        //acessar mais o TNIX, assim que migrar todo o TNIX para o Portal
                        // retirar esse if.
                        if (user.TipoAcessoID == 19)
                        {
                            if (user.LockoutEnabled)
                            {

                                var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

                                if (result.Succeeded)
                                {
                                    try
                                    {
                                        HttpContext.Session.SetString("ClientesID", user.ClientesID.ToString());
                                        var cliente = _clientes.GetNomeCliente(user.ClientesID);

                                        HttpContext.Session.SetString("ClientesNome", cliente);
                                        HttpContext.Session.SetString("Nome", user.Nome);
                                        HttpContext.Session.SetString("UserName", user.UserName);
                                        HttpContext.Session.SetString("UserEmail", user.Email);
                                        HttpContext.Session.SetString("UsuarioID", user.Id);

                                        var storage = new StorageHelper(_storageConfig);
                                        var retorno = storage.ExistsAsync(user.Id + ".jpg", "logo");

                                        if (retorno.Result.Retorno)
                                        {
                                            HttpContext.Session.SetString("PathLogo", retorno.Result.Url);
                                        }
                                        else
                                        {
                                            HttpContext.Session.SetString("PathLogo", "/img/profile-photos/1.png");
                                        }

                                        if (returnUrl != null)
                                        {
                                            return RedirectToLocal(returnUrl);
                                        }
                                        else
                                        {
                                            TempData["Acesso"] = "";

                                            if (user.Acesso == false)
                                            {
                                                TempData["Acesso"] = false;

                                                await _usuarios.UpdateAcesso(user);

                                                HttpContext.Session.SetString("DefaultHome", "/sinistros/index");

                                                _appLog.SaveLogin(user);

                                                return Json(new { ok = true, message = "/sinistros/index" });
                                            }
                                            else
                                            {
                                                TempData["Acesso"] = true;

                                                HttpContext.Session.SetString("DefaultHome", "/sinistros/index");

                                                _appLog.SaveLogin(user);

                                                return Json(new { ok = true, message = "/sinistros/index" });
                                            }


                                        }

                                    }
                                    catch (Exception)
                                    {
                                        return Json(new { ok = false, message = "Ambiente não localizado!" });
                                    }
                                }
                                if (result.IsLockedOut)
                                {
                                    return Json(new { ok = false, message = "Conta de usuário bloqueada!" });
                                }
                                else
                                {
                                    return Json(new { ok = false, message = "Senha Inválida!" });
                                }
                            }
                            else
                            {
                                return Json(new { ok = false, message = "Você não tem mais acesso ao Portal Trade Vale!" });
                            }
                        }
                        else
                        {
                            return Json(new { ok = false, message = "Desculpe, você não tem o acesso de Segurado!" });
                        }
                    }
                    else if (comercial)
                    {
                        //Esse if é para verificar se o Acesso do usuário é COMERCIAL e
                        //não deixar o mesmo acessar o sistema por que no TNIX o mesmo tem
                        //a role COMERCIAL mas o Acesso dele é diferenciado a não pode
                        //acessar o Portal Trade por que se não o mesmo não vai conseguir 
                        //acessar mais o TNIX, assim que migrar todo o TNIX para o Portal
                        // retirar esse if.
                        if (user.TipoAcessoID == 27)
                        {
                            if (user.LockoutEnabled)
                            {

                                var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

                                if (result.Succeeded)
                                {
                                    try
                                    {
                                        HttpContext.Session.SetString("ClientesNome", "Trade Vale");
                                        HttpContext.Session.SetString("Nome", user.Nome);
                                        HttpContext.Session.SetString("UserName", user.UserName);
                                        HttpContext.Session.SetString("UserEmail", user.Email);
                                        HttpContext.Session.SetString("UsuarioID", user.Id);

                                        var storage = new StorageHelper(_storageConfig);

                                        var retorno = storage.ExistsAsync(user.Id + ".jpg", "logo");

                                        if (retorno.Result.Retorno)
                                        {
                                            HttpContext.Session.SetString("PathLogo", retorno.Result.Url);
                                        }
                                        else
                                        {
                                            HttpContext.Session.SetString("PathLogo", "/img/profile-photos/1.png");
                                        }

                                        if (returnUrl != null)
                                        {
                                            return RedirectToLocal(returnUrl);
                                        }
                                        else
                                        {
                                            TempData["Acesso"] = "";

                                            if (user.Acesso == false)
                                            {
                                                TempData["Acesso"] = false;

                                                await _usuarios.UpdateAcesso(user);

                                                HttpContext.Session.SetString("DefaultHome", "/sinistros/IndexProdutor");

                                                _appLog.SaveLogin(user);

                                                return Json(new { ok = true, message = "/sinistros/IndexProdutor" });
                                            }
                                            else
                                            {
                                                TempData["Acesso"] = true;

                                                HttpContext.Session.SetString("DefaultHome", "/sinistros/IndexProdutor");

                                                _appLog.SaveLogin(user);

                                                return Json(new { ok = true, message = "/sinistros/IndexProdutor" });
                                            }


                                        }

                                    }
                                    catch (Exception)
                                    {
                                        return Json(new { ok = false, message = "Ambiente não localizado!" });
                                    }
                                }
                                if (result.IsLockedOut)
                                {
                                    return Json(new { ok = false, message = "Conta de usuário bloqueada!" });
                                }
                                else
                                {
                                    return Json(new { ok = false, message = "Senha Inválida!" });
                                }
                            }
                            else
                            {
                                return Json(new { ok = false, message = "Você não tem mais acesso ao Portal Trade Vale!" });
                            }
                        }
                        else
                        {
                            return Json(new { ok = false, message = "Desculpe, você não tem o acesso de Comercial!" });
                        }
                    }
                    else if (administrador)
                    {
                        if (user.TipoAcessoID == 1)
                        {
                            if (user.LockoutEnabled)
                            {

                                var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

                                if (result.Succeeded)
                                {
                                    try
                                    {
                                        HttpContext.Session.SetString("ClientesNome", "Trade Vale");
                                        HttpContext.Session.SetString("Nome", user.Nome);
                                        HttpContext.Session.SetString("UserName", user.UserName);
                                        HttpContext.Session.SetString("UserEmail", user.Email);
                                        HttpContext.Session.SetString("UsuarioID", user.Id);

                                        var storage = new StorageHelper(_storageConfig);

                                        var retorno = storage.ExistsAsync(user.Id + ".jpg", "logo");

                                        if (retorno.Result.Retorno)
                                        {
                                            HttpContext.Session.SetString("PathLogo", retorno.Result.Url);
                                        }
                                        else
                                        {
                                            HttpContext.Session.SetString("PathLogo", "/img/profile-photos/1.png");
                                        }

                                        if (returnUrl != null)
                                        {
                                            return RedirectToLocal(returnUrl);
                                        }
                                        else
                                        {
                                            TempData["Acesso"] = "";

                                            if (user.Acesso == false)
                                            {
                                                TempData["Acesso"] = false;

                                                await _usuarios.UpdateAcesso(user);

                                                HttpContext.Session.SetString("DefaultHome", "/sinistros/IndexAdmin");

                                                _appLog.SaveLogin(user);

                                                return Json(new { ok = true, message = "/sinistros/IndexAdmin" });
                                            }
                                            else
                                            {
                                                TempData["Acesso"] = true;

                                                HttpContext.Session.SetString("DefaultHome", "/sinistros/IndexAdmin");

                                                _appLog.SaveLogin(user);

                                                return Json(new { ok = true, message = "/sinistros/IndexAdmin" });
                                            }


                                        }

                                    }
                                    catch (Exception)
                                    {
                                        return Json(new { ok = false, message = "Ambiente não localizado!" });
                                    }
                                }
                                if (result.IsLockedOut)
                                {
                                    return Json(new { ok = false, message = "Conta de usuário bloqueada!" });
                                }
                                else
                                {
                                    return Json(new { ok = false, message = "Senha Inválida!" });
                                }
                            }
                            else
                            {
                                return Json(new { ok = false, message = "Você não tem mais acesso ao Portal Trade Vale!" });
                            }
                        }
                        else
                        {
                            return Json(new { ok = false, message = "Desculpe, você não tem o acesso de Administrador!" });
                        }
                    }
                    else
                    {
                        return Json(new { ok = false, message = "Desculpe, você não tem o acesso de Administrador, Segurado ou Comercial!" });
                    }
                }
                else
                {
                    return Json(new { ok = false, message = "Usuário informado não existe no sistema!" });
                }
            }
            return Json(new { ok = false, message = "Preencha todas as informações!" });
        }

        /// <summary>
        /// Método para efetuar o logout do usuário
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Logout()
        {
            var Ambiente = HttpContext.Session.GetString("UsuarioID");
            _appLog.SaveLogout(Ambiente);
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction(nameof(Login));
                //return RedirectToAction(nameof(AccountController.Login), "Account");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"Não foi possível carregar o usuário com o ID'{userId}'.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.ResetPasswordCallbackLink(user.Id, code, Request.Scheme);
                await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                   $"Redefina sua senha clicando aqui: <a href='{callbackUrl}'>link</a>");
                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "PORTALCLIENTE, COMERCIAL")]
        public IActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "PORTALCLIENTE, COMERCIAL")]
        public async Task<IActionResult> AlterarSenha(AlterarSenhaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    return Json(new { ok = false, message = "A sua NOVA SENHA e a CONFIRMAÇÃO DA SENHA estão diferentes!" });
                }
                else
                {
                    return Json(new { ok = false, message = "Preencha todos os campos corretamente!" });
                }
            }
            else
            {
                var UsuarioID = HttpContext.Session.GetString("UsuarioID");

                var user = await _userManager.FindByIdAsync(UsuarioID);

                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    return RedirectToAction(nameof(Logout));
                }
                var check = await _signInManager.PasswordSignInAsync(user, model.PasswordAtual, false, false);

                if (check.Succeeded)
                {
                    var result = await _userManager.ChangePasswordAsync(user, model.PasswordAtual, model.Password);

                    if (result.Succeeded)
                    {
                        return Json(new { ok = true, message = "Sua Senha foi alterado!" });
                    }
                }
                return Json(new { ok = false, message = "A senha atual não corresponde com a senha do usuário!" });

            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ReenviarEmail(string email)
        {
            var retorno = new ForgotPasswordViewModel
            {
                Email = email
            };
            return View(retorno);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ReenviarEmail(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _usuarios.GetExisteEmail(model.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return Json(new { ok = false, message = "Conta não Localizada!" });
                }
                if (!user.LockoutEnabled)
                {
                    return Json(new { ok = false, message = "Você não tem mais acesso ao Portal Trade Vale!" });
                }
                if (!user.EmailConfirmed)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code }, protocol: Request.Scheme);
                    var emailBody = _makeEmail.MakeEmailNewUser(user.Nome, callbackUrl);
                    await _emailSender.SendEmailAsync(user.Email, "Confirmação da Criação de Usuário no Portal Trade Vale", emailBody);
                    return Json(new { ok = true, message = "/Account/ReenviarEmailConfirmation" });
                }
                else
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code }, protocol: Request.Scheme);
                    var emailBody = _makeEmail.MakeEmailRecuperaSenha(user.Nome, callbackUrl);
                    await _emailSender.SendEmailAsync(user.Email, "Recuperação de Senha", emailBody);
                    return Json(new { ok = true, message = "/Account/ReenviarEmailConfirmation" });
                }
            }
            return Json(new { ok = false, message = "Campo do e-mail está vazio!" });
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ReenviarEmailConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string userId, string code)
        {
            if (code == null)
            {
                throw new ApplicationException("Um código deve ser fornecido para redefinição de senha.");
            }
            var model = new ResetPasswordViewModel { Id = userId, Code = code };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Erro ao Salvar!");
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                // Don't reveal that the user does not exist
                ModelState.AddModelError(string.Empty, "Conta não Localizada!");
                return View(model);
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(ResetConfirmation));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Sua Senha não pode ser alterada!");
                return View(model);
            }
        }


        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Json(new { ok = true, message = returnUrl });
            }
            else
            {
                return Json(new { ok = true, message = "/Account/Login" });
            }
        }
        #endregion

        #region Métodos Json

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> CheckEmailReenvio(string usuario)
        {
            if (usuario != null)
            {
                var user = await _usuarios.GetExisteUsuario(usuario);
                if (user != null)
                {
                    return Json(new { ok = true, message = "/Account/ReenviarEmail/?email=" + user.Email + "" });
                }
                else
                {
                    return Json(new { ok = false, message = "Usuário informado não existe no sistema!" });
                }
            }
            else
            {
                return Json(new { ok = false, message = "Preencha o login do usuário antes de clicar em Reenviar Email!" });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> CheckEmailEsqueceu(string usuario)
        {
            if (usuario != null)
            {
                var user = await _usuarios.GetExisteUsuario(usuario);
                if (user != null)
                {
                    return Json(new { ok = true, message = "/Account/ReenviarEmail/?email=" + user.Email + "" });
                }
                else
                {
                    return Json(new { ok = false, message = "Usuário informado não existe no sistema!" });
                }
            }
            else
            {
                return Json(new { ok = false, message = "Preencha o login do usuário antes de clicar em Esqueceu a senha?" });
            }
        }

        #endregion

        // GET api/profile/me
        //[HttpGet]
        //public async Task<IActionResult> Me()
        //{
        //    // retrieve the user info
        //    var userId = _caller.Claims.Single(c => c.Type == "id");
        //    var customer = await _appDbContext.Customers.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);

        //    return new OkObjectResult(new
        //    {
        //        Message = "This is secure API and user data!",
        //        customer.Identity.UserName,
        //        customer.Identity.Email,
        //        customer.Location,
        //    });
        //}
    }
}