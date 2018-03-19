using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace TraducaoBot
{    

        public class Linguagem
        {
            private readonly string _translateApiKey = ConfigurationManager.AppSettings["TranslateApiKey"];
            private readonly string _translateUri = ConfigurationManager.AppSettings["TranslateUri"];

            public async Task<string> TraducaoEn(string text)
            {
                var cliente = new HttpClient();
                cliente.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _translateApiKey);

                var uri = _translateUri + "?to=en-us" +
                          "&text=" + System.Net.WebUtility.UrlEncode(text);

                var response = await cliente.GetAsync(uri);
                var result = await response.Content.ReadAsStringAsync();
                var content = XElement.Parse(result).Value;

                return $" A sua tradução em inglês é: { content }";
            }
        }
    }