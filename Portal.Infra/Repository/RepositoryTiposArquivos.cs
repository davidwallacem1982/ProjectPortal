using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Core.Interfaces;
using Portal.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Infra.Repository
{
    public class RepositoryTiposArquivos : Repository<TiposArquivos>, IRepositoryTiposArquivos
    {
        public RepositoryTiposArquivos(DbContext context) : base(context) { }

        /// <summary>
        /// Busca o tipo do arquivo com base no Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public TiposArquivosSmallViewModel SelectFirstTiposArquivosById(long Id)
        {
            var result = Items.Where(t => t.Id == Id)
                                .Select(tp => new TiposArquivosSmallViewModel
                                {
                                    Tipo = tp.Tipo,
                                    Nome = tp.Nome,
                                }).FirstOrDefault();

            return result;
        }

        /// <summary>
        /// Busca todos os tipos de arquivos
        /// <returns>Retorna uma lista somente com o Id e o Tipo</returns>
        public List<ListaTiposViewModel> ListarTiposArquivos()
        {
            var result = Items
                .Select(tp => new ListaTiposViewModel
                {
                    Id = tp.Id,
                    Tipo = tp.Tipo
                }).ToList();

            return result;
        }
    }
}
