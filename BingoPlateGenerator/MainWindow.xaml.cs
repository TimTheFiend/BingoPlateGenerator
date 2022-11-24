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
using System.Drawing;
using System.Windows.Threading;
using System.Threading;

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

        private SynchronizationContext mainThread;


        public MainWindow()
        {
            InitializeComponent();
            /* Initialise fields */
            outputDirectory = "";
            tbTitle = this.txtbxTitle;
            tbAmount = this.txtbxPlateAmount;

            this.lbLoad.Visibility = Visibility.Hidden;

            mainThread = SynchronizationContext.Current;
            if (mainThread == null)
            {
                throw new Exception("NO CONTEXT");
            }

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


        private delegate void printDelegate(string text, string amount, string output, Bitmap bitmap);
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

            this.lbLoad.Visibility = Visibility.Visible;
            this.lbLoad.IsIndeterminate = true;


            DisableControlsDuringGeneration();
            PrintPlates(tbTitle.Text, tbAmount.Text, outputDirectory, Properties.Resources.CardTemplate);
        }

        private async void PrintPlates(string text, string amount, string outputDirectory, Bitmap cardTemplate)
        {

            await Task.Run(() => {
                    var id = PlateFactory.CreatePlates(Convert.ToInt32(amount)).ToList();
                    BingoPrinter.PrintPlates(id, text, outputDirectory, cardTemplate);
                });

            mainThread.Send((object state) => {
                OnSuccessfulPrint();
                DisableControlsDuringGeneration();
            }, null);
        }

        private void DisableControlsDuringGeneration()
        {
            txtbxPlateAmount.IsEnabled = !txtbxPlateAmount.IsEnabled;
            txtbxTitle.IsEnabled = !txtbxTitle.IsEnabled;
            btnGenerate.IsEnabled = !btnGenerate.IsEnabled;
            btnOutput.IsEnabled = !btnOutput.IsEnabled;
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
                this.lbLoad.Visibility = Visibility.Hidden;
                this.lbLoad.IsIndeterminate = false;
            }
        }
    }
