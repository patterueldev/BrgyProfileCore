﻿using System;
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

namespace BrgyProfileCore.Windows.Households
{
    using Windows.Residents;

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
            if (selectedHousehold == null)
            {
                return;
            }

            ResidentListWindow resWin = new ResidentListWindow(selectedHousehold);
            resWin.ShowDialog();
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
            printRBIButton.IsEnabled = idx >= 0;
            exportRBIButton.IsEnabled = idx >= 0;
            exportSheetButton.IsEnabled = idx >= 0;
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

        private void printRBIButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedHousehold == null)
            {
                return;
            }

            var household = selectedHousehold;

            var db = new BrgyContext();
            var residents = db.Residents
            .Include(r => r.Household)
            .Include(r => r.Sitio)
            .ToList()
            .FindAll(resident =>
            {
                if (household == null)
                {
                    return false;
                }
                return resident.HouseholdId == household.HouseholdId;
            });

            var dialog = new PrinterSelectDialogWindow();
            var result = dialog.ShowDialog();
            if(result == true)
            {
                PrintHelper.PrintHouseholdToPDF(residents, "test", true, dialog.SelectedPrinter);
            }

        }

        private void exportRBIButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedHousehold == null)
            {
                return;
            }

            var household = selectedHousehold;

            var dialog = new SaveFileDialog();
            dialog.DefaultExt = "pdf";
            dialog.Filter = "PDF Files |*.pdf";
            dialog.AddExtension = true;
            var datesuffix = DateTime.Now.ToString("_MM-dd_HH-mm-ss");
            dialog.FileName = $"RBI{datesuffix}.pdf";

            if (dialog.ShowDialog() == true)
            {
                var db = new BrgyContext();
                var residents = db.Residents
                .Include(r => r.Household)
                .Include(r => r.Sitio)
                .ToList()
                .FindAll(resident =>
                {
                    if (household == null)
                    {
                        return false;
                    }
                    return resident.HouseholdId == household.HouseholdId;
                });

                PrintHelper.PrintHouseholdToPDF(residents, dialog.FileName, false);
            }
        }

        private void exportSheetButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedHousehold == null)
            {
                return;
            }

            var household = selectedHousehold;
            var dialog = new SaveFileDialog();
            dialog.DefaultExt = "xls";
            dialog.Filter = "Spreadsheet Files |*.xls";
            dialog.AddExtension = true;
            var datesuffix = DateTime.Now.ToString("_MM-dd_HH-mm-ss");
            dialog.FileName = $"RBI{datesuffix}.xls";

            if (dialog.ShowDialog() == true)
            {
                var db = new BrgyContext();
                var residents = db.Residents
                .Include(r => r.Household)
                .Include(r => r.Sitio)
                .ToList()
                .FindAll(resident =>
                {
                    if (household == null)
                    {
                        return false;
                    }
                    return resident.HouseholdId == household.HouseholdId;
                });

                PrintHelper.ExportRBISheet(residents, dialog.FileName);
            }
        }
    }
}
