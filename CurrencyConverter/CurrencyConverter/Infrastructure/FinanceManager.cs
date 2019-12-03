using CurrencyConverter.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Infrastructure
{
    class FinanceManager
    {
        public NetworkManager networkManager;
        public FinanceManager()
        {
            networkManager = new NetworkManager();
        }
        public Task<ObservableCollection<Organization>> GetBanks()
        {
            return Task.Run(() =>
            {
                ObservableCollection<Organization> banks = new ObservableCollection<Organization>();

                string json = networkManager.GetJson();
                JObject obj = JObject.Parse(json);
                IList<JToken> organizations = obj["organizations"].Children().ToList();

                foreach (JToken item in organizations)
                {
                    Organization organization = item.ToObject<Organization>();
                    IList<JToken> jtoken = item["currencies"].Children().ToList();
                    foreach (JProperty res in jtoken)
                    {
                        Currenc currenc = new Currenc();
                        currenc = res.Value.ToObject<Currenc>();
                        currenc.Name = res.Name;
                        organization.Currencies.Add(currenc);
                    }
                    banks.Add(organization);
                }
                return banks;
            });
        }

        public Task<ObservableCollection<CourseTitle>> GetExchangeRates()
        {
            return Task.Run(() =>
            {
                ObservableCollection<CourseTitle> results = new ObservableCollection<CourseTitle>();

                string json = networkManager.GetJson();
                JObject obj = JObject.Parse(json);
                IList<JToken> currencies = obj["currencies"].Children().ToList();

                foreach (JProperty res in currencies)
                {
                    CourseTitle calculation = new CourseTitle();
                    calculation.Abbreviation = res.Name;
                    calculation.DecodingAbbreviations = res.Value.ToString();

                    results.Add(calculation);
                }
                return results;
            });
        }
    }
}
