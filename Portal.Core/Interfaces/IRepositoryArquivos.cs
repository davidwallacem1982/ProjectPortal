using Portal.Core.Entities;
using Portal.Core.Models;
using System.Collections.Generic;

namespace Portal.Core.Interfaces
{
    public interface IRepositoryArquivos : IRepository<Arquivos>
    {
        int SelectByContaArquivosPendentes(long sinistrosID, int TipoSinistrosID);

        List<DocumentosPendentesViewModel> SelectByArquivosPendentes(long sinistrosID, int TipoSinistrosID);

        List<DocumentosSinistroViewModel> SelectByDocumentosSinistro(long sinistrosID);

        int CountByArquivosAprovado(string Arquivo, long Controller, long SinistroID);

        int CountArquivosReprovado(string Arquivo, long Controller, long SinistroID, long Sequencial);

        Arquivos SelectByArquivosRepetido(string Tipo, int Sequencial, long Status, int Substituido);
    }
}
