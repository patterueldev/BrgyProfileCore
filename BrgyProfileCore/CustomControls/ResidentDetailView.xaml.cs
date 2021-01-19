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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BrgyProfileCore.CustomControls
{
    /// <summary>
    /// Interaction logic for ResidentDetailView.xaml
    /// </summary>
    public partial class ResidentDetailView : UserControl
    {
        /// <summary>
        /// Field Header displayed on the left side
        /// </summary>
        public string FieldHeader
        {
            get
            {
                return (string)FieldHeaderLabel.Content;
            }
            set
            {
                FieldHeaderLabel.Content = value;
            }
        }

        /// <summary>
        /// Field Value displayed on the right side
        /// </summary>
        public string FieldValue
        {
            get
            {
                return (string)FieldValueLabel.Content;
            }
            set
            {
                FieldValueLabel.Content = value;
            }
        }
        public ResidentDetailView()
        {
            InitializeComponent();
        }
    }
}
