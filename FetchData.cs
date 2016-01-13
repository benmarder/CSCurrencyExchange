using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace WpfApplication1
{
    delegate void GetFromXml();
    delegate void Refresh();

    class FetchData : IReadable
    {
        /// <summary>
        /// Store for the dictionary property
        /// </summary>
        private static Dictionary<string, Currency> myCurrencies = new Dictionary<string,Currency>();
        /// <summary>
        /// Store for the xml property
        /// </summary>
        private XDocument myXml;
        private const string SHEKEL_NAME = "Shekel";
        private const string SHEKEL_CODE = "ILS";
        private const string SHEKEL_COUNTRY = "Israel";

        /// <summary>
        /// MyCurrencies property 
        /// </summary>
        public Dictionary<string, Currency> MyCurrencies
        {
            get
            {
                return myCurrencies;
            }
        }

        /// <summary>
        /// get currencies data from the web and put it in the dictionary.
        /// </summary>
        public void LoadAllRates()
        {
            try
            {
                myXml = XDocument.Load("http://boi.org.il/currency.xml"); // Loads the XML document.

            }
            catch (System.Net.WebException e)
            {
                MessageBox.Show("Please connect to the internet");
                System.Environment.Exit(1);
            }
            var currencies = from x in myXml.Descendants("CURRENCY")
                             select new
                             {
                                 Name = x.Descendants("NAME").First().Value,
                                 Unit = x.Descendants("UNIT").First().Value,
                                 CurrencyCode = x.Descendants("CURRENCYCODE").First().Value,
                                 Country = x.Descendants("COUNTRY").First().Value,
                                 Rate = x.Descendants("RATE").First().Value,
                                 Change = x.Descendants("CHANGE").First().Value
                             };

            foreach (var currency in currencies) //add the values to our dictionary
            {
                try 
                {
                    MyCurrencies.Add(currency.CurrencyCode, new Currency(currency.Name, Convert.ToInt32(currency.Unit),
                        currency.CurrencyCode, currency.Country, Convert.ToDouble(currency.Rate), Convert.ToDouble(currency.Change)));
                }
                catch(ExchangeException e)
                {
                    Console.WriteLine(e.Data);
                }
            }
            MyCurrencies.Add(SHEKEL_CODE, new Currency(SHEKEL_NAME, 1, SHEKEL_CODE, SHEKEL_COUNTRY, 1, 0)); //adding the shekel to the dictionary
        }

        /// <summary>
        /// calculate the conversion from any currency to another.
        /// </summary>
        /// <returns>
        /// Returns the result from the conversion.
        /// </returns>7
        public double CalculateConversion(Currency fromCurrency, Currency toCurrecy, int amount)
        {
            if (amount>=0)
            {
                return (amount * fromCurrency / fromCurrency.Unit) / (toCurrecy / toCurrecy.Unit);
            }
           MessageBox.Show("minus amount is not allowed ");
           return 0;
        }

        /// <summary>
        /// get the Enumeragtor object so that we can use foreach statement for the FetchData class.
        /// </summary>
        public System.Collections.IEnumerator GetEnumerator()
        {
            foreach (Currency c in MyCurrencies.Values)
            {
                yield return c;
            }
        }

        /// <summary>
        /// remove all the old data from the dictionary and reload it.
        /// </summary>
        public void RefreshRates()
        {
            MyCurrencies.Clear();
            LoadAllRates();
        }

    }
}
