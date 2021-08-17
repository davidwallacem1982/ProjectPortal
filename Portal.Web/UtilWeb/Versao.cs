using System.Reflection;

namespace Portal.Web.UtilWeb
{
    public class Versao
    {
        public static string PegarVersao()
        {
            return $"{Assembly.GetExecutingAssembly().GetName().Version}";
        }
    }
}
