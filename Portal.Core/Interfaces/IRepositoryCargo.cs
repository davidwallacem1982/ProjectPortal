using Portal.Core.Entities;
using Portal.Core.Models;
using System.Collections.Generic;

namespace Portal.Core.Interfaces
{
    public interface IRepositoryCargo : IRepository<Cargo>
    {
        string SelectCargo(long Id);

        List<CargoViewModel> SelectCargos();
    }
}
