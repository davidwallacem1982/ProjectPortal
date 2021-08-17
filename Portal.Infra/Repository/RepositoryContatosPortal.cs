using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Core.Interfaces;
using Portal.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Infra.Repository
{
    public class RepositoryContatosPortal : Repository<ContatosPortal>, IRepositoryContatosPortal
    {
        public RepositoryContatosPortal(DbContext context) : base(context) { }

        /// <summary>
        /// Busca todos os ContatosPortal
        /// </summary>
        /// <returns>Retorna todos os Ids e os titulos do ContatosPortal</returns>
        public List<ContatosPortalViewModel> SelectContatos() =>
            Items.Select(cont => new ContatosPortalViewModel
            {
                Id = cont.Id,
                Titulo = cont.Titulo
                
            }).ToList();
    }
}
