using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Core.Interfaces;

namespace Portal.Infra.Repository
{
    public class RepositoryProdutores_Clientes : Repository<Produtores_Clientes>, IRepositoryProdutores_Clientes
    {
        public RepositoryProdutores_Clientes(DbContext context) : base(context) { }
    }
}
