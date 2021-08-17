using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Portal.Core.Models
{
    public class HistoricosUsuarios : TableEntity
    {
        public string Controller { get; set; }
        public string Metodo { get; set; }
        public string MetodoHTTP { get; set; }
        public DateTime DataAcesso { get; set; }
        public string Navegador { get; set; }
        public string Uri { get; set; }
        public string Data { get; set; }
        public string Detalhes { get; set; }
        public string Tratado { get; set; }
        public string Titulo { get; set; }
        public string QuantRegistros { get; set; }
        public string StatusZip { get; set; }

        /// <summary>
        ///         ''' 
        ///         ''' </summary>
        ///         ''' <param name="RowKey">ID Login (Session Ambiente)</param>
        ///         ''' <remarks></remarks>
        public HistoricosUsuarios(string RowKey)
        {
            this.PartitionKey = RowKey;
            this.RowKey = Guid.NewGuid().ToString();
        }

        public HistoricosUsuarios()
        {
        }
    }

}
