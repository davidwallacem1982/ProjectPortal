using System;

namespace Portal.Core.Models
{
    public class RegrasEnvioEmailsHistoricoViewModel
    {
        public long RegrasEnvioEmailsID { get; set; }
        public long? HttpMethodsID { get; set; }
        public string Role { get; set; }
        public int? TempoDia { get; set; }
        public DateTime? horaInicio { get; set; }
        public DateTime? horaFim { get; set; }
        public string IPs { get; set; }
        public int? Quantidade { get; set; }
        public int? QuantRegistrosTotal { get; set; }
        public int? QuantRegistrosMaximo { get; set; }
        public int? QuantRegistrosMinimo { get; set; }
        public string TituloEmail { get; set; }
        public string Emails { get; set; }
        public string DiaSemana { get; set; }
        public bool? Feriados { get; set; }
        public bool BloquearAcesso { get; set; }
        public string RoleExcecao { get; set; }
    }
}