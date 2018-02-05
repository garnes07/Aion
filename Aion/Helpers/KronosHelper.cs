using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Aion.Helpers
{
    public static class KronosHelper
    {
        private static CookieContainer cookieContainer = new CookieContainer();

        public static string PostString(this string data, string url, bool newSession)
        {
            if (newSession)
            {
                cookieContainer = new CookieContainer();
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.CookieContainer = cookieContainer;
            request.KeepAlive = false;
            request.Timeout = 10000;

            //Encode content request
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            //Initialise stream and send
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            //Get response
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            return reader.ReadToEnd();
        }

        public static async Task<string> PostStringAsync(this string data, string url, bool newSession)
        {
            if (newSession)
            {
                cookieContainer = new CookieContainer();
            }
            var toRtn = string.Empty;
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler))
            {
                //var httpContent = new StringContent(data, Encoding.UTF8, "text/x-www-form-urlencoded");

                var byteArray = Encoding.UTF8.GetBytes(data);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                var byteArrayContent = new ByteArrayContent(byteArray);
                byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                var result = await client.PostAsync(url, byteArrayContent);
                if (result.IsSuccessStatusCode)
                {
                    toRtn = await result.Content.ReadAsStringAsync();
                }
                else
                {
                    var resp = result.Content;
                    toRtn = result.ReasonPhrase + " - " + await resp.ReadAsStringAsync();
                    if (toRtn == null) { }
                }
            }

            return toRtn;
        }

        public static async Task<List<T>> DeserializeToObjectAsync<T>(this string xmlString) where T : new()
        {
            var result = new List<T>();
            if (string.IsNullOrEmpty(xmlString)) return result;

            var serializer = new XmlSerializer(typeof(T));

            var byteArray = Encoding.UTF8.GetBytes(xmlString);
            var stream = new MemoryStream(byteArray);
            var reader = new XmlTextReader(stream);

            var objName = typeof(T).Name;
            while (reader.Read())
            {
                while (reader.NodeType == XmlNodeType.Element &&
                    reader.Name.Equals(objName, StringComparison.InvariantCultureIgnoreCase))
                {
                    var doc = new XmlDocument();
                    doc.LoadXml(await reader.ReadOuterXmlAsync());
                    result.Add(Deserialize<T>(doc));
                }
            }

            return result;
        }

        public static List<T> DeserializeToObject<T>(this string xmlString) where T : new()
        {
            var result = new List<T>();
            if (string.IsNullOrEmpty(xmlString)) return result;

            var serializer = new XmlSerializer(typeof(T));

            var byteArray = Encoding.UTF8.GetBytes(xmlString);
            var stream = new MemoryStream(byteArray);
            var reader = new XmlTextReader(stream);

            var objName = typeof(T).Name;
            while (reader.Read())
            {
                while (reader.NodeType == XmlNodeType.Element &&
                    reader.Name.Equals(objName, StringComparison.InvariantCultureIgnoreCase))
                {
                    var doc = new XmlDocument();
                    doc.LoadXml(reader.ReadOuterXml());
                    result.Add(Deserialize<T>(doc));
                }
            }
            return result;
        }

        private static T Deserialize<T>(XmlDocument doc) where T : new()
        {
            var result = default(T);

            using (var reader = new XmlNodeReader(doc.DocumentElement))
            {
                var settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.None;
                using (var xReader = XmlReader.Create(reader, settings))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    result = (T)serializer.Deserialize(xReader);
                }
            }

            return result;
        }
    }
}