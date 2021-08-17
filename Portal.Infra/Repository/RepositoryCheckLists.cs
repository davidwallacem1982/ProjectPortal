using Microsoft.EntityFrameworkCore;
using Portal.Core.Entities;
using Portal.Core.Interfaces;
using Portal.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Infra.Repository
{
    public class RepositoryCheckLists : Repository<CheckLists>, IRepositoryCheckLists
    {
        public RepositoryCheckLists(DbContext context) : base(context) { }

        /// <summary>
        /// Buscar algum CheckList com base no id do TiposSinistros. 
        /// </summary>
        /// <param name="tiposSinistrosID"></param>
        /// <returns>Retorna uma lista somente com os Tipo de Arquivo</returns>
        public List<TiposArquivosViewModel> SelectByTiposSinistrosID(int tiposSinistrosID) =>

            Items.Where(c => c.TiposSinistrosID == tiposSinistrosID)
                 .Include(s => s.TiposArquivos)
                 .Select(ck => new TiposArquivosViewModel
                 {
                     Tipo = ck.TiposArquivos.Tipo
                 }).ToList();

        /// <summary>
        /// Buscar algum CheckList com base no id do Sinistro.
        /// </summary>
        /// <param name="sinistroId"></param>
        /// <returns>Retorna uma lista somente com os Tipo de Arquivo</returns>
        public List<TiposArquivosViewModel> SelectBySinistrosID(long sinistroId) =>

            Items.Where(c => c.SinistrosID == sinistroId)
                 .Include(t => t.TiposArquivos)
                 .Select(ck => new TiposArquivosViewModel
                 {
                     Tipo = ck.TiposArquivos.Tipo
                 }).ToList();

        /// <summary>
        /// Buscar algum CheckList com base no id do TiposSinistros. 
        /// </summary>
        /// <param name="tiposSinistrosID"></param>
        /// <returns>Retorna uma lista somente com o ID e o Tipo de Arquivo</returns>
        public List<DocumentosPendentesViewModel> SelectCheckPendentesByTiposSinistrosID(int tiposSinistrosID) =>

            Items.Where(c => c.TiposSinistrosID == tiposSinistrosID)
                 .Include(t => t.TiposArquivos)
                 .Select(ck => new DocumentosPendentesViewModel
                 {
                     TiposID = ck.TiposID,
                     Tipo = ck.TiposArquivos.Tipo
                 }).ToList();

        /// <summary>
        /// Buscar algum CheckList com base no id do Sinistro.
        /// </summary>
        /// <param name="sinistroId"></param>
        /// <returns>Retorna uma lista somente com o ID e o Tipo de Arquivo</returns>
        public List<DocumentosPendentesViewModel> SelectCheckPendentesBySinistrosId(long sinistroId) =>

            Items.Where(c => c.SinistrosID == sinistroId)
                 .Include(s => s.TiposArquivos).ToList()
                 .Select(ck => new DocumentosPendentesViewModel
                 {
                     TiposID = ck.TiposID,
                     Tipo = ck.TiposArquivos.Tipo
                 }).ToList();
    }
}
