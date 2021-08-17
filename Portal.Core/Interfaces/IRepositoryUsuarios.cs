using Portal.Core.Entities;
using Portal.Core.Identity;
using System.Threading.Tasks;

namespace Portal.Core.Interfaces
{
    public interface IRepositoryUsuarios : IRepository<Ufs>
    {
        Task<ApplicationUser> SelectExisteUsuario(string userName);

        Task<ApplicationUser> EditAcesso(ApplicationUser user);

        Task<ApplicationUser> SelectVerificarEmail(string email);

        Task<ApplicationUser> GetUnitUserAsync(string userName);
    }
}
