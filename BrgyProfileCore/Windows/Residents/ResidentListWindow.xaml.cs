using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using BrgyProfileCore.Core;

namespace BrgyProfileCore.Windows.Residents
{
    /// <summary>
    /// Interaction logic for ResidentList.xaml
    /// </summary>
    public partial class ResidentListWindow : Window
    {
        Resident selectedResident
        {
            get
            {
                // loads index of selected row
                var idx = residentsDataGrid.SelectedIndex;

                if (idx < 0 || idx >= residentsDataGrid.Items.Count)
                {
                    return null;
                }
                return (Resident)residentsDataGrid.Items[idx];
            }
        }

        /// <summary>
        /// Class Initializer
        /// </summary>
        public ResidentListWindow()
        {
            InitializeComponent();

            var db = new BrgyContext();
            HouseholdBox.ItemsSource = db.Households.ToList();
            SitioBox.ItemsSource = db.Sitio.ToList();
        }

        /// <summary>
        /// Class Initializer
        /// </summary>
        public ResidentListWindow(Household household)
        {
            InitializeComponent();

            var db = new BrgyContext();
            var households = db.Households.ToList();
            HouseholdBox.ItemsSource = households;

            var sitioList = db.Sitio.ToList();
            SitioBox.ItemsSource = sitioList;

            HouseholdBox.SelectedItem = households.FirstOrDefault(h => h.HouseholdId == household.HouseholdId);
            HouseholdBox.IsEnabled = false;
        }

        /// <summary>
        /// Class Initializer
        /// </summary>
        public ResidentListWindow(Sitio sitio)
        {
            InitializeComponent();

            var db = new BrgyContext();
            var households = db.Households.ToList();
            HouseholdBox.ItemsSource = households;

            var sitioList = db.Sitio.ToList();
            SitioBox.ItemsSource = sitioList;

            SitioBox.SelectedItem = sitioList.FirstOrDefault(s => s.SitioId == sitio.SitioId);
            SitioBox.IsEnabled = false;
        }

        /// <summary>
        /// Window did Activate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Activated(object sender, EventArgs e)
        {
            this.refreshList();
        }

        /// <summary>
        /// Edit Resident Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResidentDetailsButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (selectedResident == null)
            {
                return;
            }

            var window = new ViewResidentWindow(selectedResident);
            window.ShowDialog();
        }

        /// <summary>
        /// Edit Resident Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditResidentButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (selectedResident == null)
            {
                return;
            }

            var window = new UpsertResidentWindow(selectedResident);
            window.ShowDialog();
        }
        /// <summary>
        /// Delete Resident Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteResidentButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedResident == null)
            {
                return;
            }

            var resident = selectedResident;
            MessageBoxResult messageBoxResult = MessageBox.Show($"Delete {selectedResident.FullName} from the records? This action cannot be undone.", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var db = new BrgyContext();
                db.Remove(resident);
                db.SaveChanges();

                this.refreshList();
            }
        }

        /// <summary>
        /// Add Resident Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddResidentButton_Click(object sender, RoutedEventArgs e)
        {
            // Shows Add Resident window when tapped
            UpsertResidentWindow addWindow = new UpsertResidentWindow();
            addWindow.ShowDialog();
        }

        /// <summary>
        /// Selection event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void residentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // loads index of selected row
            var idx = residentsDataGrid.SelectedIndex;

            // toggles buttons
            EditResidentButton.IsEnabled = idx >= 0;
            DeleteResidentButton.IsEnabled = idx >= 0;
        }

        /// <summary>
        /// Refreshes data grid
        /// </summary>
        public void refreshList(string keyword = null)
        {
            var db = new BrgyContext();
            residentsDataGrid.ItemsSource = db.Residents
                .Include(r => r.Household)
                .Include(r => r.Sitio)
                .ToList()
                .FindAll(resident =>
                {
                    if (keyword == null)
                    {
                        return true;
                    }
                    return resident.FullName.ToLower().Contains(keyword.ToLower());
                })
                .FindAll( resident =>
                {
                    var selectedHousehold = (Household)HouseholdBox.SelectedItem;
                    if (selectedHousehold == null)
                    {
                        return true;
                    }
                    return resident.HouseholdId == selectedHousehold.HouseholdId;
                })
                .FindAll(resident =>
                {
                    var selectedSitio = (Sitio)SitioBox.SelectedItem;
                    if (selectedSitio == null)
                    {
                        return true;
                    }
                    return resident.SitioId == selectedSitio.SitioId;
                });
        }

        /// <summary>
        /// Search Text Box Text Change Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = SearchTextBox.Text;
            this.refreshList(keyword);
        }

        private void HouseholdBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var keyword = SearchTextBox.Text;
            this.refreshList(keyword);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            if (HouseholdBox.IsEnabled)
            {
                HouseholdBox.SelectedIndex = -1;
            }

            if (SitioBox.IsEnabled)
            {
                SitioBox.SelectedIndex = -1;
            }
        }

        private void SitioBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var keyword = SearchTextBox.Text;
            this.refreshList(keyword);
        }

        private void exportResidentButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.DefaultExt = "pdf";
            dialog.Filter = "PDF Files |*.pdf";
            dialog.AddExtension = true;
            var datesuffix = DateTime.Now.ToString("_MM-dd_HH-mm-ss");
            dialog.FileName = $"Residents{datesuffix}.pdf";

            if (dialog.ShowDialog() == true)
            {
                var db = new BrgyContext();
                var residents = db.Residents
                .Include(r => r.Household)
                .Include(r => r.Sitio)
                .ToList();

                PrintHelper.PrintResidentsToPDF(residents, dialog.FileName, false);
            }
        }

        private void printResidentsButton_Click(object sender, RoutedEventArgs e)
        {
            var db = new BrgyContext();
            var residents = db.Residents
            .Include(r => r.Household)
            .Include(r => r.Sitio)
            .ToList()
            .FindAll(resident =>
            {
                var selectedHousehold = (Household)HouseholdBox.SelectedItem;
                if (selectedHousehold == null)
                {
                    return true;
                }
                return resident.HouseholdId == selectedHousehold.HouseholdId;
            });

            var dialog = new PrinterSelectDialogWindow();
            var result = dialog.ShowDialog();
            if (result == true)
            {
                PrintHelper.PrintResidentsToPDF(residents, "test", true, dialog.SelectedPrinter);
            }
        }
    }
}
