using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class Currency
    {
        /// <summary>
        /// Store for the name property
        /// </summary>
        private string name;
        /// <summary>
        /// Store for the unit property
        /// </summary>
        private int unit;
        /// <summary>
        /// Store for the currencyCode property
        /// </summary>
        private string currencyCode;
        /// <summary>
        /// Store for the rate property
        /// </summary>
        private double rate;
        /// <summary>
        /// Store for the country property
        /// </summary>
        private string country;
        /// <summary>
        /// Store for the change property
        /// </summary>
        private double change;

        /// <summary>
        /// The class constructor. 
        /// </summary>
        public Currency(string nameVal, int unitVal, string codeVal, string countryVal, double rateVal, double changeVal)
        {
            Name = nameVal;
            Unit = unitVal;
            CurrencyCode = codeVal;
            Rate = rateVal;
            Country = countryVal;
            Change = changeVal;
        }

        /// <summary>
        /// Name property 
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (!value.Equals(""))
                    name = value;
                else throw new ExchangeException("Name value is not set");
            }
        }
        /// <summary>
        /// Unit property 
        /// </summary>
        public int Unit
        {
            get
            {
                return unit;
            }
            set
            {
                if (value>0)
                    unit = value;
                else throw new ExchangeException("Unit value is not set");
            }
        }
        /// <summary>
        /// CurrencyCode property 
        /// </summary>
        public string CurrencyCode
        {
            get
            {
                return currencyCode;
            }
            set
            {
                if (!value.Equals(""))
                    currencyCode = value;
                else throw new ExchangeException("CurrencyCode value is not set");
            }
        }
        /// <summary>
        /// Rate property 
        /// </summary>
        public double Rate
        {
            get
            {
                return rate;
            }
            set
            {
                if (value>0)
                    rate = value;
                else throw new ExchangeException("Rate value is not set");
            }
        }
        /// <summary>
        /// Country property 
        /// </summary>
        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                if (!value.Equals(""))
                    country = value;
                else throw new ExchangeException("Country value is not set");
            }
        }
        /// <summary>
        /// Change property 
        /// </summary>
        public double Change
        {
            get
            {
                return change;
            }
            set
            {
                change = value;
            }
        }
        /// <summary>
        /// implicit Currency to double conversion operator
        /// </summary>
        /// <returns>
        /// Returns the currency rate as double from the Currency object.
        /// </returns>
        public static implicit operator double(Currency c) 
        {
            return c.Rate;
        }
    }
}
