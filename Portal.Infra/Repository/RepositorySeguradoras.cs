using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Core.Interfaces;

namespace Portal.Infra.Repository
{
    public class RepositorySeguradoras : Repository<Seguradoras>, IRepositorySeguradoras
    {
        public RepositorySeguradoras(DbContext context) : base(context) { }
    }
}
