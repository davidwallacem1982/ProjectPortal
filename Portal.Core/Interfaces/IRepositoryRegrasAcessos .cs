using Portal.Core.Entities;
using Portal.Core.Models;

namespace Portal.Core.Interfaces
{
    public interface IRepositoryRegrasAcessos : IRepository<RegrasAcessos>
    {
        RolesViewModel ActionByAction(string controle, string Methods, string HttpMethodsID);
    }
}
