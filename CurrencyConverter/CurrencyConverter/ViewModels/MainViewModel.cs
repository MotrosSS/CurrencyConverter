using CurrencyConverter.Infrastructure;
using CurrencyConverter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Organization> Organizations { get; set; }
       
        public Organization SelectedOrganization { get; set; }

        public  MainViewModel()
        {
            GetOrganizations();
        }


        #region Methods

        private async void GetOrganizations()
        {
            FinanceManager financeManager = new FinanceManager();
            Organizations = await financeManager.GetBanks();
        }

        #endregion

    }
}
