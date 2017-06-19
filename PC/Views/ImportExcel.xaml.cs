using PC.DataAccess;
using PC.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ImportExcel.xaml
    /// </summary>
    public partial class ImportExcel : UserControl
    {
        private ObservableCollection<Pc> pcList;
        public ImportExcel(IEnumerable<Pc> _pcList)
        {
            this.pcList = Util.ToObservableCollection(_pcList);
            InitializeComponent();
            LoadDataSource();
        }

        private void LoadDataSource()
        {
            if(pcList != null)
            {
                pcImportDataGrid.ItemsSource = pcList;
            }
        }
    }
}
