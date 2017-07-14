using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using PC.DataAccess;
using PC.Utils;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

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

            try
            {
                using (var db = new PCEntities())
                {
                    var locations = db.Offices.AsQueryable().Select(q => q.Location).ToList();
                    foreach (var office in locations)
                    {
                        office_LocatedComboBox.Items.Add(office);
                    }

                    var item = new ComboBoxItem();
                    OtherLocation otherlocation = new OtherLocation();
                    item.Content = otherlocation;
                    office_LocatedComboBox.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Util.WriteLog(ex.Message + ex.StackTrace);
                Util.ShowMessageBoxAsync("Error", ex.Message);
                throw;
            }

            office_LocatedComboBox.SelectedIndex = 0;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            type_combo_box.ItemsSource = Enum.GetValues(typeof(Types)).Cast<Types>();
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
                        var otherLocation = ((office_LocatedComboBox.Items
                            .GetItemAt(office_LocatedComboBox.Items.Count - 1) as ComboBoxItem).Content as OtherLocation)
                            .FindChild<TextBox>("txt_otherLocation").Text;

                        if (!string.IsNullOrEmpty(otherLocation))
                        {
                            db.Offices.Add(new Office
                            {
                                Location = otherLocation,
                                Active = true,
                            });
                            await db.SaveChangesAsync();

                            pc.Office_Located = otherLocation;
                        }

                        pc.Active = true;

                        db.Pcs.Add(pc);
                        await db.SaveChangesAsync();

                        currentTab.Header = pc.PC_Name;
                        btnSave.Content = "Saved";
                    }
                    catch (Exception ex)
                    {
                        Util.WriteLog(ex.Message + "\n" + ex.StackTrace);
                        Util.ShowMessageBoxAsync("Error", ex.Message);
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
                using (var db = new PCEntities())
                {
                    if (db.Pcs.Any(q => q.PC_Name.ToLower().Equals(pc.PC_Name.ToLower()) && q.Active == true))
                    {
                        check.ValidateMessage = "Pc already existed";
                        return check;
                    }

                    if (!String.IsNullOrEmpty(pc.PC_Name) && !String.IsNullOrEmpty(pc.Type) && !String.IsNullOrEmpty(pc.PB) && !String.IsNullOrEmpty(pc.Office_Located) && (!String.IsNullOrEmpty(pc.MAC) || !String.IsNullOrEmpty(pc.MAC2)))
                    {
                        var addMacReg1 = "^[0-9A-Fa-f]{2}-[0-9A-Fa-f]{2}-[0-9A-Fa-f]{2}-[0-9A-Fa-f]{2}-[0-9A-Fa-f]{2}-[0-9A-Fa-f]{2}$";
                        var addMacReg2 = "^[0-9A-Fa-f]{2}:[0-9A-Fa-f]{2}:[0-9A-Fa-f]{2}:[0-9A-Fa-f]{2}:[0-9A-Fa-f]{2}:[0-9A-Fa-f]{2}$";

                        var regex1 = new Regex(addMacReg1);
                        var regex2 = new Regex(addMacReg2);

                        if (!String.IsNullOrEmpty(pc.MAC) && !String.IsNullOrEmpty(pc.MAC2))
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
