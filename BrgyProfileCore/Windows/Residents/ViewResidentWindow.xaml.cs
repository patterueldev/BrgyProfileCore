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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ViewResidentWindow : Window
    {
        Resident resident;

        public ViewResidentWindow(Resident resident)
        {
            InitializeComponent();

            this.resident = resident;

            FirstNameDetailView.FieldValue = NAifNull(resident.FirstName);
            MiddleNameDetailView.FieldValue = NAifNull(resident.MiddleName);
            LastNameDetailView.FieldValue = NAifNull(resident.LastName);
            if (resident.DateOfBirth != null)
            {
                DateOfBirthDetailView.FieldValue = resident.DateOfBirth.ToString("MMM dd, yyyy");
            }
            else
            {
                DateOfBirthDetailView.FieldValue = "N/A";
            }
            AddressDetailView.FieldValue = NAifNull(resident.Address);
            ContactNumberDetailView.FieldValue = NAifNull(resident.ContactNumber);
        }

        string NAifNull(string str)
        {
            if (str == null || str.Trim() == "")
            {
                return "N/A";
            }
            return str;
        }
    }
}
