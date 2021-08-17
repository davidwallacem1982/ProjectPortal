using Portal.Core.Entities;
using Portal.Core.Models;
using System.Collections.Generic;

namespace Portal.Core.Interfaces
{
    public interface IRepositoryContatosPortal : IRepository<ContatosPortal>
    {
        List<ContatosPortalViewModel> SelectContatos();
    }
}
