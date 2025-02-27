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

namespace BrgyProfileCore.Windows.Households
{
    /// <summary>
    /// Interaction logic for UpsertHouseholdWindow.xaml
    /// </summary>
    public partial class UpsertHouseholdWindow : Window
    {
        BrgyContext db = new BrgyContext();
        public Household household;
        List<Resident> selectedResidents
        {
            get
            {
                List<Resident> residents = (List<Resident>)residentsSelectionDataGrid.ItemsSource;
                if (residents.Count == 0)
                {
                    return new List<Resident>();
                }
                return residents.FindAll(resident =>
                {
                    var checkbox = (CheckBox)residentSelectionColumn.GetCellContent(resident);
                    if (checkbox == null)
                    {
                        return false;
                    }
                    return checkbox.IsChecked == true;
                });
            }
            set
            {
                List<Resident> residents = (List<Resident>)residentsSelectionDataGrid.ItemsSource;
                foreach (var resident in residents)
                {
                    var checkbox = (CheckBox)residentSelectionColumn.GetCellContent(resident);
                    var res = value.FirstOrDefault(r => { return r.ResidentId == resident.ResidentId; });
                    checkbox.IsChecked = res != null;
                }
            }
        }

        int? headResidentId
        {
            get
            {
                var headResident = (Resident)HeadResidentBox.SelectedItem;
                if (headResident != null)
                {
                    return headResident.ResidentId;
                }
                return null;
            }
        }

        public UpsertHouseholdWindow()
        {
            InitializeComponent();
            this.refreshList();
        }

        public UpsertHouseholdWindow(Household household)
        {
            InitializeComponent();

            this.household = db.Households.Include(h => h.Residents).First(h => h.HouseholdId == household.HouseholdId);

            HouseholdNameField.Text = this.household.HouseholdName;
            this.refreshList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (HouseholdNameField.Text.Trim() == "")
            {
                this.ShowInvalidInputMessage("Household name must not be empty");
                return;
            }

            var residents = selectedResidents.ToList();
            if (household == null)
            {
                // Is Adding
                this.household = new Household
                {
                    HouseholdName = HouseholdNameField.Text.Trim(),
                    HeadResidentId = headResidentId,
                };
                db.Add(this.household);
            }
            else
            {
                // Is Editing
                this.household.HouseholdName = HouseholdNameField.Text.Trim();
                this.household.HeadResidentId = headResidentId;

                db.Update(this.household);
            }

            if (this.household.HasResidents())
            {
                var existingResidents = this.household.Residents.ToList();

                existingResidents.ForEach(resident =>
                {
                    resident.Household = null;
                });
                db.UpdateRange(existingResidents);
            }

            if (residents.Count > 0)
            {
                residents.ForEach(resident =>
                {
                    resident.Household = this.household;
                });
                db.UpdateRange(residents);
            }

            db.SaveChanges();
            MessageBox.Show("Your changes have been made.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void handleChecked(object sender, RoutedEventArgs e)
        {
            HeadResidentBox.ItemsSource = selectedResidents;
        }

        /// <summary>
        /// Refreshes data grid
        /// </summary>
        public void refreshList()
        {
            var residents = db.Residents.Include(r => r.Household).ToList().FindAll(resident =>
            {
                if (this.household != null)
                {
                    return resident.HouseholdId == this.household.HouseholdId || resident.Household == null;
                }
                return resident.Household == null;
            });

            residentsSelectionDataGrid.ItemsSource = residents;
            HeadResidentBox.ItemsSource = selectedResidents;
        }

        void ShowInvalidInputMessage(string message)
        {
            MessageBox.Show(message, "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
