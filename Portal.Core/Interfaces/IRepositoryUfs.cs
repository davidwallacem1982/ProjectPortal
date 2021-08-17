using Portal.Core.Entities;

namespace Portal.Core.Interfaces
{
    public interface IRepositoryUfs : IRepository<Ufs>
    {
        string SelectEstadoByIBGE(int IBGE);
    }
}
