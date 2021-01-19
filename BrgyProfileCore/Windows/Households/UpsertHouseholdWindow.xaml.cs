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

namespace BrgyProfileCore.Windows.Households
{
    /// <summary>
    /// Interaction logic for UpsertHouseholdWindow.xaml
    /// </summary>
    public partial class UpsertHouseholdWindow : Window
    {
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
        }

        int headResidentId
        {
            get
            {
                var headResident = (Resident)HeadResidentBox.SelectedItem;
                if (headResident != null)
                {
                    return headResident.ResidentId;
                }
                return 0;
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
            this.household = household;

            HouseholdNameField.Text = household.HouseholdName;
            this.refreshList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (HouseholdNameField.Text.Trim() == "")
            {
                this.ShowInvalidInputMessage("Household name must not be empty");
                return;
            }

            var db = new BrgyContext();
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
            var db = new BrgyContext();

            var residents = db.Residents.ToList().FindAll(resident =>
            {
                if (this.household != null)
                {
                    return resident.Household == this.household || resident.Household == null;
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
