using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Core.Interfaces;

namespace Portal.Infra.Repository
{
    public class RepositoryRamosSeguros : Repository<RamosSeguros>, IRepositoryRamosSeguros
    {
        public RepositoryRamosSeguros(DbContext context) : base(context) { }
    }
}
