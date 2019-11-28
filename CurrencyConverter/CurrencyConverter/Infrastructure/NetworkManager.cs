using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Infrastructure
{
    class NetworkManager
    {
        public string GetJson()
        {
            HttpClient client = new HttpClient();
            var task = client.GetAsync("http://resources.finance.ua/ru/public/currency-cash.json").Result;
            return task.Content.ReadAsStringAsync().Result;
        }
    }
}
