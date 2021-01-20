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

namespace BrgyProfileCore.Windows.Households
{
    /// <summary>
    /// Interaction logic for HouseholdListWindow.xaml
    /// </summary>
    public partial class HouseholdListWindow : Window
    {
        Household selectedHousehold
        {
            get
            {
                // loads index of selected row
                var idx = householdsDataGrid.SelectedIndex;

                if (idx < 0 || idx >= householdsDataGrid.Items.Count)
                {
                    return null;
                }
                return (Household)householdsDataGrid.Items[idx];
            }
        }
        public HouseholdListWindow()
        {
            InitializeComponent();
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

        private void AddHouseholdButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new UpsertHouseholdWindow();
            window.ShowDialog();
        }

        private void HouseholdResidentsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditHouseholdButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedHousehold == null)
            {
                return;
            }

            var window = new UpsertHouseholdWindow(selectedHousehold);
            window.ShowDialog();
        }

        private void DeleteHouseholdButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedHousehold == null)
            {
                return;
            }

            var household = selectedHousehold;
            MessageBoxResult messageBoxResult = MessageBox.Show($"Delete {selectedHousehold.HouseholdName} from the records? This action cannot be undone.", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var db = new BrgyContext();
                db.Remove(household);
                db.SaveChanges();

                this.refreshList();
            }
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

        private void householdsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // loads index of selected row
            var idx = householdsDataGrid.SelectedIndex;

            // toggles buttons
            HouseholdResidentsButton.IsEnabled = idx >= 0;
            EditHouseholdButton.IsEnabled = idx >= 0;
            DeleteHouseholdButton.IsEnabled = idx >= 0;
        }

        /// <summary>
        /// Refreshes data grid
        /// </summary>
        public void refreshList(string keyword = null)
        {
            var db = new BrgyContext();
            householdsDataGrid.ItemsSource = db.Households.Include(h => h.Residents).ToList().FindAll(household =>
            {
                if (keyword == null)
                {
                    return true;
                }
                var residents = new List<Resident>();
                if (household.Residents.Count > 0)
                {
                    residents = household.Residents.FindAll(resident =>
                    {
                        return resident.FullName.ToLower().Contains(keyword.ToLower());
                    });
                }
                return household.HouseholdName.ToLower().Contains(keyword.ToLower()) || residents.Count > 0;
            });
        }
    }
}
