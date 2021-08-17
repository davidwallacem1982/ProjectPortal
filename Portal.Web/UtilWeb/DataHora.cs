using System;

namespace Portal.Web.UtilWeb
{
    public static class DataHora
    {
        /// <summary>
        /// Method de Retorno da Data/Hora atual Brasil (GMT -3).
        /// </summary>
        public static DateTime Agora
        {
            get
            {
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "E. South America Standard Time");
            }
        }
    }
}
