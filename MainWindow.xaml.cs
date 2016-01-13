using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Store for the FetchData object property
        /// </summary>
        private static FetchData fd = new FetchData();

        /// <summary>
        /// Store for the dictionary object property
        /// </summary>
        private Dictionary<string, Currency> dictionary;

        /// <summary>
        /// initialize components and show the currency table on the screen.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            GetFromXml ob = fd.LoadAllRates;
            IAsyncResult asyncResult = ob.BeginInvoke(null, null);
            ob.EndInvoke(asyncResult);
            dictionary = fd.MyCurrencies;
            currencies.DataContext = dictionary.Values;
            PopulateComboBoxes();
        }

        /// <summary>
        /// initialize the comboboxes and the textbox.
        /// </summary>
        private void PopulateComboBoxes()
        {
            if (fromComboBox.Items.Count == 0 && toComboBox.Items.Count == 0)
            {
                foreach (Currency c in fd)
                {
                    string code = c.CurrencyCode;
                    fromComboBox.Items.Add(code);
                    toComboBox.Items.Add(code);
                }
            }
            amountTextBox.Text = "1";
        }

        /// <summary>
        /// when clicking on the 'Calculate' button, takes the input from the user and calculate the conversion.
        /// </summary>
        private void OnClick(object sender, RoutedEventArgs e)
        {
            if (!fromComboBox.Text.Equals("") && !toComboBox.Text.Equals(""))
            {
                try
                {
                    double result = fd.CalculateConversion(dictionary[fromComboBox.Text],
                        dictionary[toComboBox.Text], Convert.ToInt32(amountTextBox.Text));
                        answerTextfBox.Text = Convert.ToString(result);
                }
                catch (ExchangeException ex)
                {
                    Console.WriteLine(ex.Message);

                }
                catch (FormatException excp)
                {
                    MessageBox.Show(excp.Message);
                }
            }
            else
            {
                MessageBox.Show("Missing parameters");
            }

        }

        /// <summary>
        /// when clicking on the 'Refresh' button, clear the table and reload it.
        /// </summary>
        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            Refresh ob = fd.RefreshRates;
            IAsyncResult asyncResult = ob.BeginInvoke(null, null);
            ob.EndInvoke(asyncResult);
            dictionary = fd.MyCurrencies;
            currencies.DataContext = null;
            currencies.DataContext = dictionary.Values;
            PopulateComboBoxes();
        }
    }
}
