namespace Portal.Web.UtilWeb
{
    public class VerificaTipoArquivo
    {
        public static string VerificaTipo(string contentType)
        {
            if (contentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                contentType = "application/xlsx";
            }
            else if (contentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                contentType = "application/docx";
            }

            return contentType;
        }
    }
}
