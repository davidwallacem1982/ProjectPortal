using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Core.Interfaces;
using System.Linq;

namespace Portal.Infra.Repository
{
    public class RepositoryUfs : Repository<Ufs>, IRepositoryUfs
    {
        public RepositoryUfs(DbContext context) : base(context) { }

        /// <summary>
        /// Buscar algum UF do Sinistro com base no IBGE.
        /// </summary>
        /// <param name="IBGE">Atributo IBGE da tabela UF</param>
        /// <returns>Retorna somente o Estado</returns>
        public string SelectEstadoByIBGE(int IBGE) =>

            Items.Where(s => s.IBGE == IBGE)
                 .Select(u => new Ufs
                 {
                     UF = u.UF
                 }).FirstOrDefault().UF;
    }
}
