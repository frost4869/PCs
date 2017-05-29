using MahApps.Metro.Controls;
using PC.DataAccess;
using PC.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace PC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private readonly IBaseRepoAsync<Pc> _pcRepo;
        private PCEntities pcContext = new PCEntities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //System.Windows.Data.CollectionViewSource pcViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("pcViewSource")));
            //// Load data by setting the CollectionViewSource.Source property:
            //// pcViewSource.Source = [generic data source]
            //pcContext.Pcs.Load();
            //pcViewSource.Source = pcContext.Pcs.Local;
        }

        private void BtnCreateClick(object sender, RoutedEventArgs e)
        {
            MetroTabItem item = new MetroTabItem
            {
                Header = "Create new PC",
                CloseButtonEnabled = true,
            };

            Views.CreateForm view = new Views.CreateForm();
            item.Content = view;

            MainTabControl.Items.Add(item);
        }

        private void BtnSearchClick(object sender, RoutedEventArgs e)
        {
        }

        private void BtnListClick(object sender, RoutedEventArgs e)
        {

        }

        private void MainTabControl_TabItemClosingEvent(object sender, BaseMetroTabControl.TabItemClosingEventArgs e)
        {
            if (e.ClosingTabItem.Header.ToString().StartsWith("sizes"))
                e.Cancel = true;
        }
    }
}
