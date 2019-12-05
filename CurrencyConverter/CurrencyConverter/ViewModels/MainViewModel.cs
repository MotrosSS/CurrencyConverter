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
using System.Text.RegularExpressions;
using System.Windows;
using CurrencyConverter.Context;
using System.Threading;

namespace CurrencyConverter.ViewModels
{
    public class MainViewModel : Notifier
    {
        private Organization selectedOrganization = new Organization();
        private Currency selectedСurrency = new Currency();
        private float sum; 
        public bool flag;

        #region Properties
        public ObservableCollection<Organization> Organizations { get; set; }
        public ObservableCollection<Currency> ListOfCurrencies { get; set; }
        public ObservableCollection<CalculationResult> Calculation { get; set; }
        public ObservableCollection<Course> ListBanks { get; set; }
        public Currency SelectedCourse
        {
            get => selectedСurrency;
            set
            {
                selectedСurrency = value;
                GetUkraineBanks();
                Notify();
            }
        }
        public Organization SelectedOrganization
        {
            get => selectedOrganization;
            set
            {
                selectedOrganization = value;
                GetRezult(flag);
                Notify();
            }
        }
        public float Sum
        {
            get => sum;
            set
            {
                sum = value;
                GetRezult(flag);
                Notify();

            }
        }
        #endregion

        public MainViewModel()
        {
            Organizations = new ObservableCollection<Organization>();
            Calculation = new ObservableCollection<CalculationResult>();
            ListOfCurrencies = new ObservableCollection<Currency>();
            ListBanks = new ObservableCollection<Course>();
            flag = true;
            GetData();
            
        }

        #region Methods

        private async void GetData()
        {
            FinanceManager financeManager = new FinanceManager();

            foreach (var item in await financeManager.GetBanks())
                Organizations.Add(item);

            foreach (var item in await financeManager.GetExchangeRates())
                ListOfCurrencies.Add(item);
        }

     

        private void GetRezult(bool flag)
        {
            Calculation.Clear();
            if (flag)
                foreach (var item in selectedOrganization.Currencies)
                {
                    CalculationResult calculation = new CalculationResult
                    {
                        Name = item.Name,
                        Result = item.Purchase * Sum,
                        FinalCourse = item.Purchase
                    };
                    calculation.Result = (float)Math.Round(calculation.Result, 2);
                    Calculation.Add(calculation);
                }
            else
                foreach (var item in selectedOrganization.Currencies)
                {
                    CalculationResult calculation = new CalculationResult
                    {
                        Name = item.Name,
                        Result = Sum / item.Purchase,
                        FinalCourse = item.Sale
                    };
                    calculation.Result = (float)Math.Round(calculation.Result, 2);
                    Calculation.Add(calculation);
                }
        }

        private void GetUkraineBanks()
        {
            ListBanks.Clear();
            foreach (var item in Organizations)
            {
                foreach (var res in item.Currencies)
                {
                    if (res.Name == selectedСurrency.Abbreviation)
                    {
                        Course currenc = new Course
                        {
                            Name = item.Title,
                            Purchase = res.Purchase,
                            Sale = res.Sale
                        };
                        ListBanks.Add(currenc);
                    }
                }
            }
        }

        #endregion

        #region Commands

        private ICommand checkPurchaseCommand;
        private ICommand checkSaleCommand;

        public ICommand CheckPurchaseCommand => checkPurchaseCommand ?? (checkPurchaseCommand = new RelayCommand(x =>
        {
            flag = true;
            try
            {
                GetRezult(flag);
            }
            catch { }

        }));
        public ICommand CheckSaleCommand => checkSaleCommand ?? (checkSaleCommand = new RelayCommand(x =>
         {
             flag = false;
             try
             {
                 GetRezult(flag);
             }
             catch { }
         }));

        #endregion
    }
}
