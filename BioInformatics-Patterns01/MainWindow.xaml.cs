using System;
using System.Collections.Generic;
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

namespace BioInformatics_Patterns01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int PatternLength;
        public int DecimalIndex;
        public int Decimal;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_DecimalToDna(object sender, RoutedEventArgs e)
        {
            txtStatus.Text = "";

            if (txtLength.Text == "")
            {
                txtStatus.Text = @"Length invalid";
                return;
            }

            if (!Int32.TryParse(txtLength.Text, out PatternLength))
            {
                txtStatus.Text = @"Length invalid";
                return;
            }

            if (PatternLength < 1)
            {
                txtStatus.Text = @"Length invalid";
                return;
            }

            if (txtDecimal.Text == "")
            {
                txtStatus.Text = @"Decimal is invalid";
                return;
            }

            if (!Int32.TryParse(txtDecimal.Text, out DecimalIndex))
            {
                txtStatus.Text = @"Decimal is invalid";
                return;
            }

            if (DecimalIndex < 1)
            {
                txtStatus.Text = @"Decimal is invalid";
                return;
            }
            
            if ((Math.Pow(4, PatternLength)) <= DecimalIndex)
            {
                txtStatus.Text = @"Decimal or Length invalid";
                return;
            }

            string result = "";
            for (int i = 0; i < PatternLength; i++)
            {
                var lowEnd = DecimalIndex % 4;
                switch (lowEnd)
                {
                    case 0:
                        result = "A" + result;
                        break;
                    case 1:
                        result = "C" + result;
                        break;
                    case 2:
                        result = "G" + result;
                        break;
                    case 3:
                        result = "T" + result;
                        break;
                }

                DecimalIndex = DecimalIndex / 4;
            }

            txtDNA.Text = result;
        }

        private void Button_DnaToDecimal(object sender, RoutedEventArgs e)
        {
            if (txtDNA.Text == "")
            {
                txtStatus.Text = @"DNA invalid";
                return;
            }

            txtStatus.Text = "";
            txtDecimal.Text = "";

            txtLength.Text = txtDNA.Text.Length.ToString();

            Decimal = 0;
            int j = 0;
            for (int i = txtDNA.Text.Length - 1; i >= 0; i--)
            {
                var piece = txtDNA.Text.Substring(i, 1);

                switch (piece)
                {
                    case "A":
                        Decimal += (int) Math.Pow(4, j) * 0;
                        break;
                    case "C":
                        Decimal += (int) Math.Pow(4, j) * 1;
                        break;
                    case "G":
                        Decimal += (int) Math.Pow(4, j) * 2;
                        break;
                    case "T":
                        Decimal += (int) Math.Pow(4, j) * 3;
                        break;
                    default:
                        txtStatus.Text = @"DNA Invalid";
                        return;
                }

                j++;
            }

            txtDecimal.Text = Decimal.ToString();
        }
    }
}
