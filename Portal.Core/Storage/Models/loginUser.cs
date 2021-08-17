using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Portal.Core.Models
{
    public class LoginUser : TableEntity
    {
        public string UserName { get; set; }
        public string Nome { get; set; }
        public long ClientesID { get; set; }
        public string IP { get; set; }
        public DateTime DataLogin { get; set; }
        public DateTime? DataLogout { get; set; }
        public string Logado { get; set; }
        public string LoginExpirado { get; set; }

        public bool Sistema { get; set; }

        /// <summary>
        ///</summary>
        ///<param name="RowKey">usuarioID</param>
        ///<remarks></remarks>
        public LoginUser(string RowKey)
        {
            this.PartitionKey = Guid.NewGuid().ToString();
            this.RowKey = RowKey;
        }
        public LoginUser()
        {
        }
    }
}

