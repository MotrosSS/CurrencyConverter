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
using System.Windows.Input;

namespace CurrencyConverter.ViewModels
{
    public class MainViewModel : Notifier
    {
        public ObservableCollection<Organization> Organizations { get; set; }

        private double summa;        
        public double Summa
        {
            get => summa;
            set
            {
                summa = value;
                Notify();
            }
        }


        public Organization selectedOrganization;
        public Organization SelectedOrganization
        {
            get => selectedOrganization;
            set
            {
                selectedOrganization = value;
                Notify();
            }
        }

        public  MainViewModel()
        {
            GetOrganizations();
            Organizations = new ObservableCollection<Organization>();

              
    }


        #region Methods

        private async void GetOrganizations()
        {
            FinanceManager financeManager = new FinanceManager();
            foreach (var item in await financeManager.GetBanks())
            {
                Organizations.Add(item);
            } 
        }

        #endregion

        #region Commands

        private ICommand checkRadioButton;

        public ICommand CheckRadioButton => checkRadioButton ?? (checkRadioButton = new RelayCommand(x=>
        {
            
        }));

        #endregion

    }
}
