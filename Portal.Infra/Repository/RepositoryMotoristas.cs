using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Core.Interfaces;
using System.Linq;

namespace Portal.Infra.Repository
{
    public class RepositoryMotoristas : Repository<Motoristas>, IRepositoryMotoristas
    {
        public RepositoryMotoristas(DbContext context) : base(context) { }

        /// <summary>
        /// Buscar o Motorista com base no CPFMotorista
        /// </summary>
        /// <param name="CPFMotorista">Cpf do Motorista</param>
        /// <returns>Retorna somente o Nome do Motorista</returns>
        public string SelectNomeMotoristaByCPFMotorista(string CPFMotorista) =>

            Items.Where(m => m.Documento == CPFMotorista)
                 .Select(mt => new Motoristas
                 {
                     NomePrincipal = mt.NomePrincipal
                 }).FirstOrDefault().NomePrincipal;
    }
}
