using AutoMapper;
using PC.DataAccess;
using PC.DataAccess.Repository;
using PC.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PC.Views
{
    /// <summary>
    /// Interaction logic for AllPC.xaml
    /// </summary>
    public partial class AllPC : UserControl
    {
        private readonly PCEntities db = new PCEntities();
        public AllPC()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }

            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                CollectionViewSource pcViewSource = ((CollectionViewSource)(this.FindResource("pcViewSource")));
                db.Pcs.Load();
                var source = db.Pcs.Local.Where(q => q.Active);
                pcViewSource.Source = source;
            }
        }
    }
}
