using Portal.Core.Entities;

namespace Portal.Core.Interfaces
{
    public interface IRepositoryMotoristas : IRepository<Motoristas>
    {
        string SelectNomeMotoristaByCPFMotorista(string CPFMotorista);
    }
}
