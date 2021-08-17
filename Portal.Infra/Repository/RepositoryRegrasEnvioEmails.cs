using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Core.Interfaces;

namespace Portal.Infra.Repository
{
    public class RepositoryRegrasEnvioEmails : Repository<RegrasEnvioEmails>, IRepositoryRegrasEnvioEmails
    {
        public RepositoryRegrasEnvioEmails(DbContext context) : base(context) { }
    }
}