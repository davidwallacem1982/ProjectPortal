using System;

namespace Portal.Web.UtilWeb
{
    public class Mascaras
    {
        public static string AplicarMascaraTelefone(string strNumero)
        {
            // por omissão tem 10 ou menos dígitos
            var strMascara = "";
            if (strNumero.Length == 11)
                strMascara = "{0:(00)00000-0000}";
            else
                strMascara = "{0:(00)0000-0000}";

            // converter o texto em número
            long lngNumero = Convert.ToInt64(strNumero);

            return string.Format(strMascara, lngNumero);
        }
    }
}
