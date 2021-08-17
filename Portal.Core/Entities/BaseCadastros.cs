using System;

namespace Portal.Core.Entities
{
    public class BaseCadastros : BaseEntity
    {
        public string NomePrincipal { get; set; }
        /// <summary>
        /// CPF ou CNPJ
        /// </summary>
        public string Documento { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Municipio { get; set; }
        public string CEP { get; set; }
        public string UF { get; set; }
        public string Pais { get; set; }
        public string TelefoneFixo { get; set; }
        public string Email { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool Habilitado { get; set; }
        // API
        public string Ticket { get; set; }
    }
}
