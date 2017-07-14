using PC.DataAccess;
using PC.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PC.Views
{
    /// <summary>
    /// Interaction logic for FilteriItem.xaml
    /// </summary>
    public partial class FilteriItem : UserControl
    {
        private bool handle = true;
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (handle) Handle();
            handle = true;
        }

        private void Handle()
        {
            var filterOption = (Filters)Enum.Parse(typeof(Filters), filter_combobox.SelectedItem.ToString());

            if(filterOption == Filters.Location)
            {
                filter_value.Visibility = Visibility.Hidden;
                location_options.Visibility = Visibility.Visible;
            }
            else
            {
                filter_value.Visibility = Visibility.Visible;
                location_options.Visibility = Visibility.Hidden;
            }
        }

        public FilteriItem()
        {
            InitializeComponent();
            filter_combobox.ItemsSource = Enum.GetValues(typeof(Filters)).Cast<Filters>();
            try
            {
                using (var db = new PCEntities())
                {
                    var locations = db.Offices.ToList();
                    location_options.ItemsSource = locations.Select(q => q.Location);
                }
            }
            catch (Exception ex)
            {
                Util.WriteLog(ex.Message + "\n" + ex.StackTrace);
                Util.ShowMessageBoxAsync("Error", ex.Message);
                throw;
            }
        }

        private void BtnRemoveFilterClick(object sender, RoutedEventArgs e)
        {
            ((StackPanel)Parent).Children.Remove(this);
        }

        private void filter_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            Handle();
        }

        private void filter_combobox_DropDownClosed(object sender, EventArgs e)
        {
            if (handle) Handle();
            handle = true;
        }
    }
}
