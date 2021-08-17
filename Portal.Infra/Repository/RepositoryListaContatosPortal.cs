using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Core.Interfaces;
using Portal.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Infra.Repository
{
    public class RepositoryListaContatosPortal : Repository<ListaContatosPortal>, IRepositoryListaContatosPortal
    {
        public RepositoryListaContatosPortal(DbContext context) : base(context) { }

        public List<ListaContatosViewModel> SelectListaContatos(long Id) =>

            Items.Where(l => l.ContatosPortalId == Id)
                 .Select(cont => new ListaContatosViewModel
                 {
                     Id = cont.Id,
                     Nome = cont.Nome,
                     Telefone = cont.Telefone,
                     Email = cont.Email,
                     Site = cont.Site,
                     Cargo = cont.CargoId.ToString()

                 }).ToList();

        public ListaContatosPortal SelectContatoPortal(long Id) =>

            Items.Where(l => l.Id == Id)
                 .Select(cont => new ListaContatosPortal
                 {
                     Id = cont.Id,
                     Nome = cont.Nome,
                     Telefone = cont.Telefone,
                     Email = cont.Email,
                     Site = cont.Site,
                     ContatosPortalId = cont.ContatosPortalId,
                     CargoId = cont.CargoId
                 }).FirstOrDefault();

        public bool SelectNomeContatoPortal(string nome)
        {
            var result = Items.Where(l => l.Nome == nome).Count();

            if (result > 0)
                return true;
            else
                return false;
        }
    }
}
