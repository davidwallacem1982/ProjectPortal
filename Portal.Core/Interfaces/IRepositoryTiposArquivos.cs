using Portal.Core.Entities;
using Portal.Core.Models;
using System.Collections.Generic;

namespace Portal.Core.Interfaces
{
    public interface IRepositoryTiposArquivos : IRepository<TiposArquivos>
    {

        TiposArquivosSmallViewModel SelectFirstTiposArquivosById(long Id);

        List<ListaTiposViewModel> ListarTiposArquivos();
    }
}
