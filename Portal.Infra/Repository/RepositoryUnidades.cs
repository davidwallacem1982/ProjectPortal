using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Core.Interfaces;

namespace Portal.Infra.Repository
{
    public class RepositoryUnidades : Repository<Unidades>, IRepositoryUnidades
    {
        public RepositoryUnidades(DbContext context) : base(context) { }
    }
}
