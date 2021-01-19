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

namespace BrgyProfileCore.Windows.Residents
{
    /// <summary>
    /// Interaction logic for AddResidentWindow.xaml
    /// </summary>
    public partial class UpsertResidentWindow : Window
    {
        public Resident resident;
        public UpsertResidentWindow()
        {
            InitializeComponent();
            UpdateView();
        }

        public UpsertResidentWindow(Resident resident)
        {
            InitializeComponent();

            this.resident = resident;

            FirstNameField.Text = resident.FirstName;
            MiddleNameField.Text = resident.MiddleName;
            LastNameField.Text = resident.LastName;

            DateOfBirthPicker.SelectedDate = resident.DateOfBirth;
            AddressField.Text = resident.Address;
            ContactNumberField.Text = resident.ContactNumber;
            GuardianField.Text = resident.Guardian;

            UpdateView();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (FirstNameField.Text.Trim() == "")
            {
                this.ShowInvalidInputMessage("First name must not be empty");
                return;
            }

            if (LastNameField.Text.Trim() == "")
            {
                this.ShowInvalidInputMessage("Last name must not be empty");
                return;
            }

            if (DateOfBirthPicker.SelectedDate == null)
            {
                this.ShowInvalidInputMessage("Date of birth must not be empty");
                return;
            }

            if (AddressField.Text.Trim() == "")
            {
                this.ShowInvalidInputMessage("Address must not be empty");
                return;
            }

            var db = new BrgyContext();

            if (resident == null)
            {
                // Is Adding
                db.Add(
                    new Resident
                    {
                        FirstName = FirstNameField.Text.Trim(),
                        MiddleName = MiddleNameField.Text.Trim(),
                        LastName = LastNameField.Text.Trim(),

                        DateOfBirth = (DateTime)DateOfBirthPicker.SelectedDate,
                        Address = AddressField.Text.Trim(),
                        ContactNumber = ContactNumberField.Text.Trim(),
                        Guardian = GuardianField.Text.Trim(),
                    });
            }
            else
            {
                // Is Editing
                this.resident.FirstName = FirstNameField.Text.Trim();
                this.resident.MiddleName = MiddleNameField.Text.Trim();
                this.resident.LastName = LastNameField.Text.Trim();

                this.resident.DateOfBirth = (DateTime)DateOfBirthPicker.SelectedDate;
                this.resident.Address = AddressField.Text.Trim();
                this.resident.ContactNumber = ContactNumberField.Text.Trim();
                this.resident.Guardian = GuardianField.Text.Trim();

                db.Update(this.resident);
            }

            db.SaveChanges();

            MessageBox.Show("Your changes have been made.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        void ShowInvalidInputMessage(string message)
        {
            MessageBox.Show(message, "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        void UpdateView()
        {
            var title = resident == null ? "Add Resident" : "Edit Resident";
            GroupBox.Header = title;
            this.Title = title;
        }
    }
}
