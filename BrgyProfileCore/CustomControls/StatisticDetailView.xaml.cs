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
    public partial class StatisticDetailView : UserControl
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
        public string FieldValueText
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

        public static readonly DependencyProperty FieldValueTextProperty = DependencyProperty.Register(
            "FieldValueText",  // "ProductTitleText" ?
            typeof(string),
            typeof(StatisticDetailView), new UIPropertyMetadata(FieldValueTextChangeHandler));

        public static void FieldValueTextChangeHandler(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Get instance of current control from sender
            // and property value from e.NewValue

            // Set public property on TaregtCatalogControl, e.g.
            ((StatisticDetailView)sender).FieldValueText = e.NewValue.ToString();
        }
        public StatisticDetailView()
        {
            InitializeComponent();
        }
    }
}
