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
using Microsoft.Win32;

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
            BrgyStatistics.RefreshStatistics();

            BrgyNameLabel.Content = BrgyProfileCore.Properties.Settings.Default.BrgyName;
            MunicipalityLabel.Content = BrgyProfileCore.Properties.Settings.Default.Municipality;
            ProvinceLabel.Content = BrgyProfileCore.Properties.Settings.Default.Province;

            ResidentsAge0_3yrs.FieldValueText = $"{BrgyStatistics.TotalResidentsByAge(0, 3)}";
            ResidentsAge4_6yrs.FieldValueText = $"{BrgyStatistics.TotalResidentsByAge(4, 6)}";
            ResidentsAge7_11yrs.FieldValueText = $"{BrgyStatistics.TotalResidentsByAge(7, 11)}";
            ResidentsAge12_20yrs.FieldValueText = $"{BrgyStatistics.TotalResidentsByAge(12, 20)}";
            ResidentsAge21_35yrs.FieldValueText = $"{BrgyStatistics.TotalResidentsByAge(21, 35)}";
            ResidentsAge36_50yrs.FieldValueText = $"{BrgyStatistics.TotalResidentsByAge(36, 50)}";
            ResidentsAge51_80yrs.FieldValueText = $"{BrgyStatistics.TotalResidentsByAge(51, 80)}";
            ResidentsAge81_aboveyrs.FieldValueText = $"{BrgyStatistics.TotalResidentsByAge(81)}";
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

        private void reportButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.DefaultExt = "xls";
            dialog.Filter = "Spreadsheet Files |*.xls";
            dialog.AddExtension = true;
            var datesuffix = DateTime.Now.ToString("_MM-dd_HH-mm-ss");
            dialog.FileName = $"Report{datesuffix}.xls";

            if (dialog.ShowDialog() == true)
            {
                PrintHelper.ExportReportSheet(dialog.FileName);
            }
        }
    }
}
