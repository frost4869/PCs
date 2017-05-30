using MahApps.Metro.Controls;
using PC.DataAccess;
using PC.DataAccess.Repository;
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
    /// Interaction logic for CreateForm.xaml
    /// </summary>
    public partial class CreateForm : UserControl
    {
        public Pc pc = new Pc();
        public CreateForm()
        {
            InitializeComponent();
            pc_form.DataContext = pc;
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
        }

        private async void BtnSaveClickAsync(object sender, RoutedEventArgs e)
        {
            MetroAnimatedSingleRowTabControl mainTabControl = Util.FindParent<MetroAnimatedSingleRowTabControl>(this);
            var currentTab = (MetroTabItem)mainTabControl.SelectedItem;

            using (var db = new PCEntities())
            {
                try
                {
                    pc.Active = true;
                    db.Pcs.Add(pc);
                    await db.SaveChangesAsync();

                    currentTab.Header = pc.PC_Name;
                    btnSave.Content = "Saved";
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }
    }
}
