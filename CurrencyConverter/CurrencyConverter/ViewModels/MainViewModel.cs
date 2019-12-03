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

namespace CurrencyConverter.ViewModels
{
    public class MainViewModel : Notifier
    {
        private Organization selectedOrganization = new Organization();
        private CourseTitle selectedCourse = new CourseTitle();
        private float sum;
        public bool flag;

        #region Properties
        public ObservableCollection<Organization> Organizations { get; set; }
        public ObservableCollection<CourseTitle> CalculationResults { get; set; }
        public ObservableCollection<CalculationResult> Results { get; set; }
        public ObservableCollection<Currenc> UkraineBanks { get; set; }

        public CourseTitle SelectedCourse
        {
            get => selectedCourse;
            set
            {
                selectedCourse = value;
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
            Results = new ObservableCollection<CalculationResult>();
            CalculationResults = new ObservableCollection<CourseTitle>();
            UkraineBanks = new ObservableCollection<Currenc>();
            flag = true;
            GetOrganizations();
            GetExchangeRates();
            GetUkraineBanks();
        }

        #region Methods

        private async void GetOrganizations()
        {
            FinanceManager financeManager = new FinanceManager();
            foreach (var item in await financeManager.GetBanks())
                Organizations.Add(item);
        }

        private async void GetExchangeRates()
        {
            FinanceManager financeManager = new FinanceManager();
            foreach (var item in await financeManager.GetExchangeRates())
                CalculationResults.Add(item);
        }

        private void GetRezult(bool flag)
        {
            Results.Clear();
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
                    Results.Add(calculation);
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
                    Results.Add(calculation);
                }
        }

        private void GetUkraineBanks()
        {
            UkraineBanks.Clear();
            foreach (var item in Organizations)
            {
                foreach (var res in item.Currencies)
                {
                    if (res.Name == selectedCourse.Abbreviation)
                    {
                        Currenc currenc = new Currenc
                        {
                            Name = item.Title,
                            Purchase = res.Purchase,
                            Sale = res.Sale
                        };
                        UkraineBanks.Add(currenc);
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
