using Portal.Core.Notifications;
using System;
using System.ComponentModel.DataAnnotations;

namespace Portal.Core.Models
{
    public class SinistrosViewModel : Notifica
    {
        public Int64 ID { get; set; }

        [Display(Name = "Data Sinistro")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataSinistro { get; set; }

        [Display(Name = "Último Status")]
        public string UltimoStatus { get; set; }

        [Display(Name = "Ramo")]
        public string Ramo { get; set; }

        public string Seguradora { get; set; }

        [Display(Name = "Protocolo")]
        public Int64 Protocolo { get; set; }

        [Display(Name = "Doctos")]
        public bool Pendente { get; set; }
        /// <summary>
        /// Valor do Prejuizo - ValorSinistro
        /// </summary>
        [Display(Name = "Valor R$")]
        public decimal Valor { get; set; }
        /// <summary>
        /// Apelido da Seguradora
        /// </summary>
        [Display(Name = "Cia")]
        public string Apelido { get; set; }
        /// <summary>
        /// Semaforo 1-verde 2-amarelo 3-vermelho
        /// </summary>
        public int Semaforo { get; set; }
    }

}
