using PC.DataAccess;
using PC.Utils;
using PC.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Filter.xaml
    /// </summary>
    public partial class Filter : UserControl
    {
        private ObservableCollection<Pc> pcViewSource = new ObservableCollection<Pc>();

        public Filter()
        {
            InitializeComponent();
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                using (var db = new PCEntities())
                {
                    db.Pcs.Where(q => q.Active).Load();
                    pcDataGrid.ItemsSource = db.Pcs.Local;
                }
            }

        }

        private void BtnAddFilterClick(object sender, RoutedEventArgs e)
        {
            filters.Children.Add(new FilteriItem());
        }

        private void BtnFilterClick(object sender, RoutedEventArgs e)
        {
            using (var db = new PCEntities())
            {
                var result = db.Pcs.Where(q => q.Active).AsEnumerable();

                foreach (var item in filters.Children)
                {
                    if (item is FilteriItem)
                    {
                        var filterOptionObject = ((item as FilteriItem).FindName("filter_combobox") as ComboBox).SelectedItem;
                        var filterOption = (Filters)Enum.Parse(typeof(Filters), filterOptionObject.ToString());

                        var filterText = ((item as FilteriItem).FindName("filter_value") as TextBox).Text;
                        var location_option = ((item as FilteriItem).FindName("location_options") as ComboBox).SelectedItem.ToString();
                        result = LoadDataSource(result, filterOption, filterText.ToLower(), location_option);
                    }
                }

                pcDataGrid.ItemsSource = Util.ToObservableCollection(result);
            }

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.

        }

        private IEnumerable<Pc> LoadDataSource(IEnumerable<Pc> PcList, Filters? option, string query, string location_option)
        {

            query = Util.RejectMarks(query);
            location_option = Util.RejectMarks(location_option);

            using (var db = new PCEntities())
            {
                if (option != null)
                {
                    switch (option.Value)
                    {
                        case Filters.Pc_Name:
                            PcList = PcList.Where(q => Util.RejectMarks(q.PC_Name).Contains(query) && q.Active == true);
                            break;
                        case Filters.PB:
                            PcList = PcList.Where(q => Util.RejectMarks(q.PB).Contains(query) && q.Active == true);
                            break;
                        case Filters.NV:
                            PcList = PcList.Where(q => Util.RejectMarks(q.NV).Contains(query) &&
                                            q.Active == true);
                            break;
                        case Filters.MAC:
                            PcList = PcList.Where(q => q.MAC.Equals(query) && q.Active == true);
                            break;
                        case Filters.MAC2:
                            PcList = PcList.Where(q => q.MAC2.Equals(query) && q.Active == true);
                            break;
                        case Filters.IP:
                            PcList = PcList.Where(q => q.IP.Equals(query) && q.Active == true);
                            break;
                        case Filters.Location:
                            PcList = PcList.Where(q => Util.RejectMarks(q.Office_Located).Contains(location_option) &&
                                            q.Active == true);
                            break;
                        default:
                            break;
                    }
                }

                return PcList;
            }
        }
    }
}
