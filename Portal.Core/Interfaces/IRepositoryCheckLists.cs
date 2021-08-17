using Portal.Core.Entities;
using Portal.Core.Models;
using System.Collections.Generic;

namespace Portal.Core.Interfaces
{
    public interface IRepositoryCheckLists : IRepository<CheckLists>
    {
        List<TiposArquivosViewModel> SelectByTiposSinistrosID(int tiposSinistrosID);

        List<TiposArquivosViewModel> SelectBySinistrosID(long sinistroId);

        List<DocumentosPendentesViewModel> SelectCheckPendentesByTiposSinistrosID(int tiposSinistrosID);

        List<DocumentosPendentesViewModel> SelectCheckPendentesBySinistrosId(long sinistroId);
    }
}
