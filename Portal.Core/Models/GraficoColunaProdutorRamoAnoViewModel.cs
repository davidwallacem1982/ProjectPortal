namespace Portal.Core.Models
{
    public class GraficoColunaProdutorRamoAnoViewModel
    {
        /// <summary>
        /// mês/ano da Data do Sinistro
        /// </summary>
        public string MesAno { get; set; }
        /// <summary>
        /// Ramo
        /// </summary>
        public string Ramo { get; set; }
        /// <summary>
        /// Valor do Prejuízo(ValorSinistro) por mês
        /// </summary>
        public decimal ValorPrejuizoMes { get; set; }
        /// <summary>
        /// Quantidade de Eventos(Sinistros) por mês
        /// </summary>
        public int EventosMes { get; set; }
        /// <summary>
        /// Total de Eventos(Sinistros) por ano
        /// </summary>
        public int EventosAno { get; set; }
    }
}
