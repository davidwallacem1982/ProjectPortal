namespace Portal.Core.Models
{
    public class GraficoPizzaAdministradorViewModel
    {
        /// <summary>
        /// Tipo do Ramos Seguros
        /// </summary>
        public string Ramo { get; set; }

        /// <summary>
        /// Soma do Valor Total do Prejuízo(ValorSinistro) por Ramo
        /// </summary>
        public decimal ValorTotalPrejuizo { get; set; }
    }
}
