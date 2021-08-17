namespace Portal.Core.Models
{
    public class GraficoColunaProdutorAnoViewModel
    {
        /// <summary>
        /// Ano da Data do Sinistro
        /// </summary>
        public string Ano { get; set; }
        /// <summary>
        /// Ramo
        /// </summary>
        public string Ramo { get; set; }
        /// <summary>
        /// Valor do Prejuízo(ValorSinistro) por Ramo
        /// </summary>
        public decimal ValorPrejuizoAno { get; set; }
        /// <summary>
        /// Quantidade de Eventos(Sinistros) por Ramo
        /// </summary>
        public int Eventos { get; set; }
        /// <summary>
        /// Total de Eventos(Sinistros)
        /// </summary>
        public int TotalEventos { get; set; }
    }
}
