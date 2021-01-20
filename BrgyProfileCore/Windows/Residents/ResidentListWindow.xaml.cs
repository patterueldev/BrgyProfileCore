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
            var households = db.Households.ToList();
            HouseholdBox.ItemsSource = households;
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

            HouseholdBox.SelectedItem = households.FirstOrDefault(h => h.HouseholdId == household.HouseholdId);
            HouseholdBox.IsEnabled = false;
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
            ResidentDetailsButton.IsEnabled = idx >= 0;
            EditResidentButton.IsEnabled = idx >= 0;
            DeleteResidentButton.IsEnabled = idx >= 0;
        }

        /// <summary>
        /// Refreshes data grid
        /// </summary>
        public void refreshList(string keyword = null)
        {
            var db = new BrgyContext();
            residentsDataGrid.ItemsSource = db.Residents.Include(r => r.Household)
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
        }
    }
}
