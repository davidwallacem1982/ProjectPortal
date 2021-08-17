using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Text;

namespace Portal.Storage.Util
{
    public class UtilStorage
    {
        ///// <summary>
        ///// Retorna o PathLogo do logo que está no Storage.
        ///// </summary>
        ///// <param name="usuarioID">Identificação única do Usuário</param>
        ///// <returns></returns>
        //public static string PathLogo(string usuarioID)
        //{
        //    return "https://maisprotecao.blob.core.windows.net/logo/" + usuarioID + ".jpg";
        //}

        public static string ShowURL(ActionExecutingContext url)
        {
            var request = url.HttpContext.Request;

            var absoluteUri = string.Concat(
                        request.Scheme,
                        "://",
                        request.Host.ToUriComponent(),
                        request.PathBase.ToUriComponent(),
                        request.Path.ToUriComponent(),
                        request.QueryString.ToUriComponent());
            return absoluteUri;
        }
        /// <summary>
        /// Method de Retorno da Data/Hora atual Brasil (GMT -3) com 3 horas menos.
        /// </summary>
        public static DateTime AgoraStorageMenosTres
        {
            get
            {
                var data = DateTime.Now;
                var novaData = data.AddHours(-3);
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(novaData, "E. South America Standard Time");
            }
        }

        /// <summary>
        /// Method de Retorno da Data/Hora atual Brasil (GMT -3)
        /// </summary>
        public static DateTime AgoraStorage
        {
            get
            {
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "E. South America Standard Time");
            }
        }

        /// <summary>
        /// Compacta string da prop Data e Define StatusZip para 0 ou 1
        /// <para>Caso não exista as props Data e StatusZip não faz nada</para>
        /// </summary>
        /// <param name="obj"></param>
        public static void CompressDataProp(TableEntity obj)
        {
            try
            {
                PropertyInfo propData = obj.GetType().GetProperty("Data");
                PropertyInfo propStatusZip = obj.GetType().GetProperty("StatusZip");
                if (propData != null & propStatusZip != null)
                {
                    string value = propData.GetValue(obj).ToString();
                    propData.SetValue(obj, CompressText(value));
                    propStatusZip.SetValue(obj, "1");
                }
                else
                {
                    if (propStatusZip != null)
                    {
                        propStatusZip.SetValue(obj, "0");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public static void UnCompressDataProp(TableEntity obj)
        {
            try
            {
                PropertyInfo propData = obj.GetType().GetProperty("Data");
                PropertyInfo propStatusZip = obj.GetType().GetProperty("StatusZip");
                if (propData != null & propStatusZip.GetValue(obj).ToString() == "1")
                {
                    string value = propData.GetValue(obj).ToString();
                    propData.SetValue(obj, UnCompressText(value));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public static string CompressText(string value)
        {
            var UnZippedData = Encoding.UTF8.GetBytes(value);
            using MemoryStream ms = new MemoryStream();
            using (GZipStream GZip = new GZipStream(ms, CompressionLevel.Optimal))
            {
                GZip.Write(UnZippedData, 0, UnZippedData.Length);
            }
            var ZippedData = ms.ToArray();
            var Content = Convert.ToBase64String(ZippedData);
            return Content;
        }

        public static string UnCompressText(string value)
        {
            var ZippedData = Convert.FromBase64String(value);
            using var objMemStream = new MemoryStream(ZippedData);
            using var objGZipStream = new GZipStream(objMemStream, CompressionMode.Decompress);
            using MemoryStream bigStreamOut = new MemoryStream();
            objGZipStream.CopyTo(bigStreamOut);
            var lng = System.Convert.ToInt32(bigStreamOut.Length);
            return Encoding.UTF8.GetString(bigStreamOut.ToArray(), 0, lng);
        }
    }
}
