using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Core.Interfaces;
using Portal.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Infra.Repository
{
    public class RepositoryCargo : Repository<Cargo>, IRepositoryCargo
    {
        public RepositoryCargo(DbContext context) : base(context) { }

        public string SelectCargo(long Id) =>

            Items.Where(l => l.Id == Id).FirstOrDefault().Nome;

        /// <summary>
        /// Busca todos os Cargo
        /// </summary>
        /// <returns>Retorna todos os Ids e os cargos da tabela Cargo</returns>
        public List<CargoViewModel> SelectCargos() =>
            Items.Select(cg => new CargoViewModel
            {
                Id = cg.Id,
                Nome = cg.Nome

            }).ToList();
    }
}
