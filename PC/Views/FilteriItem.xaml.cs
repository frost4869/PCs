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
        public FilteriItem()
        {
            InitializeComponent();

            filter_combobox.ItemsSource = Enum.GetValues(typeof(Filters)).Cast<Filters>();
        }

        private void BtnRemoveFilterClick(object sender, RoutedEventArgs e)
        {
            ((StackPanel)Parent).Children.Remove(this);
        }
    }
}
