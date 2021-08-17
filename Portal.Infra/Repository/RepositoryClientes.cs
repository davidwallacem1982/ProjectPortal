using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Core.Interfaces;
using System.Linq;

namespace Portal.Infra.Repository
{
    public class RepositoryClientes : Repository<Clientes>, IRepositoryClientes
    {
        public RepositoryClientes(DbContext context) : base(context) { }

        /// <summary>
        /// Buscar o Cliente com base no seu ID
        /// </summary>
        /// <param name="clienteID">Identificador único do Cliente</param>
        /// <returns>Retorna somente o Nome do Cliente</returns>
        public string SelectNomeCliente(long clienteID) =>

            Items.Where(s => s.Id == clienteID)
                 .Select(cli => new Clientes
                 {
                     NomePrincipal = cli.NomePrincipal
                 }).FirstOrDefault().NomePrincipal;
    }
}
