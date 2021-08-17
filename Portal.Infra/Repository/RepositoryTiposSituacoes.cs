using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Core.Interfaces;
using System.Linq;

namespace Portal.Infra.Repository
{
    public class RepositoryTiposSituacoes : Repository<TiposSituacoes>, IRepositoryTiposSituacoes
    {
        public RepositoryTiposSituacoes(DbContext context) : base(context) { }

        /// <summary>
        /// Retorna o valor do Semarofo da Situação do Sinistro 1-verde 2-amarelo 3-vermelho
        /// </summary>
        /// <param name="situacao"></param>
        /// <returns></returns>
        public int SelectBySituacao(string situacao) => Items.Where(t => t.Tipo == situacao)
                        .FirstOrDefault().Semaforo;
    }
}
