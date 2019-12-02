using CurrencyConverter.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Models
{
    public class CalculationResult : Notifier
    {
        private string name;
        private float result;
        private float finalCourse;

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
        public float Result
        {
            get => result;
            set
            {
                result = value;
                Notify();
            }
        }
        public float FinalCourse
        {
            get => finalCourse;
            set
            {
                finalCourse = value;
                Notify();
            }
        }


        #endregion
    }
}
