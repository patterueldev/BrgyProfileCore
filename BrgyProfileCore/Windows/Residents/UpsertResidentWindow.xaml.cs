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

        List<string> Religions = new List<string>()
        {
            "Roman Catholic",
            "Iglesia ni Cristo",
            "Seventh Day Adventist",
            "Baptist",
            "Jehova's Witnesses",
        };

        List<string> EducationalAttainments = new List<string>()
        {
            "Elementary",
            "High School",
            "College",
            "Vocational",
            "Graduate School"
        };

        List<string> Occupations = new List<string>()
        {

        };
        private void LoadReferences()
        {
            var residents = db.Residents.ToList();
            foreach (var r in residents)
            {
                if (r.Religion == null || r.Religion.Trim() == "")
                {
                    continue;
                }
                if (!Religions.Contains(r.Religion))
                {
                    Religions.Add(r.Religion);
                }
            }

            foreach (var r in residents)
            {
                if (r.HighestEducationalAttainment == null || r.HighestEducationalAttainment.Trim() == "")
                {
                    continue;
                }
                if (!EducationalAttainments.Contains(r.HighestEducationalAttainment))
                {
                    EducationalAttainments.Add(r.HighestEducationalAttainment);
                }
            }

            foreach (var r in residents)
            {
                if (r.Occupation == null || r.Occupation.Trim() == "")
                {
                    continue;
                }
                if (!Occupations.Contains(r.Occupation))
                {
                    Occupations.Add(r.Occupation);
                }
            }
        }

        public UpsertResidentWindow()
        {
            InitializeComponent();

            LoadReferences();
            religionBox.ItemsSource = Religions;
            educationalAttainmentBox.ItemsSource = EducationalAttainments;
            occupationBox.ItemsSource = Occupations;

            this.resident = new Resident();
            resident.DateOfBirth = new DateTime(2000, 1, 1);

            var households = db.Households.Include(h => h.Residents).ToList();
            HouseholdBox.ItemsSource = households;
            var sitioList = db.Sitio.Include(s => s.Residents).ToList();
            SitioBox.ItemsSource = sitioList;

            UpdateView();
        }

        public UpsertResidentWindow(Resident resident)
        {
            InitializeComponent();

            LoadReferences();
            religionBox.ItemsSource = Religions;
            educationalAttainmentBox.ItemsSource = EducationalAttainments;
            occupationBox.ItemsSource = Occupations;

            this.resident = db.Residents.First(r => r.ResidentId == resident.ResidentId);

            //FirstNameField.Text = resident.FirstName;
            //MiddleNameField.Text = resident.MiddleName;
            //LastNameField.Text = resident.LastName;

            //DateOfBirthPicker.SelectedDate = resident.DateOfBirth;
            //AddressStreetNameField.Text = resident.Address;
            //ContactNumberField.Text = resident.ContactNumber;
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = resident;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsNullOrEmpty(resident.FirstName))
            {
                this.ShowInvalidInputMessage("First name must not be empty");
                return;
            }

            if (IsNullOrEmpty(resident.LastName))
            {
                this.ShowInvalidInputMessage("Last name must not be empty");
                return;
            }

            if (IsNullOrEmpty(resident.Gender))
            {
                this.ShowInvalidInputMessage("Gender must not be empty");
                return;
            }

            if (IsNullOrEmpty(resident.MaritalStatus))
            {
                this.ShowInvalidInputMessage("Marital Status must not be empty");
                return;
            }

            if (IsNullOrEmpty(resident.AddressStreet))
            {
                this.ShowInvalidInputMessage("Street Name must not be empty");
                return;
            }

            if (IsNullOrEmpty(resident.AddressSubdivision))
            {
                this.ShowInvalidInputMessage("Name of Subdivision must not be empty");
                return;
            }

            if (resident.DateOfBirth == null)
            {
                this.ShowInvalidInputMessage("Date of birth must not be empty");
                return;
            }

            if (IsNullOrEmpty(resident.PlaceOfBirth))
            {
                this.ShowInvalidInputMessage("Place of Birth must not be empty");
                return;
            }

            if (IsNullOrEmpty(resident.Religion))
            {
                this.ShowInvalidInputMessage("Religious Affiliation must not be empty");
                return;
            }

            if (IsNullOrEmpty(resident.Citizenship))
            {
                this.ShowInvalidInputMessage("Citizenship must not be empty");
                return;
            }

            if (!IsNullOrEmpty(resident.TotalActualIncomeofEarningMember))
            {
                double income;
                if (double.TryParse(resident.TotalActualIncomeofEarningMember, out income))
                {
                    // success
                }
                else
                {
                    this.ShowInvalidInputMessage("Total Actual Income of Earning Member is not valid.");
                    return;
                }

            }
            var household = (Household)this.HouseholdBox.SelectedItem;
            var sitio = (Sitio)this.SitioBox.SelectedItem;

            // need to identify if resident is already created or new
            if (resident.ResidentId == 0)
            {
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

        bool IsNullOrEmpty(string validatable)
        {
            if(validatable != null)
            {
                return validatable.Trim() == "";
            }
            return true;
        }

        void UpdateView()
        {
            var title = resident == null ? "Add Resident" : "Edit Resident";
            GroupBox.Header = title;
            this.Title = title;
        }
    }
}
