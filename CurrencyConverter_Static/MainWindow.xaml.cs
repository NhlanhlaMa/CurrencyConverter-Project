using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace CurrencyConverter_Static
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BindCurrency();

            // Sql Call
        }

        private void BindCurrency()
        {
            DataTable dtCurrency = new DataTable();
            dtCurrency.Columns.Add("Text");
            dtCurrency.Columns.Add("Value");

            //Add rown in the Datatable with text and value
            dtCurrency.Rows.Add("--SELECT--", 0);
            dtCurrency.Rows.Add("INR", 1);
            dtCurrency.Rows.Add("USD", 75);
            dtCurrency.Rows.Add("EUR", 85);
            dtCurrency.Rows.Add("SAR", 20);
            dtCurrency.Rows.Add("POUND", 5);
            dtCurrency.Rows.Add("DEM", 43);

            cmbFromCurrency.ItemsSource = dtCurrency.DefaultView;
            cmbFromCurrency.DisplayMemberPath = "Text";
            cmbFromCurrency.SelectedValuePath = "Value";
            cmbFromCurrency.SelectedIndex = 0;

            cmbToCurrency.ItemsSource = dtCurrency.DefaultView;
            cmbToCurrency.DisplayMemberPath = "Text";
            cmbToCurrency.SelectedValuePath = "Value";
            cmbToCurrency.SelectedIndex = 0;
        }



        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            // Create the variable as Convertable with double
            double ConvertableValue;

            // Check if the amount textbox is Null or Blank
            if(txtCurrency.Text == null || txtCurrency.Text.Trim() == "")
            {
                //If amounf textbox is Null or Blank it will show this message box
                MessageBox.Show("Please Enter Currency", "information", MessageBoxButton.OK, MessageBoxImage.Information);
                //After clicking on message box OK set focus on amount textbox
                txtCurrency.Focus();
                return;
            }
            //Else if currency From is not selected or select default text --SELECT--
            else if(cmbFromCurrency.SelectedValue == null || cmbFromCurrency.SelectedIndex == 0)
            {
                // Show the message
                MessageBox.Show("please Select currency From", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                // Set focus on the From Combobox
                cmbFromCurrency.Focus();
            }
            // Else if currency To is not selected or select default text --SELECT--
            else if(cmbToCurrency.SelectedValue == null || cmbToCurrency.SelectedIndex == 0)
            {
                // Show the message
                MessageBox.Show("please Select currency To", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                // Set focus on the From Combobox
                cmbToCurrency.Focus();
            }

            // Check if From and To Combobox selected values are same
            if(cmbFromCurrency.Text == cmbToCurrency.Text)
            {
                // Amount textbox value set in ConvertableValue
                //double.parse is used for convertinf the datatype String To double.
                //textbox text have string and ConvertedValue is double Datatype
                ConvertableValue = double.Parse(txtCurrency.Text);
                //Show the label converted currency and converted currency name and ToString("N3") is used to place 000 after the decimal point
                lblCurrency.Content = cmbToCurrency.Text + " " + ConvertableValue.ToString("N3");
            }
            else
            {
                // Calculate for currency converter is From Currency value multply(*)
                // with the amount textbox value and then that total devided(/) with To Currency value.
                ConvertableValue = (double.Parse(cmbFromCurrency.SelectedValue.ToString()) *
                    double.Parse(txtCurrency.Text)) / 
                    double.Parse(cmbToCurrency.SelectedValue.ToString());

                // show the label converted currency and converted currency name
                lblCurrency.Content = cmbToCurrency.Text + " " + ConvertableValue.ToString("N3");
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
        }

        private void ClearControls()
        {
            txtCurrency.Text = string.Empty;
            if (cmbFromCurrency.Items.Count > 0)
                cmbFromCurrency.SelectedIndex = 0;
            if (cmbToCurrency.Items.Count > 0)
                cmbToCurrency.SelectedIndex = 0;

            lblCurrency.Content = "";
            txtCurrency.Focus();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
