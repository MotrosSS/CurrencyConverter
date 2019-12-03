using CurrencyConverter.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Models
{
    public class Organization:Notifier
    {

        private string title;
        private string id;
        private ObservableCollection<Currenc> currencies;


        #region Properties
        [JsonProperty(PropertyName = "title")]
        public string Title
        {
            get => title;
            set
            {
                title = value;
                Notify();
            }
        }

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get => id;
            set
            {
                id = value;
                Notify();
            }
        }

        [JsonProperty(PropertyName = "")]
        public ObservableCollection<Currenc> Currencies
        {
            get => currencies;
            set
            {
                currencies = value;
                Notify();
            }
        }

        #endregion

        public Organization()
        {
            currencies = new ObservableCollection<Currenc>();
        }

       
    }
}
