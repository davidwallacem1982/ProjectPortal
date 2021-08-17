using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Core.Interfaces;

namespace Portal.Infra.Repository
{
    public class RepositoryRel_Usuarios_Produtores : Repository<Rel_Usuarios_Produtores>, IRepositoryRel_Usuarios_Produtores
    {
        public RepositoryRel_Usuarios_Produtores(DbContext context) : base(context) { }
    }
}
