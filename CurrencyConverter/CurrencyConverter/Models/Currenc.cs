﻿using CurrencyConverter.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Models
{
    public class Currenc : Notifier
    {
        private string name;
        private float purchase;
        private float sale;

        #region Properties
        public string Name
        {
            get => name;
            set
            {
                name = value;
                Notify();
            }
        }

        [JsonProperty(PropertyName = "ask")]
        public float Purchase
        {
            get => purchase;
            set
            {
                purchase = value;
                Notify();
            }
        }

        [JsonProperty(PropertyName = "bid")]
        public float Sale
        {
            get => sale;
            set
            {
                sale = value;
                Notify();
            }
        }

        
        #endregion

    }
}
