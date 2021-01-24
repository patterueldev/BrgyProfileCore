using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
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
    /// Interaction logic for AddResidentWindow.xaml
    /// </summary>
    public partial class UpsertResidentWindow : Window
    {
        public Resident resident;

        BrgyContext db = new BrgyContext();

        public UpsertResidentWindow()
        {
            InitializeComponent();

            var households = db.Households.Include(h => h.Residents).ToList();
            HouseholdBox.ItemsSource = households;

            var sitioList = db.Sitio.Include(s => s.Residents).ToList();
            SitioBox.ItemsSource = sitioList;

            UpdateView();
        }

        public UpsertResidentWindow(Resident resident)
        {
            InitializeComponent();

            this.resident = db.Residents.First(r => r.ResidentId == resident.ResidentId);

            FirstNameField.Text = resident.FirstName;
            MiddleNameField.Text = resident.MiddleName;
            LastNameField.Text = resident.LastName;

            DateOfBirthPicker.SelectedDate = resident.DateOfBirth;
            AddressStreetNameField.Text = resident.Address;
            ContactNumberField.Text = resident.ContactNumber;
            //GuardianField.Text = resident.Guardian;

            var households = db.Households.Include(h => h.Residents).ToList();
            HouseholdBox.ItemsSource = households;
            HouseholdBox.SelectedItem = households.FirstOrDefault(h =>
            {
                return h.HouseholdId == resident.HouseholdId;
            });

            var sitioList = db.Sitio.Include(s => s.Residents).ToList();
            SitioBox.ItemsSource = sitioList;
            SitioBox.SelectedItem = sitioList.FirstOrDefault(s =>
            {
                return s.SitioId == resident.SitioId;
            });

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

            if (AddressStreetNameField.Text.Trim() == "")
            {
                this.ShowInvalidInputMessage("Address must not be empty");
                return;
            }

            var household = (Household)this.HouseholdBox.SelectedItem;
            var sitio = (Sitio)this.SitioBox.SelectedItem;

            if (resident == null)
            {
                var resident = new Resident
                {
                    FirstName = FirstNameField.Text.Trim(),
                    MiddleName = MiddleNameField.Text.Trim(),
                    LastName = LastNameField.Text.Trim(),

                    DateOfBirth = (DateTime)DateOfBirthPicker.SelectedDate,
                    Address = AddressStreetNameField.Text.Trim(),
                    ContactNumber = ContactNumberField.Text.Trim(),
                    //Guardian = GuardianField.Text.Trim(),
                };

                // Is Adding
                if (household != null)
                {
                    household.Residents.Add(resident);
                }
                
                if(sitio != null)
                {
                    sitio.Residents.Add(resident);
                }


                if(household == null && sitio == null)
                {
                    db.Add(resident);
                }
            }
            else
            {
                // Is Editing
                this.resident.FirstName = FirstNameField.Text.Trim();
                this.resident.MiddleName = MiddleNameField.Text.Trim();
                this.resident.LastName = LastNameField.Text.Trim();

                this.resident.DateOfBirth = (DateTime)DateOfBirthPicker.SelectedDate;
                this.resident.Address = AddressStreetNameField.Text.Trim();
                this.resident.ContactNumber = ContactNumberField.Text.Trim();
                //this.resident.Guardian = GuardianField.Text.Trim();

                this.resident.Household = household;
                this.resident.Sitio = sitio;

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

        private void autofillbutton_click(object sender, RoutedEventArgs e)
        {
            FirstNameField.Text = Faker.Name.First();
            LastNameField.Text = Faker.Name.Last();
            AddressStreetNameField.Text = Faker.Address.StreetAddress();
        }
    }
}
