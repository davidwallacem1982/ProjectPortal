using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Core.Interfaces;

namespace Portal.Infra.Repository
{
    public class RepositoryHttpMethods : Repository<HttpMethods>, IRepositoryHttpMethods
    {
        public RepositoryHttpMethods(DbContext context) : base(context)
        {

        }
    }
}
