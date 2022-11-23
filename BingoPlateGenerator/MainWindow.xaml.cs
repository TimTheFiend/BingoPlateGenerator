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
using System.Text.RegularExpressions;
using System.IO;

namespace BingoPlateGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TextBox tbTitle;
        private TextBox tbAmount;
        private string outputDirectory;


        public MainWindow()
        {
            InitializeComponent();
            /* Initialise fields */
            outputDirectory = "";
            tbTitle = this.txtbxTitle;
            tbAmount = this.txtbxPlateAmount;



            this.btnGenerate.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(btnGenerate_PreviewMouseLeftButtonUp);
            this.btnOutput.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(btnOutput_PreviewMouseLeftButtonUp);
        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnOutput_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            outputDirectory = "";

            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    outputDirectory = dialog.SelectedPath;
                }
            }
        }

        private void btnGenerate_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (String.IsNullOrEmpty(tbTitle.Text) || String.IsNullOrEmpty(tbAmount.Text))
            {
                return;
            }
            if (!Directory.Exists(outputDirectory))
            {
                return;
            }
            var id = PlateFactory.CreatePlates(Convert.ToInt32(tbAmount.Text)).ToList();

            BingoPrinter.PrintPlates(id, tbTitle.Text, outputDirectory, Properties.Resources.CardTemplate);

            OnSuccessfulPrint();
        }

        private void OnSuccessfulPrint()
        {
            outputDirectory = "";
            tbAmount.Text = String.Empty;
            tbTitle.Text = String.Empty;

            string msgTxt = "Successfully generated bingo plates.";
            string msgCaption = "Success!";

            MessageBoxButton msgBtns = MessageBoxButton.OK;
            MessageBox.Show(msgTxt, msgCaption, msgBtns);
        }
    }
}
