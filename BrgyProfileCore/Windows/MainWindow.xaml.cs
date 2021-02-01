﻿using System;
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

namespace BrgyProfileCore.Windows
{
    using Residents;
    using Households;
    using SitioModule;

    using Core;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Selected Resident from the data grid
        /// </summary>
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
        /// Constructor for Main Window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            if(BrgySession.current == null)
            {
                // open login
                Visibility = Visibility.Hidden;
                var window = new LoginWindow();
                window.mainWindow = this;
                window.ShowDialog();
            }
        }

        /// <summary>
        /// Window did Activate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Activated(object sender, EventArgs e)
        {
            this.refreshList();

            BrgyNameLabel.Content = BrgyProfileCore.Properties.Settings.Default.BrgyName;
            MunicipalityLabel.Content = BrgyProfileCore.Properties.Settings.Default.Municipality;
            ProvinceLabel.Content = BrgyProfileCore.Properties.Settings.Default.Province;

            AverageResidentperHouseholdDetail.FieldValueText = $"{BrgyStatistics.AverageResidentPerHousehold}";
            AveragePopulationperSitioDetail.FieldValueText = $"{BrgyStatistics.AveragePopulationperSitio}";
            AverageResident1_18perSitioDetail.FieldValueText = $"{BrgyStatistics.AverageResident1_18perSitio}";
            AverageResident19_50perSitioDetail.FieldValueText = $"{BrgyStatistics.AverageResident19_50perSitio}";
            AverageResident50_AboveperSitioDetail.FieldValueText = $"{BrgyStatistics.AverageResident51_AboveperSitio}";
        }

        /// <summary>
        /// View All Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewAllResidentsButton_Click(object sender, RoutedEventArgs e)
        {
            ResidentListWindow resWin = new ResidentListWindow();
            resWin.ShowDialog();
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

        private void AddResidentButton_Click(object sender, RoutedEventArgs e)
        {
            UpsertResidentWindow addWindow = new UpsertResidentWindow();
            addWindow.ShowDialog();
        }

        private void residentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // loads index of selected row
            var idx = residentsDataGrid.SelectedIndex;

            // toggles buttons
            EditResidentButton.IsEnabled = idx >= 0;
        }

        /// <summary>
        /// Refreshes data grid
        /// </summary>
        public void refreshList()
        {
            var db = new BrgyContext();
            residentsDataGrid.ItemsSource = db.Residents.ToList();
            householdsDataGrid.ItemsSource = db.Households.ToList();
            sitioDataGrid.ItemsSource = db.Sitio.ToList();
        }

        private void ViewAllHouseholdsButton_Click(object sender, RoutedEventArgs e)
        {
            HouseholdListWindow window = new HouseholdListWindow();
            window.ShowDialog();
        }

        private void AddHouseholdButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new UpsertHouseholdWindow();
            window.ShowDialog();
        }

        private void householdsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ViewAllSitioButton_Click(object sender, RoutedEventArgs e)
        {
            SitioListWindow window = new SitioListWindow();
            window.ShowDialog();
        }

        private void AddSitioButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new UpsertSitioWindow();
            window.ShowDialog();
        }

        private void UpdateProfileButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new UpdateProfileWindow();
            window.ShowDialog();
        }

        private void EditResidentButton_Clicked(object sender, RoutedEventArgs e)
        {
            if (selectedResident == null)
            {
                return;
            }

            var window = new UpsertResidentWindow(selectedResident);
            window.ShowDialog();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            BrgySession.LogoutUser();

            if (BrgySession.current == null)
            {
                // open login
                Visibility = Visibility.Hidden;
                var window = new LoginWindow();
                window.mainWindow = this;
                window.ShowDialog();
            }
        }
    }
}
