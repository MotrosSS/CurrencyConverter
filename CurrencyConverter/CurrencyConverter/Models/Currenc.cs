using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Models
{
    public class Currenc
    {
        public string Name { get; set; }

        [JsonProperty(PropertyName = "ask")]
        public float Purchase { get; set; }

        [JsonProperty(PropertyName = "bid")]
        public float Sale { get; set; }
    }
}
