using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using PC.DataAccess;
using PC.DataAccess.Repository;
using PC.Utils;
using System;
using System.Collections.Generic;
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
            var validated = this.ValidateInput();

            if (validated.IsValidated)
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
            else
            {
                var metroWindow = (Application.Current.MainWindow as MetroWindow);
                await metroWindow.ShowMessageAsync("Warning", validated.ValidateMessage);
            }

        }

        private Validation ValidateInput()
        {
            var check = new Validation();
            if (pc != null)
            {
                if (!String.IsNullOrEmpty(pc.PC_Name) && !String.IsNullOrEmpty(pc.Type) && !String.IsNullOrEmpty(pc.OS) && !String.IsNullOrEmpty(pc.PB) && !String.IsNullOrEmpty(pc.Office_Located) && (!String.IsNullOrEmpty(pc.MAC) || !String.IsNullOrEmpty(pc.MAC2)))
                {
                    var addMacReg1 = "^[0-9A-Fa-f]{2}-[0-9A-Fa-f]{2}-[0-9A-Fa-f]{2}-[0-9A-Fa-f]{2}-[0-9A-Fa-f]{2}-[0-9A-Fa-f]{2}$";
                    var addMacReg2 = "^[0-9A-Fa-f]{2}:[0-9A-Fa-f]{2}:[0-9A-Fa-f]{2}:[0-9A-Fa-f]{2}:[0-9A-Fa-f]{2}:[0-9A-Fa-f]{2}$";

                    var regex1 = new Regex(addMacReg1);
                    var regex2 = new Regex(addMacReg2);

                    if(!String.IsNullOrEmpty(pc.MAC) && !String.IsNullOrEmpty(pc.MAC2))
                    {
                        if ((regex1.IsMatch(pc.MAC) || regex2.IsMatch(pc.MAC)) && (regex1.IsMatch(pc.MAC2) || regex2.IsMatch(pc.MAC2)))
                        {
                            check.IsValidated = true;
                        }
                        else
                        {
                            check.ValidateMessage = "MAC or MAC2 format is NOT a valid mac address format (##:##:##:##:##:##)\n";
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(pc.MAC))
                        {
                            if (regex1.IsMatch(pc.MAC) || regex2.IsMatch(pc.MAC))
                            {
                                check.IsValidated = true;
                            }
                            else
                            {
                                check.ValidateMessage = "MAC format is NOT a valid mac address format (##:##:##:##:##:##)\n";
                            }
                        }


                        if (!String.IsNullOrEmpty(pc.MAC2))
                        {
                            if (regex1.IsMatch(pc.MAC2) || regex2.IsMatch(pc.MAC2))
                            {
                                check.IsValidated = true;
                            }
                            else
                            {
                                check.ValidateMessage += "MAC2 format is NOT a valid mac address format (##:##:##:##:##:##)\n";
                            }
                        }
                    }
                }
                else
                {
                    check.ValidateMessage += "Fields with [ * ] mark must not be empty\n";
                }
            }

            return check;
        }

        private class Validation
        {
            public bool IsValidated { get; set; } = false;
            public string ValidateMessage { get; set; }
        }
    }
}
