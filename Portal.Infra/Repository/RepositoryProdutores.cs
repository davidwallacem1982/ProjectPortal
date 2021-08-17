using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Core.Interfaces;

namespace Portal.Infra.Repository
{
    public class RepositoryProdutores : Repository<Produtores>, IRepositoryProdutores
    {
        public RepositoryProdutores(DbContext context) : base(context) { }
    }
}
