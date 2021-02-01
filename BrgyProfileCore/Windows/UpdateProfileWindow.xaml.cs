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

namespace BrgyProfileCore.Windows
{
    /// <summary>
    /// Interaction logic for UpdateProfileWindow.xaml
    /// </summary>
    public partial class UpdateProfileWindow : Window
    {
        public UpdateProfileWindow()
        {
            InitializeComponent();

            var settings = Properties.Settings.Default;
            BrgyNameField.Text = settings.BrgyName;
            MunicipalityField.Text = settings.Municipality;
            ProvinceField.Text = settings.Province;

            PreparedByField.Text = settings.RBI_PreparedBy;
            CertifiedCorrectField.Text = settings.RBI_CertifiedCorrected;
            ValidatedByField.Text = settings.RBI_ValidatedBy;

            PreparedByTitleField.Text = settings.RBI_PreparedByTitle;
            CertifiedCorrectTitleField.Text = settings.RBI_CertifiedCorrectedTitle;
            ValidatedByTitleField.Text = settings.RBI_ValidatedByTitle;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            if (BrgyNameField.Text.Trim() == "")
            {
                this.ShowInvalidInputMessage("Brgy name must not be empty");
                return;
            }

            //if (MunicipalityField.Text.Trim() == "")
            //{
            //    this.ShowInvalidInputMessage("Last name must not be empty");
            //    return;
            //}

            //if (ProvinceField.Text.Trim() == "")
            //{
            //    this.ShowInvalidInputMessage("Address must not be empty");
            //    return;
            //}

            var settings = Properties.Settings.Default;
            settings.BrgyName = BrgyNameField.Text.Trim();
            settings.Municipality = MunicipalityField.Text.Trim();
            settings.Province = ProvinceField.Text.Trim();

            settings.RBI_PreparedBy = PreparedByField.Text.Trim();
            settings.RBI_CertifiedCorrected = CertifiedCorrectField.Text.Trim();
            settings.RBI_ValidatedBy = ValidatedByField.Text.Trim();

            settings.RBI_PreparedByTitle = PreparedByTitleField.Text.Trim();
            settings.RBI_CertifiedCorrectedTitle = CertifiedCorrectTitleField.Text.Trim();
            settings.RBI_ValidatedByTitle = ValidatedByTitleField.Text.Trim();

            settings.Save();

            MessageBox.Show("Your changes have been made.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        void ShowInvalidInputMessage(string message)
        {
            MessageBox.Show(message, "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void updateUsernameButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new UpdateUsernameWindow();
            window.ShowDialog();
        }
    }
}
