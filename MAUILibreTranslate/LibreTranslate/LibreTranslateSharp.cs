using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MAUILibreTranslate
{
    internal class LibreTranslateSharp
    {

        public string txtinput { get; set; }
        public string server { get; set; } = "https://translate.terraprint.co/translate";
        public string source { get; set; }
        public string target { get; set; }


        public string Translate() 
        {
            string result = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(server);
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://translate.argosopentech.com/translate");
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://translate.terraprint.co/translate");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                JObject json_text = new JObject(
                      new JProperty("q", txtinput),
                      new JProperty("source", source),
                      new JProperty("target", target));

                streamWriter.Write(json_text);
                streamWriter.Flush();
                streamWriter.Close();

                try 
                {
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                    }
                }
                catch(Exception ex) 
                {
                    result = "{\"translatedText\":\"Error\"}";
                }
            }
            JObject obj = JObject.Parse(result);
            string text = (string)obj["translatedText"];
            //Console.WriteLine(text);
            return text;

        }
    }
}
