using Portal.Core.Entities;

namespace Portal.Core.Interfaces
{
    public interface IRepositoryClientes : IRepository<Clientes>
    {
        string SelectNomeCliente(long clienteID);
    }
}
