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
    /// Interaction logic for SitioListWindow.xaml
    /// </summary>
    public partial class SitioListWindow : Window
    {
        Sitio selectedSitio
        {
            get
            {
                // loads index of selected row
                var idx = sitioDataGrid.SelectedIndex;

                if (idx < 0 || idx >= sitioDataGrid.Items.Count)
                {
                    return null;
                }
                return (Sitio)sitioDataGrid.Items[idx];
            }
        }
        public SitioListWindow()
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

        private void SitioResidentsButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void AddSitioButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new UpsertSitioWindow();
            window.ShowDialog();
        }

        private void EditSitioButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSitio == null)
            {
                return;
            }

            var window = new UpsertSitioWindow(selectedSitio);
            window.ShowDialog();
        }

        private void DeleteSitioButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSitio == null)
            {
                return;
            }

            var sitio = selectedSitio;
            MessageBoxResult messageBoxResult = MessageBox.Show($"Delete {sitio.SitioName} from the records? This action cannot be undone.", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var db = new BrgyContext();
                db.Remove(sitio);
                db.SaveChanges();

                this.refreshList();
            }
        }

        private void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = SearchTextBox.Text;
            this.refreshList(keyword);
        }

        /// <summary>
        /// Refreshes data grid
        /// </summary>
        public void refreshList(string keyword = null)
        {
            var db = new BrgyContext();
            sitioDataGrid.ItemsSource = db.Sitio.Include(h => h.Residents).ToList().FindAll(sitio =>
            {
                if (keyword == null)
                {
                    return true;
                }
                var residents = new List<Resident>();
                if (sitio.Residents.Count > 0)
                {
                    residents = sitio.Residents.FindAll(resident =>
                    {
                        return resident.FullName.ToLower().Contains(keyword.ToLower());
                    });
                }
                return sitio.SitioName.ToLower().Contains(keyword.ToLower()) || residents.Count > 0;
            });
        }

        private void sitioDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // loads index of selected row
            var idx = sitioDataGrid.SelectedIndex;

            // toggles buttons
            SitioResidentsButton.IsEnabled = idx >= 0;
            EditSitioButton.IsEnabled = idx >= 0;
            DeleteSitioButton.IsEnabled = idx >= 0;
        }
    }
}
