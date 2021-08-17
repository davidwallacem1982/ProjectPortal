using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Portal.Core.Storage.Models
{
    public class HistoricoUsuariosCounts : TableEntity
    {
        public long MetodoHTTPID { get; set; }
        public int Quant { get; set; }
        public DateTime DataAcesso { get; set; }
        public long RegrasEnvioEmails { get; set; }
        public int QuantLimite { get; set; }
        public string UltimoHistoricoID { get; set; }
        public int QuantRegistros { get; set; }

        /// <summary>
        ///         ''' 
        ///         ''' </summary>
        ///         ''' <param name="RowKey">UsuariosID</param>
        ///         ''' <remarks></remarks>
        public HistoricoUsuariosCounts(string RowKey)
        {
            this.PartitionKey = RowKey;
            this.RowKey = Guid.NewGuid().ToString();
        }

        //public HistoricoUsuariosCounts(int Id)
        //{
        //}
    }
}
