using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PrashTechnologies.Api.Helper
{
    public class CurrencyConverter
    {
        string baseUrl = "http://api.currencylayer.com/live?access_key=6aacc48d866e93226ef2a0aed3d3f4bd&format=1";

        public CurrencyLive GetQuotes()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(baseUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<CurrencyLive>(result);
                }
                else
                    return null;
            }
        }
    }
}
