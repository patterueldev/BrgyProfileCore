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
using Microsoft.EntityFrameworkCore;

namespace BrgyProfileCore.Windows.SitioModule
{
    /// <summary>
    /// Interaction logic for UpsertSitioWindow.xaml
    /// </summary>
    public partial class UpsertSitioWindow : Window
    {
        BrgyContext db = new BrgyContext();
        public Sitio sitio;
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
        public UpsertSitioWindow()
        {
            InitializeComponent();
            this.refreshList();
        }

        public UpsertSitioWindow(Sitio sitio)
        {
            InitializeComponent();

            this.sitio = db.Sitio.Include(s => s.Residents).First(s => s.SitioId == sitio.SitioId);

            SitioNameField.Text = this.sitio.SitioName;
            this.refreshList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SitioNameField.Text.Trim() == "")
            {
                this.ShowInvalidInputMessage("Sitio name must not be empty");
                return;
            }

            var residents = selectedResidents.ToList();
            if (sitio == null)
            {
                // Is Adding
                this.sitio = new Sitio
                {
                    SitioName = SitioNameField.Text.Trim(),
                };
                db.Add(this.sitio);
            }
            else
            {
                // Is Editing
                this.sitio.SitioName = SitioNameField.Text.Trim();

                db.Update(this.sitio);
            }

            if (this.sitio.HasResidents())
            {
                var existingResidents = this.sitio.Residents.ToList();

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
                    resident.Sitio = this.sitio;
                });
                db.UpdateRange(residents);
            }

            db.SaveChanges();
            MessageBox.Show("Your changes have been made.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        /// <summary>
        /// Refreshes data grid
        /// </summary>
        public void refreshList()
        {
            var residents = db.Residents.Include(r => r.Sitio).ToList().FindAll(resident =>
            {
                if (this.sitio != null)
                {
                    return resident.SitioId == this.sitio.SitioId || resident.Sitio == null;
                }
                return resident.Sitio == null;
            });

            residentsSelectionDataGrid.ItemsSource = residents;
        }

        void ShowInvalidInputMessage(string message)
        {
            MessageBox.Show(message, "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
