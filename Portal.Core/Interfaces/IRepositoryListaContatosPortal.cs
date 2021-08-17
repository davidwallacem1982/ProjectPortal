using Portal.Core.Entities;
using Portal.Core.Models;
using System.Collections.Generic;

namespace Portal.Core.Interfaces
{
    public interface IRepositoryListaContatosPortal : IRepository<ListaContatosPortal>
    {
        List<ListaContatosViewModel> SelectListaContatos(long Id);

        ListaContatosPortal SelectContatoPortal(long Id);

        bool SelectNomeContatoPortal(string nome);
    }
}
