using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BrgyProfileCore.Windows
{
    /// <summary>
    /// Interaction logic for PrinterSelectDialogWindow.xaml
    /// </summary>
    public partial class PrinterSelectDialogWindow : Window
    {
        public string SelectedPrinter { get; set; }
        public PrinterSelectDialogWindow()
        {
            InitializeComponent();

            printerListBox.ItemsSource = System.Drawing.Printing.PrinterSettings.InstalledPrinters;
        }

        private void printButton_Click(object sender, RoutedEventArgs e)
        {
            var printer = (string)printerListBox.SelectedItem;
            if (printer == null)
            {
                MessageBox.Show("Select a printer first.");
                return;
            }

            this.SelectedPrinter = printer;
            this.DialogResult = true;
            this.Close();
        }
    }
}
