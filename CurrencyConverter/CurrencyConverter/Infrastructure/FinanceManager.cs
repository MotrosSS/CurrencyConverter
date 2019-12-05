using CurrencyConverter.Context;
using CurrencyConverter.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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

                string date = obj["date"].ToString();

                foreach (JToken item in organizations)
                {
                    Organization organization = item.ToObject<Organization>();
                    IList<JToken> jtoken = item["currencies"].Children().ToList();
                    foreach (JProperty res in jtoken)
                    {
                        Course currenc = new Course();
                        currenc = res.Value.ToObject<Course>();
                        currenc.Date = date;
                        currenc.Name = res.Name;
                        organization.Currencies.Add(currenc);
                    }
                    banks.Add(organization);
                }
                SendDataInDatabase(banks);
                return banks;
            });
        }

        public Task<ObservableCollection<Currency>> GetExchangeRates()
        {

            return Task.Run(() =>
            {
                ObservableCollection<Currency> results = new ObservableCollection<Currency>();

                string json = networkManager.GetJson();
                JObject obj = JObject.Parse(json);
                IList<JToken> currencies = obj["currencies"].Children().ToList();

                foreach (JProperty res in currencies)
                {
                    Currency calculation = new Currency();
                    calculation.Abbreviation = res.Name;
                    calculation.DecodingAbbreviations = res.Value.ToString();
                    results.Add(calculation);
                }
                return results;
            });
        }

        public void SendDataInDatabase(ObservableCollection<Organization> organization)
        {
            Task.Run(() =>
            {
                try
                {
                    using (var context = new FinanceContext())
                    {
                        if (context.Organizations.Count() != 0)
                            foreach (var item in organization)
                            {
                                foreach (var curr in item.Currencies)
                                {
                                    curr.OrganizationId = item.Id;
                                    context.Currencs.Add(curr);
                                    context.SaveChanges();
                                }
                            }
                        else
                            foreach (var item in organization)
                            {
                                context.Organizations.Add(item);
                                context.SaveChanges();
                            }
                    }
                }
                catch { }
            });
        }
    }
}
