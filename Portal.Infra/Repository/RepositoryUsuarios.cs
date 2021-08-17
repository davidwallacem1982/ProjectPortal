using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Portal.Core.Identity;
using Portal.Core.Interfaces;
using System.Threading.Tasks;

namespace Portal.Infra.Repository
{
    public class RepositoryUsuarios : Repository<ApplicationUser>, IRepositoryUsuarios
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public RepositoryUsuarios(DbContext context, UserManager<ApplicationUser> userManager) : base(context)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Buscar algum usuário com base no UserName.
        /// </summary>
        /// <param name="userName">login do usuário</param>
        /// <returns>Retorna os dados do usuário</returns>
        public async Task<ApplicationUser> SelectExisteUsuario(string userName) =>

            await _userManager.Users
                              .FirstOrDefaultAsync(u => u.UserName == userName);

        /// <summary>
        /// Edita o usuário.
        /// </summary>
        /// <param name="user">Model com Usuário</param>
        /// <returns>Retorna os dados do usuário já editado</returns>
        public async Task<ApplicationUser> EditAcesso(ApplicationUser user)
        {
            user.Acesso = true;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return user;
            }
            else
            {
                user.Acesso = false;
                return user;
            }
        }

        /// <summary>
        /// Edita o usuário.
        /// </summary>
        /// <param name="user">Model com Usuário</param>
        /// <returns>Retorna os dados do usuário já editado</returns>
        public async Task<ApplicationUser> SelectVerificarEmail(string email) =>

            await _userManager.Users
                              .FirstOrDefaultAsync(u => u.Email == email);

        public async Task<ApplicationUser> GetUnitUserAsync(string userName)
        {
            var model = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            return new ApplicationUser()
            {
                Id = model.Id,
                UserName = model.UserName,
                Email = model.Email,
                bloqueioIP = model.bloqueioIP,
                Nome = model.Nome,
                Interno = model.Interno,
                TipoAcessoID = model.TipoAcessoID,
                ClientesID = model.ClientesID
            };
        }
    }
}
