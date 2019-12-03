using CurrencyConverter.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Models
{
    public class CourseTitle : Notifier
    {
        private string abbreviation;
        private string decodingAbbreviations;


        #region Properties
        public string Abbreviation
        {
            get => abbreviation;
            set
            {
                abbreviation = value;
                Notify();
            }
        }

        public string DecodingAbbreviations
        {
            get => decodingAbbreviations;
            set
            {
                decodingAbbreviations = value;
                Notify();
            }
        }
        #endregion

        public override string ToString()
        {
            return $"{abbreviation}: {decodingAbbreviations}";
        }
    }
}
